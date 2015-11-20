package com.abs.v0.interfaz.imagen{
	import com.abs.v0.constantes.Estado;
	import com.abs.v0.interfaz.Contenedor;
	//
	import flash.display.MovieClip;
	import flash.display.Loader;
	import flash.display.Bitmap;
	import flash.display.BitmapData;
	import flash.events.*;
	import flash.net.URLRequest;
	import fl.transitions.Tween;
	import fl.transitions.easing.*;
	/**
	 *<b>ImageLoader</b> es usada para cargar contenido externo de forma dinamica,
	 * principalmente imagenes, pero tambien soporta .SWFs
	 * */
	public class ImageLoader extends Contenedor{
		private var _widthCargado:Number;
		private var _heightCargado:Number;
		private var cargado:Boolean;
		/**
		 * Loader esta publica aún por que falta la definición
		 * de los eventos con EventDispatcher.<br />
		 * Será removida en proximas versiones
		 * */
		public var loader:Loader;
		private var _ruta:String
		//
		/**
		 * Crea un nuevo ImageLoader en base a un MovieClip en el stage.<br />
		 * @param target MovieClip contenedor.
		 * @example 
		 * <listing version="3.0">
		 * var il:ImageLoader = new ImageLoader(my_mc);
		 * </listing>
		 * */
		function ImageLoader(target:MovieClip) {
			init();
			super(target);

		}
		
		/**
		 * Inicializador de variables del constructor
		 * */
		private function init() {
			_widthCargado = 0;
			_heightCargado = 0;
			cargado = false;
			loader = new Loader();
			_ruta = "";
		}
		
		override public function set escala(value:String) :void {
			_escala = value;
			
			if (!cargado) {
				loader.contentLoaderInfo.addEventListener(Event.COMPLETE, onCompleteEscalar)
			}else {
				super.escala = _escala;
			}
		}
		
		private function onCompleteEscalar(e:Event) {
			super.escala = _escala;
		}
		
		
		override public function centrar() {
			if (!cargado) {
				loader.contentLoaderInfo.addEventListener(Event.COMPLETE, onCompleteCentrar)
			}else {
				super.centrar();
			}
		}
		
		private function onCompleteCentrar(e:Event) {
			super.centrar();
		}
		
		private function onComplete(e:Event) {
			trace("onComplete")
			Bitmap(loader.content).smoothing = true;
			doFade();
			cargado = true;		
		}

		public function cargar(path:String, encima:Boolean = false) {
			_ruta = path;

			alfaTween.stop();
			loader.contentLoaderInfo.addEventListener(Event.INIT,onCargar);
			var request:URLRequest=new URLRequest(_ruta);
			loader.load(request);
			if (!encima) {
				target_mc.addChild(loader);
			} else {
				var nLoader:Loader = new Loader();
				nLoader.contentLoaderInfo.addEventListener(Event.INIT, onCargar);
				nLoader.load(request);
				target_mc.addChildAt(nLoader,target_mc.numChildren);
			}

		}
		private function onCargar(e:Event) {
			
			_widthCargado = target_mc.width;
			_heightCargado = target_mc.height;
		}
		
		public function onProgressStart(value:Function){
			loader.contentLoaderInfo.addEventListener(Event.OPEN, value);
		}
		public function onProgressEvent(value:Function){
			loader.contentLoaderInfo.addEventListener(ProgressEvent.PROGRESS,value);
		}
		public function onCompleteEvent(value:Function){
			loader.contentLoaderInfo.addEventListener(Event.COMPLETE, value);
		}
		public function onInitEvent(value:Function){
			loader.contentLoaderInfo.addEventListener(Event.INIT, value);
		}

		public function get heightCargado():Number{
			return _heightCargado;			
		}
		public function get widthCargado():Number{
			return _widthCargado;
		}
		/**
		 * Obtiene la ruta de la imagen o swf que se ha cargado.
		 */
		public function get ruta():String { return _ruta; }
		
		/**
		 * Establece la imagen o swf a cargar.
		 */
		public function set ruta(value:String):void {
			_ruta = value;
			
			alfaTween.stop();
			loader.contentLoaderInfo.addEventListener(Event.INIT, onCargar);
			loader.contentLoaderInfo.addEventListener(Event.COMPLETE,onComplete);
			var request:URLRequest = new URLRequest(_ruta);
			loader.load(request);
			target_mc.removeChildAt(0)
			target_mc.addChild(loader);
		
		}

	}
}