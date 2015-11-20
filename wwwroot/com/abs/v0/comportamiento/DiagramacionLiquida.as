package com.abs.v0.comportamiento {
	
	import flash.display.MovieClip;
	import flash.display.Stage;
	import flash.display.StageScaleMode;
	import flash.display.StageAlign
	import flash.events.Event;
	import fl.transitions.Tween;
	import fl.transitions.easing.*;
	/**
	 * <p>Clase que usada para proveer a la capacidad mantener a los objetos alineados en una posicion espec&iacute;fica, 
	 * esta posici&oacute;n est&aacute; basada en los estados internos de alineaci&oacute;n como por ejemplo 
	 * <code>DiagramacionLiquida.SUPERIOR_DERECHA</code>.</p>
	 */
	public class DiagramacionLiquida {
		/**
		 * 
		 */
		public static  const NULO:String = "nulo";
		/**
		 * 
		 */
		public static  const SUPERIOR:String = "superior";
		/**
		 * 
		 */
		public static  const INFERIOR:String = "inferior";
		/**
		 * 
		 */
		public static  const DERECHA:String = "derecha";
		/**
		 * 
		 */
		public static  const IZQUIERDA:String = "izquierda";
		/**
		 * 
		 */
		public static  const CENTRO:String = "centro";
		/**
		 * 
		 */
		public static const CENTRO_SUPERIOR:String = "centro_superior";
		/**
		 * 
		 */
		public static const CENTRO_INFERIOR:String = "centro_inferior";
		/**
		 * 
		 */
		public static  const SUPERIOR_DERECHA:String = "superior_derecha";
		/**
		 * 
		 */
		public static  const SUPERIOR_IZQUIERDA:String = "superior_izquierda";
		/**
		 * 
		 */
		public static  const INFERIOR_DERECHA:String = "inferior_derecha";
		/**
		 * 
		 */
		public static  const INFERIOR_IZQUIERDA:String = "inferior_izquierda";
		/**
		 * 
		 */
		public static  const FLOTAR:String = "flotar";
		/**
		 * 
		 */
		public static const VERTICAL:String = "vertical"
		/**
		 * 
		 */
		public static const HORIZONTAL:String = "horizontal"
		//
		public static const ANCHO:String = "ancho";
		/**
		 * 
		 */
		public static const ALTO:String = "alto";
		/**
		 * 
		 */
		public static const DIAGONAL:String = "diagonal";
		
		
		private var _target:MovieClip;
		private var _alineacion:String
		private var stage:Stage;
		
		private var _x:Number
		private var _y:Number
		private var proporcionX:Number;
		private var proporcionY:Number;
		private var tweenX:Tween;
		private var tweenY:Tween;
		private var _tiempo:Number;
		
		/**
		 * Crea una nueva instancia de <code>DiagramacionLiquida</code> la cual provee
		 * alineacion autom&aacute;tica de cualquier <code>MovieClip</code> que se necesite.
		 * @param	target_mc <code>MovieClip</code> que necesitamos alinear.
		 * @param	alineacion Tipo de alineaci&oacute;n definida por las propiedades est&aacute;ticas de la clase.
		 * @param	tiempo Tiempo de respuesta de alineaci&oacute;n. Si el tiempo es mayor, el <code>MovieClip</code> se
		 * desplazar&aacute; a travez de las coordenadas m&aacute;s lentamente.
		 */
		function DiagramacionLiquida(target_mc:MovieClip, alineacion:String = DiagramacionLiquida.NULO, tiempo:Number = 2 ) {
			_target = target_mc;
			_alineacion = alineacion;
			stage = target.root.stage
			this._tiempo = tiempo;
			

			tweenX = new Tween(target,"x",Strong.easeOut,target.x,target.x,_tiempo,true);
			tweenY = new Tween(target, "y", Strong.easeOut, target.y, target.y, _tiempo, true);
			
			if (stage.scaleMode != StageScaleMode.NO_SCALE){
				stage.scaleMode = StageScaleMode.NO_SCALE
			}
			if (stage.align != StageAlign.TOP_LEFT){
				stage.align = StageAlign.TOP_LEFT
			}
			//
			x = target.x;
			y = target.y;
			proporcionX = target.x / stage.stageWidth;
			proporcionY = target.y / stage.stageHeight;
			

			//
			stage.addEventListener(Event.ACTIVATE, onActivate);
			stage.addEventListener(Event.RESIZE, onResize);
		
		}
				
		private function onActivate(e:Event):void {
			trace("onActivate");
			alinear();
		}
		
		private function onResize(e:Event):void {
			trace("onResize");
			alinear();
		}
		
		/**
		 * Fuerza a <code>target</code> a realinearse si se ha cambiado <code>alineacion</code>.
		 */
		public function alinear():void {
			trace("***************Llamar desde propiedad alinear************")
			trace(alineacion);
			tweenX.stop();
			tweenY.stop();
			switch(alineacion){
				case SUPERIOR: superior(); break;
				case INFERIOR: inferior(); break;
				case DERECHA: derecha(); break;
				case IZQUIERDA: izquierda(); break;
				case CENTRO: centro(); break;
				case CENTRO_SUPERIOR: centroSuperior(); break;
				case SUPERIOR_DERECHA: superiorDerecha(); break;
				case SUPERIOR_IZQUIERDA: superiorIzquierda(); break;
				case INFERIOR_DERECHA: inferiorDerecha(); break;
				case INFERIOR_IZQUIERDA: inferiorIzquierda(); break;
				case FLOTAR: flotar(); break;
				case VERTICAL: vertical(); break;
				case HORIZONTAL: horizontal(); break;
			}
		}
		
		private function superior():void{
			//target.y = 0;
			tweenY = new Tween(target, "y", Strong.easeOut, target.y, 0, _tiempo, true);
		}
		
		private function inferior():void{
			//target.y = stage.stageHeight - target.height;
			tweenY = new Tween(target, "y", Strong.easeOut, target.y, stage.stageHeight - target.height, _tiempo, true);
		}
		
		private function derecha():void{
			//target.x = stage.stageWidth - target.width;
			tweenX = new Tween(target, "x", Strong.easeOut, target.x, stage.stageWidth - target.width, _tiempo, true);
		}
		
		private function izquierda():void{
			//target.x = 0
			tweenX = new Tween(target, "x", Strong.easeOut, target.x, 0, _tiempo, true);
		}

		private function vertical():void {
			var pY:Number = (stage.stageHeight - target.height)/2
			tweenY = new Tween(target, "y", Strong.easeOut, target.y, pY, _tiempo, true);
		}
		private function horizontal():void {
			var pX:Number = (stage.stageWidth - target.width) / 2
			tweenX = new Tween(target, "x", Strong.easeOut, target.x, pX, _tiempo, true);
		}
		private function centro():void{
			vertical();
			horizontal();
		}
		
		private function centroSuperior():void {
			var pX:Number = (stage.stageWidth - target.width)/2
			tweenX = new Tween(target, "x", Strong.easeOut, target.x, pX, _tiempo, true);
			superior();
		}
		
		private function superiorDerecha():void{
			superior();
			derecha();
		}
		
		private function superiorIzquierda():void{
			superior();
			izquierda();

		}
		
		private function inferiorDerecha():void{
			inferior();
			derecha();
		}
		
		private function inferiorIzquierda():void{
			inferior();
			izquierda();
		}
		
		private function flotar():void{
			tweenX = new Tween(target, "x", Strong.easeOut, target.x, proporcionX * stage.stageWidth, _tiempo, true);
			tweenY = new Tween(target, "y", Strong.easeOut, target.y, proporcionY * stage.stageHeight, _tiempo, true);
		}
		
		/**
		 * Obtiene la referencia del <code>MovieClip</code> que se est&aacute; alineando por esta instancia de <code>DiagramacionLiquida</code>
		 */
		public function get target():MovieClip {
			return _target; 
		}
		/**
		 * Establece la referencia del <code>MovieClip</code> que se debe alinear.
		 */
		public function set target(value:MovieClip):void {
			_target = value;
		}
		
		/**
		 * Obtiene la alineaci&oacute;n actual utilizada por la instancia.
		 */
		public function get alineacion():String {
			return _alineacion; 
		}
		/**
		 * Establece una nueva alineaci&oacute;n.
		 */
		public function set alineacion(value:String):void {
			_alineacion = value;
		}
		/**
		 * Obtiene la posicion original en <code>y</code> que ten&iacute;a <code>target</code> al ser asignado
		 * a la instancia de <code>DiagramcionLiquida</code>, a menos que durante la ejecuci&oacute;n se le 
		 * haya asignado uno nuevo.
		 */
		public function get y():Number { return _y; }
		/**
		 * Establece una nueva posicion de origen  <code>y</code> para <code>target</code>.
		 */
		public function set y(value:Number):void {
			_y = value;
		}
		/**
		 * Obtiene la posicion original en <code>x</code> que ten&iacute;a <code>target</code> al ser asignado
		 * a la instancia de <code>DiagramcionLiquida</code>, a menos que durante la ejecuci&oacute;n se le 
		 * haya asignado uno nuevo.
		 */		
		public function get x():Number { return _x; }
		/**
		 * Establece una nueva posicion de origen  <code>x</code> para <code>target</code>.
		 */		
		public function set x(value:Number):void {
			_x = value;
		}
		/**
		 * Obtiene el tiempo de respuesta actual utilizado por la instancia
		 * para reacomodar en el plano a <code>target</code>
		 */
		public function get tiempo():Number { return _tiempo; }
		/**
		 * Establece el tiempo de respuesta que debe ser utilizado para
		 * alinear a <code>target</code>.
		 */
		public function set tiempo(value:Number):void {
			_tiempo = value;
		}
	}


}




