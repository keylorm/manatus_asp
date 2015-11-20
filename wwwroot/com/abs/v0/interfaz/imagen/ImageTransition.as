package com.abs.v0.interfaz.imagen {
	
	import com.abs.v0.data.*
	import com.abs.v0.util.*
	import flash.events.ProgressEvent;
	//
	import fl.transitions.Tween;
	import fl.transitions.easing.*;
	import fl.transitions.TweenEvent;
	import flash.display.MovieClip;
	import flash.display.BlendMode;
	import flash.utils.Timer;
	import flash.events.TimerEvent;
	import flash.events.Event;
	
	/**
	 * Genera a partir de un MovieClip, un ZXML y un atributo,<br />
	 * una transicion simple de imagenes, con controles de pausa,<br />
	 * atras y adelante
	 */
	
	public class ImageTransition{
		private var _target:MovieClip;
		private var _zxml:ZXML;
		private var _ruta:String
		private var _mascara:MovieClip;
		public var arriba:MovieClip;
		public var abajo:MovieClip;
		private var mascara1:MovieClip;
		private var mascara2:MovieClip;
		private var tiempo:Number;
		public var timer:Timer;
		private var rutaImagen:String = "";
		private var rutaAnterior:String = "";
		private var atributo:String;
		
		private var _reverza:Boolean;
		private var _pausado:Boolean = false;
		
		private var _numFotos:uint;
		private var _onLoadXML:Function;
		
		/**
		 * Crea una nueva instancia de ImageTransition.
		 * @param	target MovieClip agregado al escenario con <code>alpha</code> 1, e intenrior con <code>alpha</code> 0.
		 * @param	rutaXML Ruta del XML fisico o generado por un servidor web.
		 * @param	atributo Nombre del atributo que contiene la ruta de la siguiente imagen on swf a cargar.
		 * @param	tiempo Tiempo entre transiciones, sin tomar en cuenta el tiempo de carga de cada imagen o swf.
		 * @param	escala Como en la class <code>Contenedor</code>, se usa para ajustar el contenido al <code>target</code>.
		 * @param	mascara MovieClip que debe tener las mismas dimensiones que <code>target</code>,<br />
		 * y es utilizado para crear una transicion con mascara.
		 */
		function ImageTransition(target:MovieClip, rutaXML:String, atributo:String ,tiempo:Number = 7,escala:String = "auto", mascara:MovieClip=null){
			this._target = target;
			this._rutaXML = rutaXML;	
			_mascara = mascara;
			this.atributo = atributo;
			this.tiempo = tiempo;
			timer =  new Timer(tiempo * 1000);
			_zxml = new ZXML(rutaXML, onXMLLoaded);
			arriba = Util.duplicateMovieClip(_target) as MovieClip;
			abajo = Util.duplicateMovieClip(_target) as MovieClip;
			mascara1 = Util.duplicateMovieClip(_target) as MovieClip;
			mascara2 = Util.duplicateMovieClip(_target) as MovieClip;
			abajo.x = 0;
			abajo.y = 0;
			arriba.x = 0;
			arriba.y = 0;
			
			mascara1.x = 0;
			mascara1.y = 0;
			mascara2.x = 0;
			mascara2.y = 0;
			
			if(_mascara != null){
				_target.addChild(_mascara)
				_mascara.visible = false;
				_mascara.stop();
				if (_mascara.totalFrames == 1) {
					for (var k = 0; k < _mascara.numChildren; k++ ) {
						MovieClip(_mascara.getChildAt(k)).stop();
					}
				}else {
					_mascara.stop();
				}
				arriba.il.onCompleteEvent(onCompleteArribaAnimarTransicion)
			}
			_target.addChild(abajo);
			_target.addChild(arriba);
			
			_target.addChild(mascara1);
			_target.addChild(mascara2);
			//
			abajo.il = new ImageLoader(abajo);
			//abajo.il.target = abajo;
			abajo.il.escala = escala;

			
			abajo.mask = mascara1;
			abajo.visible = false;
			abajo.blendMode = BlendMode.LAYER;
			mascara1.visible = false;
			//
			arriba.il = new ImageLoader(arriba);
			//arriba.il.target = arriba;
			//arriba.il.desvanecer(0,1,.5);
			arriba.il.escala = escala ;
			//
			if (escala != "nulo") {
				arriba.il.centrar();
				abajo.il.centrar();
			}
			arriba.mask = mascara2;
			arriba.visible = false;
			arriba.blendMode = BlendMode.LAYER;
			//arriba.il.onProgressEvent(onProgressArriba)
			


			mascara2.visible = false;
			timer.addEventListener(TimerEvent.TIMER, onTimer);
			
		}
		
		private function onCompleteArribaAnimarTransicion(e:Event) {
			arriba.mask = _mascara
				if (_mascara.totalFrames == 1) {
					for (var k = 0; k < _mascara.numChildren; k++ ) {
						MovieClip(_mascara.getChildAt(k)).play();
					}
				}else {
					_mascara.play();
				}
		}
		
		/**
		 * Metodo disparado cuando se carga el <code>ZXML</code> creado con <code>ruta</code>
		 * @event
		 * despues evento
		 * @param	value El <code>Function</code> con el codigo que debe suceder al dispararse el evento.
		 * @see ruta
		 */
		public function onLoadXML (value:Function):void {
			_onLoadXML = value;
		}
		
		private function onXMLLoaded() {
			this._numFotos = _zxml.numNodos;
			if(_onLoadXML != null){
				_onLoadXML();
			}
			trace(_zxml.numNodos);
			
			_zxml.getNextAttribute(atributo, -1);
			cargarImagen2();

		}
		private function onTimer(e:TimerEvent) {
			timer.stop();
			cargarImagen1();
		}
		
		private function cargarImagen1() {
			trace(rutaImagen);
			if (rutaImagen.indexOf(".swf") == -1) { 
				abajo.il.cargar(rutaImagen);
				abajo.il.onInitEvent(onCompleteiIl1);
			}else {
				cargarImagen2();
			}
		}
		private function onCompleteiIl1(e:Event) {
			cargarImagen2();
		}
		private function cargarImagen2() {
			var direccion:int = 1;
			if (_reverza) {
				direccion = -1;
			}
			rutaAnterior = rutaImagen;
			rutaImagen = _zxml.getNextAttribute(atributo,direccion);
			arriba.il.cargar(rutaImagen);
			arriba.il.onInitEvent(onCompleteiIl2);
			if(!_pausado){
				timer.start();
			}
		}
		private function onCompleteiIl2(e:Event) {
			arriba.visible = true;
			mascara2.visible = true;

			//mascara2.tweenW = new Tween(mascara2,"width",Strong.easeOut,0,mascara2_mc.width,3,true);
			//mascara2.tweenH = new Tween(mascara2,"height",Strong.easeOut,0,mascara2_mc.height,3,true);
			//mascara2.tweenH.addEventListener(TweenEvent.MOTION_FINISH,onMotionFinishMascara2);

			//arriba.blur = 0;
			//arriba.tweenB = new Tween(loader2_mc,"blur",Strong.easeOut,50,0,3,true);
			//arriba.tweenB.addEventListener(TweenEvent.MOTION_CHANGE,onMotionChangeMascara2);
			//
			//arriba.tweenW = new Tween(arriba,"width",Strong.easeOut,loader2_mc.width*2, loader2_mc.width,5,true);
			//arriba.tweenH = new Tween(arriba,"height",Strong.easeOut,loader2_mc.height*2, loader2_mc.height,5,true);
			//arriba.tweenX = new Tween(arriba,"x",Strong.easeOut,loader2_mc.x - 1000,loader2_mc.x,3,true);
			//arriba.tweenY = new Tween(arriba,"y",Strong.easeOut,loader2_mc.y - 600,loader2_mc.y,3,true);
			
			
			arriba.tweenA = new Tween(arriba,"alpha",Strong.easeOut,0,1,3,true);
			/**/
			/**/
			abajo.visible = true;
		}

		private function onMotionFinishMascara2(e:TweenEvent):void {
			abajo.visible = true;
		}

		private function onMotionChangeMascara2(e:TweenEvent):void {
			Util.blur(arriba,arriba.blur,0);
		}

		/**
		 * Inicia la secuencia de transicion si ha sido detenida previamente con <code>pausar()</code>.
		 * @see pausar
		 */
		public function iniciar() {
			_pausado = false;
			timer.start();
		
		}
		/**
		 * Pausa la secuencia de transicion si ha sido iniciada previamente con <code>iniciar()</code>.
		 * @see iniciar
		 */
		public function pausar() {
			_pausado = true;
			timer.stop();
		}
		/**
		 * Detiene la secuencia de transicion y pasa a la siguiente imagen o swf.
		 */
		public function siguiente() {
			_reverza = false;
			trace("timer detenido, se reestablece al llamar iniciar()")
			timer.stop();
			cargarImagen1();
			
		}
		/**
		 * Detiene la secuencia de transicion y pasa a la imagen o swf anterior.
		 */
		public function anterior() {
			_reverza = true;
			trace("timer detenido, se reestablece al llamar iniciar()")
			timer.stop();
			cargarImagen1();
		}
		
		public function get target():MovieClip { return _target; }
		
		public function set target(value:MovieClip):void {
			_target = value;
		}
		
		/**
		 * Obtiene el <code>ZXML</code> generado al enviar <code>ruta</code>.
		 */
		public function get zxml():ZXML { return _zxml; }
		
		/**
		 * Establece el <code>ZXML</code> de forma externa a <code>ImageTransition</code>.
		 */
		public function set zxml(value:ZXML):void {
			_zxml = value;
		}
		
		/**
		 * Obtiene la ruta provista para <code>zxml</code> de tipo <code>ZXML</code>
		 * 
		 */
		public function get ruta():String { return _ruta; }
		
		/**
		 * Establece la ruta provista para <code>zxml</code> de tipo <code>ZXML</code> generado
		 * al asignar este valor.
		 */
		public function set ruta(value:String):void {
			_ruta = value;
		}
		
		/**
		 * Si es <code>true</code> indica si la secuencia de transicion va en direccion opuesta.
		 */
		public function get reverza():Boolean { return _reverza; }
		
		/**
		 * Establece la direccion en que corre la secuencia de transicion, si se establece <code>false</code>
		 * avanza normalmente; si es <code>true</code> avanza hacia atras.
		 */
		public function set reverza(value:Boolean):void {
			_reverza = value;
		}
		
		/**
		 * Retorna el numero actual de nodos que se ulilizaran para la secuencia.
		 */
		public function get numFotos():uint { return _numFotos; }
		

	}
}