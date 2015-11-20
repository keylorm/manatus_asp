package com.abs.v0.interfaz.menu {
	import com.abs.v0.data.ZXML;
	/**
	 * <p>Con base en los metodos basicos de paginaci&oacute;n en <code>ZXML</code>,<br />
	 * <code>Paginacion</code> obtiene  una pantalla  de <code>ZXML</code> <br /> 
	 * y sus respectivos &iacute;ndices. </p>
	 */
	public class Paginacion {
		private var _zxml:ZXML;
		private var _numNodosPantalla:uint;
		private var _f:Function;
		/**
		 * Crea una nueva instancia de <code>Paginacion</code>
		 * @param	ruta_xml <p>Establece la ruta f&iacute;sica del <code>ZXML</code> o la ruta generada por un servidor web. </p>
		 * @param	numNodosPantalla Equivale a establecer el n&uacute;mero de thumbs o botones de paginaci&oacute;n visibles en el control. 
		 */
		function Paginacion(ruta_xml:String,numNodosPantalla:uint) {
			_numNodosPantalla = numNodosPantalla;
			_zxml = new ZXML(ruta_xml,onLoad);
			
		}
		/**
		 * <code>Function</code> llamada al dispararse el evento. 
		 * @event
		 * @param	f <code>Function</code> con el c&oacute;digo que necesitamos que se ejecute al cargar el <code>ZXML</code>.
		 */
		public function onLoadXML(f:Function):void {
			_f = f;
		}
		
		private function onLoad():void {
			_zxml.numNodosPantalla = _numNodosPantalla;
			_f();
		}
		
		/**
		 * Avanza en la paginaci&oacute;n y actualiza los indices y la pantalla.
		 */
		public function adelante():void {
			_zxml.indexPantalla++;
		}
		/**
		 * Retrocede en la paginaci&oacute;n y actualiza los indices y la pantalla.
		 */
		public function atras():void {
			_zxml.indexPantalla--;
			
		}
		
		/**
		 * <p>Obtiene los &iacute;ndices de la pantalla actual, desde 0 hasta <code>numNodosPantalla</code>, </p>
		 * <p>que es par&aacute;metro establecido en el constructor. </p>
		 * @return <code>Array</code> con los &iacute;ndices actuales.
		 * @see function Paginacion(ruta_xml:String,numNodosPantalla:uint)
		 */
		public function indices():Array {
			var res:Array = new Array();
			var indice:int;
			for (var k:uint = 0; k < _numNodosPantalla; k++ ) {
				indice = _zxml.index + k + 1
				if (indice > _zxml.numNodos) {
					res.push("")
				}else{
					res.push(indice)
				}
			}
			return res;
		}
		
		/**
		 * Obtiene una pantalla actual de <code>ZXML</code>, con los valores del atributo enviado.<br />
		 * La llamada a este metodo se puede hacer m&aacute;s de una vez sin modificar los datos internamente.
		 * @param	atributo Nombre del atributo que se encuentra en cada nodo de lista simple.
		 * @return <code>Array</code> con los valores que tiene el atributo en la pantalla.
		 */
		public function pantalla(atributo:String):Array {
			return _zxml.getPantallaActual(atributo);
		}
		
		/**
		 * Obtiene la instancia de <code>ZXML</code> utilizada en <code>Paginacion</code>
		 */
		public function get zxml():ZXML { return _zxml; }
		
		/**
		 * Establece una nueva instancia de <code>ZXML</code> durante el tiempo de ejecuci&oacute;n.
		 */
		public function set zxml(value:ZXML):void {
			_zxml = value;
		}
		
	}
}