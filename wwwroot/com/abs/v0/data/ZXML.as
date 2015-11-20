/**
 * @author Sebastian Sanabria Diaz
 * @version 0.1
 */

package com.abs.v0.data{

	import flash.net.URLRequest;
	import flash.net.URLLoader;
	import flash.events.Event;
	import flash.xml.XMLDocument;
	/**
	 * Basado en XMLDocument, para manejar listas simples de XML.<br />
	 * Cada nodo puede tener un numero indefinido de atributos.<br />
	 * Toda la lista se maneja como si fuera circular, lo que quiere decir que,<br />
	 * si se acaban los items o nodos de la lista, vuelve al inicio y viceverza.
	 */
	public class ZXML extends XMLDocument {

		public var url:URLRequest;
		public var loader:URLLoader;

		public var _index:int = 0;
		public var _indexPantalla:int = 0;
		public var numNodos:uint = 0;
		public var _numNodosPantalla:uint = 0;
		public var _numPantallas:uint = 0;
		
		private var _indexPaginado:int = 0;
		private var _numNodosPaginado:uint = 0;
		private var _paginado:Array = new Array();
		private var _primerPaginado:uint = 0;

		private static var _cargado:Boolean = false;
		
		private var f:Function;
		

		/**
		 * 
		 * @param	path Ruta fisica o generada por servidor web, del XML de lista simple.
		 * @param	f <code>Function</code> que se ejecutara al dispararse la carga del XML.
		 */	
		function ZXML(path:String = null, f:Function = null) {
			if(path != null){
				this.f = f;
				url=new URLRequest(path);
				loader = new URLLoader(url);
				loader.addEventListener(Event.COMPLETE, onLoadXML);
			};
		}
		
		/**
		 * Llama a  <code>f</code> de tipo  <code>Function</code> pasado en el constructor.<br />
		 * Se dispara al cargarse el  <code>ZXML</code>. No se puede llamar directamente.
		 * @event
		 *  @see ZXML
		 */
		private function onLoadXML(e:Event):void {
			_cargado = true;
			this.ignoreWhite = true;
			this.parseXML(loader.data);
			numNodos = this.firstChild.childNodes.length;
			f();
		}
		/**
		 * Obtiene el siguiente atributo definido por el parametro <code>atributo</code>.<br />
		 * @param	atributo Nombre del atributo que se necesita llamar de un nodo especifico.
		 * @param	avance Valor entero que indica 
		 * @return  El valor contenido en el tag atributo dentro del nodo del XML.
		 */
		public function getNextAttribute(atributo:String,avance:int):String {
			_index +=avance;
			if (_index >= numNodos) {
				_index = 0;
			} else if (_index < 0) {
				_index = numNodos -1;
			}
			return this.firstChild.childNodes[_index].attributes[atributo];
		}
		/**
		 * Obtiene el valor del atributo en un indice determinado.
		 * @param	atributo Nombre del atributo del cual necesitamos el valor.
		 * @param	my_index Indice de 0 a <code>numNodos-1</code> del cual queremos el atributo.
		 * @return  El valor contenido en el tag atributo dentro del nodo del XML. 
		 */
		public function getAttribute(atributo:String,my_index:uint):String {
			_index = my_index;
			return this.firstChild.childNodes[_index].attributes[atributo];
		}
		
		public function getAttributesWhere(atributo:String, dondeAtributo:String, tengaValor:String):Array {
			var res:Array = new Array();
			for (var k:uint; k < numNodos; k++ ) {
				if (this.firstChild.childNodes[k].attributes[dondeAtributo] == tengaValor) {
					res.push(this.firstChild.childNodes[k].attributes[atributo] )
				}
			}
			return res;
		}
		
		/**
		 * Devuelve el valor del atributo manejado por el indice actual.
		 * @param	atributo Nombre del atributo del cual necesitamos el valor.
		 * @return  El valor contenido en el tag atributo dentro del nodo del XML. 
		 */
		public function getCurrentAttribute(atributo:String):String{
			return this.firstChild.childNodes[_index].attributes[atributo];
		}
		/**
		 * Devuelve la lista de valores marcados por <code>atributo</code> 
		 * @param	indexPantalla Valor definido de 0 hasta <code>numPantallas</code>
		 * @param	atributo Nombre del atributo del cual necesitamos el valor.
		 * @return  <code>Array</code> con <code>String</code>s con los valores de los atributos tomados de la pantalla.
		 * @see numPantallas
		 */
		public function getPantalla(indexPantalla:uint,atributo:String):Array {
			var atributos:Array = new Array();
			this.indexPantalla = indexPantalla;
			trace("INDEX: "+_index);
			for (var k:uint=_index; k<_index+_numNodosPantalla; k++) {
				//trace(this.firstChild.childNodes[k].attributes[atributo]);
				atributos.push(this.firstChild.childNodes[k].attributes[atributo]);
			}
			return atributos;
		}
		/**
		 * Devuelve la lista de valores marcados por <code>atributo</code> y el <code>index</code> actual.
		 * @param	atributo Nombre del atributo del cual necesitamos el valor.
		 * @return  <code>Array</code> con <code>String</code>s con los valores de los atributos tomados de la pantalla.
		 * @see index
		 */
		public function getPantallaActual(atributo:String):Array {
			var atributos:Array = new Array();
			//this.indexPantalla = indexPantalla;
			trace("INDEX: "+_index);
			for (var k:uint=_index; k<_index+_numNodosPantalla; k++) {
				//trace(this.firstChild.childNodes[k].attributes[atributo]);
				try {
					atributos.push(this.firstChild.childNodes[k].attributes[atributo]);
				} catch (err:Error) {
					atributos.push("")
					//break;
				}
			}
			return atributos;
		}
		/**
		 * Obtiene el paginado actual.
		 * @return  <code>Array</code> con el paginado actual.
		 */
		public function getPaginado():Array{
			return _paginado;
		}		
		//
		//
		//
		/**
		 * Establece un nuevo indice para obtener atributos.
		 */
		public function set index(value:uint) {
			_index = value;
			//TODO:actualizar _indexPantalla

		}
		
		/**
		 * Obtiene el indice actual para obtener atributos.
		 */
		public function get index():uint {
			return _index;
		}
		//
		/**
		 * Establece un nuevo indice para grupo de paginacion.
		 */
		public function set indexPantalla(value:int):void  {
			_indexPantalla = value;
			if (_indexPantalla >= _numPantallas) {
				_indexPantalla = 0;
				/******************/
				_primerPaginado = _indexPantalla;
				numNodosPaginado = _numNodosPaginado;
				/******************/
				
			} else if (_indexPantalla < 0) {
				_indexPantalla = _numPantallas - 1;
				/******************/
				_primerPaginado = _indexPantalla - _numNodosPaginado;
				numNodosPaginado = _numNodosPaginado;
				/******************/
			}
			_index = (_indexPantalla * _numNodosPantalla);
			/**************/
			if (_indexPantalla >= _primerPaginado + _numNodosPaginado){
				_primerPaginado = _indexPantalla;
				numNodosPaginado = _numNodosPaginado;
			}else if (_indexPantalla < _primerPaginado){
				_primerPaginado = _indexPantalla;
				numNodosPaginado = _numNodosPaginado;
			}
			/**************/
		}
		/**
		 * Obtiene el indice par ael grupo actual de paginacion.
		 */
		public function get indexPantalla():int {
			return _indexPantalla;
		}
		/**
		 * Establece cuantos nodos deben haber en una pantalla de paginacion.
		 */
		public function set numNodosPantalla(value:uint):void {
			_numNodosPantalla = value;
			_numPantallas = numNodos / _numNodosPantalla;
			if (numNodos % _numNodosPantalla != 0){
				_numPantallas++;
			}
		}
		/**
		 * Obtiene el numero asignado para la pantalla de paginacion.
		 */
		public function get numNodosPantalla():uint {
			return _numNodosPantalla;
		}
		/**
		 * Fuerza el numero de pantallas a mostrar, y autoasigna el numero de nodos por pantalla.
		 */
		public function set numPantallas(value:uint):void {
			_numPantallas = value;
		}
		/**
		 * Obtiene el numero asignado para el numero de pantallas que se deben mostrar.
		 */
		public function get numPantallas():uint {
			return _numPantallas;
		}
		/**
		 * Obtiene el indice actual usado en el paginado.
		 */
		public function get indexPaginado():int { return _indexPaginado; }
		
		/**
		 * Establece el indice de paginacion a usar.
		 */
		public function set indexPaginado(value:int):void {
			_indexPaginado = value;
		}
		
		/**
		 * Obtiene el numero de nodos utilizados por paginado.
		 */
		public function get numNodosPaginado():uint { return _numNodosPaginado; }
		
		/**
		 * Establece el numero de nodos a utilizar por paginado.
		 */
		public function set numNodosPaginado(value:uint):void {
			_numNodosPaginado = value;
			_paginado = new Array();
			for (var k:uint = _primerPaginado; k < _primerPaginado + _numNodosPaginado; k++ ){
				if (k < _numPantallas){
					_paginado.push(k);
				}else{
					_paginado.push("");
				}
				
			}
		}
		
		/**
		 * Devuelve <code>true</code> si el ZXML se ha cargado.
		 */
		public static function get cargado():Boolean { return _cargado; }

	}
}