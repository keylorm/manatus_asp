/**
 * @author Sebastian Sanabria Diaz
 * @version 0
 */

package com.abs.v0.interfaz{
	import flash.display.DisplayObject;
	import flash.display.Loader;
	import flash.display.MovieClip;
	import flash.display.SpreadMethod;
	import flash.display.Sprite;
	import flash.events.MouseEvent;
	import flash.events.Event;
	import fl.transitions.Tween;
	import fl.transitions.TweenEvent;
	import fl.transitions.easing.*;
	import flash.geom.Point;
	import flash.text.TextField;
	import flash.text.TextFormat;
	import com.abs.v0.constantes.Estado;
	import com.abs.v0.util.Util;
	/**
	 * Un Contenedor es un <i>tipo de dato</i> que permite la manipulacion de un MovieClip
	 * dandole caracteristicas especiales, como tener la capacidad de recordar su posicion y medidas
	 * originales cuando este fue invocado; ajustarse de forma forzada o automatica dentro de las mismas
	 * medidas del contenedor; y metodos para visualizar el contenido que ha quedado fuera de los
	 * limites del contenedor.
	 */
	
	
	public class Contenedor{
		protected var target_mc:MovieClip;
		private var _x:Number;
		private var _y:Number;
		private var _width:Number;
		private var _height:Number;
		//
		private var _modoBoton:Boolean;
		//
		private var centrarVertical:Boolean;
		private var centrarHorizontal:Boolean;
		protected var _escala:String;
		//
				
		private var _fade:Boolean;
		private var alfaInicio:Number;
		private var alfaFinal:Number;
		private var duracionSegundos:Number;
		protected var _alfaTween:Tween;
		
		//
		/**
		 * Se llama cuando se necesita asignar un nuevo Contenedor.
		 * @param	target MovieClip al que necesitamos darle capacidades de contenedor.
		 * @example
		 * <listing version="3.0">
		 * var c:Contenedor = new Contenedor(my_square_mc);
		 * </listing>
		 */
		function Contenedor(target:MovieClip) {
			init();
			this.target = target;
			
		}
		
		
		private function init():void {
			_x = 0;
			_y = 0;
			_width = 0;
			_height = 0;
			_modoBoton = false;
			centrarVertical = true;
			centrarHorizontal = true;
			_escala = Estado.NULO;
			_fade = false;
			alfaInicio = 0;
			alfaFinal = 1;
			duracionSegundos = 2;
		}
		
		/**
		 * Indica si se puede arrastrar <code>target</code> con el mouse.
		 * @param	ejecutar  <code>false</code>
		 */
		public function grab(ejecutar:Boolean=true):void {
			if (ejecutar) {
				this.target.addEventListener(MouseEvent.MOUSE_MOVE,onGrabMove);
				this.target.addEventListener(MouseEvent.MOUSE_UP,onGrabUp);
				this.target.addEventListener(MouseEvent.MOUSE_DOWN,onGrabDown);
				this.target.addEventListener(MouseEvent.MOUSE_OUT,onGrabUp);
			}
		}
		private function onGrabMove(e:Event) {
			target_mc.useHandCursor = true;
			var enX:Boolean = (target_mc.parent.mouseX > this._x) && (target_mc.parent.mouseX  < (this._x + this._width));
			var enY:Boolean = (target_mc.parent.mouseY > this._y) && (target_mc.parent.mouseY < (this._y + this._height));
			if (!(enY && enX)) {
				target_mc.stopDrag();
				target_mc.buttonMode = false;
			} else {
				//mostrar mano
				if (!target_mc.buttonMode) {
					target_mc.buttonMode = true;
				}
			}
			if (target_mc.x > this._x) {
				target_mc.x = this._x;
			}
			if (target_mc.y > this._y) {
				target_mc.y = this._y;
			}
			if ((target_mc.x + target_mc.width) < (this._x + this._width)) {
				target_mc.x = _x - (target_mc.width - this._width);
			}
			if ((target_mc.y + target_mc.height) < (this._y + this._height)) {
				target_mc.y = _y - (target_mc.height - this._height);
			}

		}
		private function onGrabUp(e:Event) {
			target_mc.stopDrag();

		}
		private function onGrabDown(e:Event) {
			var enX:Boolean = (target_mc.parent.mouseX > this._x) && (target_mc.parent.mouseX  < (this._x + this._width));
			var enY:Boolean = (target_mc.parent.mouseY > this._y) && (target_mc.parent.mouseY < (this._y + this._height));
			if (enY && enX) {
				target_mc.startDrag();
			}
		}
		//
		protected function doFade(){
			if (this._fade) {
				_alfaTween = new Tween(target_mc,"alpha", Strong.easeIn,this.alfaInicio,this.alfaFinal,this.duracionSegundos,true);
			}
		}
		/**
		 * Indica si al inicializarse el Contenedor debe aparecer con un fade de <code>alpha</code>
		 * @param	alfaInicio Valor inicial del fade.
		 * @param	alfaFinal Valor final del fade.
		 * @param	duracionSegundos Tiempo que dura el fade.
		 */
		public function desvanecer(alfaInicio:Number=0,alfaFinal:Number=1,duracionSegundos:Number=2) {
			this._fade = true;
			this.alfaInicio = alfaInicio;
			this.alfaFinal = alfaFinal;
			this.duracionSegundos = duracionSegundos;
		}
		
		/**
		 * Si al Contenedor se le han declarado medidas menores o mayores que
		 * las que tiene <code>target</code>, se puede llamar para centrarlo.
		 */
		public function centrar(){
			if (centrarHorizontal) {
				target_mc.x =  _x + ((_width  - target_mc.width ) / 2);
			}
			if (centrarVertical) {
				target_mc.y = _y + ((_height - target_mc.height ) / 2);
			}
		}
		
		private function doCentrar(e:Event) {
			
		}
		
		private function escalarAncho() {
			trace("escalarAncho")
			var proporcion:Number = target_mc.width/_width
			target_mc.width /= proporcion;
			target_mc.height /= proporcion;		
			//trace(proporcion)
		}
		
		private function escalarAlto() {
			trace("escalarAlto")
			var proporcion:Number = target_mc.height / _height
			target_mc.width /= proporcion;
			target_mc.height /= proporcion;
			//trace(proporcion)

		}
		
		private function escalarAuto3() {
			var radioContenedor:Number = _width / _height;
			var radioTarget:Number = target_mc.width / target_mc.height
			trace(radioContenedor , radioTarget);
			trace(radioTarget ,radioContenedor);
			var radio  = radioContenedor / radioTarget;
			
			if (radio > 1) {
				escalarAncho();
			}else {
				escalarAlto();
			}
		}
		
		private function escalarAuto2() {
			var radioContenedor:Number = _width / _height;
			var radioTarget:Number = target_mc.width / target_mc.height
			//
			var anchoTargetMayor:Boolean = _width < target_mc.width
			var altoTargetMayor:Boolean = _height < target_mc.height
			var anchoTargetIgual:Boolean = _width == target_mc.width
			var altoTargetIgual:Boolean = _height == target_mc.height
			
			//if (anchoTargetIgual && altoTargetIgual) {
				//trace()
				//
			//}
			
			if (anchoTargetIgual) {
				trace("anchoTargetIgual")
				escalarAlto()
			}else if (altoTargetIgual) {
				trace("altoTargetIgual")
				escalarAncho();
			}
		}
		
		
		
		private function escalarAuto() {
			var radioContenedor:Number = _width / _height;
			var radioTarget:Number = target_mc.width / target_mc.height
			//
			var anchoTargetMayor:Boolean = _width < target_mc.width
			var altoTargetMayor:Boolean = _height < target_mc.height
			var anchoTargetIgual:Boolean = _width == target_mc.width
			var altoTargetIgual:Boolean = _height == target_mc.height
			
			//trace(radioContenedor, radioTarget);
			//trace(anchoTargetMayor, altoTargetMayor);
			//trace(anchoTargetIgual, altoTargetIgual);
			
			//if (anchoTargetIgual) {
				//escalarAlto();
			//}
			//if (altoTargetIgual) {
				//escalarAncho();
			//}

			var proporcion:Number = 0;
			if (radioContenedor > 1) {
				trace("Contenedor más ancho")
				if (radioTarget > 1) {
					trace("Target más ancho")
					if (anchoTargetIgual) {
						trace("Anchos Iguales17")
						//escalarAlto();
						//escalarAncho();
					}else if (anchoTargetMayor) {
						trace("Target más ancho que Contenedor")
						if (altoTargetIgual) {
							trace("AltosIguales18")
							//escalarAncho();
							//escalarAlto();
						}else if (altoTargetMayor) {
							trace("Target más alto que Contenedor1")
							escalarAncho()
						}else {
							trace("Target menos alto que Contenedor2")
							escalarAlto()
						}
					}else {
						trace("Target menos ancho que Contenedor") 
						if (altoTargetIgual) {
							trace("AltosIguales19")
							//escalarAncho();
						}else if (altoTargetMayor) {
							trace("Target más alto que Contenedor3")
							escalarAncho()
						}else {
							trace("Target menos alto que Contenedor4")
							escalarAncho()
						}					
					}
				}else if (radioTarget < 1){
					trace("Target más alto")
					if (anchoTargetIgual) {
						trace("Anchos Iguales20")
						//escalarAlto()
					}else if (anchoTargetMayor) {
						trace("Target más ancho que Contenedor")
						if (altoTargetIgual) {
							//trace("AltosIguales")
						}else if (altoTargetMayor) {
							trace("Target más alto que Contenedor5")
							escalarAncho()
						}else {
							trace("Target menos alto que Contenedor6")
						}
					}else {
						trace("Target menos ancho que Contenedor")
						if (altoTargetIgual) {
							trace("AltosIguales21")
							//escalarAncho();
						}else if (altoTargetMayor) {
							trace("Target más alto que Contenedor7")
							escalarAncho()					
							
						}else {
							trace("Target menos alto que Contenedor8")
							escalarAncho();
						}
					}
				}else {
					trace("Target Simétrico")
				}
			}else if(radioContenedor < 1){///***
				trace("Contenedor más alto")
				if (radioTarget > 1) {
					trace("Target más ancho")
					if (anchoTargetIgual) {
						trace("Anchos Iguales22")
						//escalarAlto();
					}else if (anchoTargetMayor) {
						trace("Target más ancho que Contenedor")
						if (altoTargetIgual) {
							trace("AltosIguales23")
							//escalarAncho();
						}else if (altoTargetMayor) {
							trace("Target más alto que Contenedor9")
							escalarAlto();
						}else {
							trace("Target menos alto que Contenedor10")
							escalarAlto();
						}
						
					}else {
						trace("Target menos ancho que Contenedor")
						if (altoTargetIgual) {
							trace("AltosIguales24")
							//escalarAncho();
						}else if (altoTargetMayor) {
							trace("Target más alto que Contenedor11")
						}else {
							trace("Target menos alto que Contenedor12")
							escalarAlto();
						}					
					}
				}else if (radioTarget < 1){
					trace("Target más alto")
					if (anchoTargetIgual) {
						trace("Anchos Iguales25")
						//escalarAlto();
					}else if (anchoTargetMayor) {
						trace("Target más ancho que Contenedor")
						if (altoTargetIgual) {
							trace("AltosIguales26")
							//escalarAncho();
						}else if (altoTargetMayor) {
							trace("Target más alto que Contenedor13")
							escalarAlto();
						}else {
							trace("Target menos alto que Contenedor14")
							escalarAlto();
						}
					}else {
						trace("Target menos ancho que Contenedor")
						if (altoTargetIgual) {
							trace("AltosIguales27")
							//escalarAncho();
						}else if (altoTargetMayor) {
							trace("Target más alto que Contenedor15")
							escalarAncho();
						}else {
							trace("Target menos alto que Contenedor16")
							escalarAlto();
						}
					}
				}else {
					trace("Target Simétrico")
				}
			}else {
				trace("Contenedor Simétrico")
			}
		}
		
		/**
		 * Obtiene el valor actual de la escala.
		 */
		public function get escala():String { return _escala; }
		
		/**
		 * Obtiene o establece la escala que debe tener Contenedor.
		 * @default Estado.NULO;
		 */
		public function set escala(value:String):void {
			_escala = value;
			if (_escala == Estado.AUTO) {
				escalarAuto3();
			}
			if (_escala == Estado.ANCHO) {
				escalarAncho();
			}
			if (_escala == Estado.ALTO) {
				escalarAlto();
			}
		}	
		
		
		/**
		 * Permite el autoscroll con solo pasar el mouse por el contenedor.
		 * @param	ejecutar Debe cambiarse para futuras versiones que sea Getter y Setter
		 */
		public function scroll(ejecutar:Boolean=true) {
			if (ejecutar) {
				this.target.addEventListener(MouseEvent.MOUSE_MOVE,onScroll);
			}
		}
		/**
		 * Permite poner un mensaje en frente del contenedor.
		 * @param	caption Mensaje a mostrar.
		 * @param	duracion Tiempo que se debe ver el mensaje.
		 */
		public function scrollMessage(caption:String, duracion:Number = 3) {
			var cuadro:Sprite = new Sprite();
			var tf:TextField = new TextField();
			var texto_mc:MovieClip = new MovieClip();
			var tip_mc:MovieClip = new MovieClip();
			cuadro.graphics.lineStyle(0, 0x333333);
			cuadro.graphics.beginFill(0, 1);
			
			var format:TextFormat = new TextFormat()
			tf.textColor = 0xFFFFFF
			tf.setTextFormat(format)

			tf.text = caption
			tf.selectable = false;
			

			
			
			
			
			
			//Util.cambiarColor(texto_mc, 0xFFFFFF);
			
			//texto_mc.addChild(tf);
			
			
			cuadro.graphics.drawRect(0, 0, tf.textWidth + 5, tf.textHeight + 5);
			cuadro.graphics.endFill();
			
			tip_mc.addChild(cuadro)
			tip_mc.addChild(tf);
			
			Util.alinear(target_mc.parent.root as MovieClip, tip_mc,true,true);
			Util.getRoot(target_mc).addChild(tip_mc);
			
			tip_mc.tweenA = new Tween(tip_mc, "alpha", Strong.easeOut, 0, 1, duracion, true);
			tip_mc.tweenA.addEventListener(TweenEvent.MOTION_FINISH,onFinishTweenATip)
			function onFinishTweenATip(e:TweenEvent) {
				tip_mc.tweenA = new Tween(tip_mc, "alpha", Strong.easeOut, 1, 0, duracion, true);
			}
		}

		private function onScroll(e:MouseEvent) {
			var target:MovieClip = e.currentTarget as MovieClip;
			var enX:Boolean = (target.parent.mouseX > this._x) && (target.parent.mouseX < (this._x + this._width));
			var enY:Boolean = (target.parent.mouseY > this._y) && (target.parent.mouseY < (this._y + this._height));
			//var tX,tY:Tween;
			if (enX && enY) {
				if (_modoBoton) {
					target_mc.buttonMode = true;
				}
				var px = (target.parent.mouseX - this._x) / this._width;
				var py = (target.parent.mouseY - this._y) / this._height;
				var nx = this._x - (px * ((target.width - this._width - 1)));
				var ny = this._y - (py * ((target.height - this._height - 1)));
				///
				target.tweenX = new Tween(target, "x", Strong.easeOut, target.x, nx, 1, true);
				target.tweenY = new Tween(target, "y", Strong.easeOut, target.y, ny, 1, true);
				///
			} else {
				if (_modoBoton) {
					target_mc.buttonMode = false;
				}
				
			}
		}
		//
		
		/**
		 * Establece el <code>target</code> a usarse de tipo MovieClip.
		 *
		 */
		public function set target(value:MovieClip) {
			this._x=value.x;
			this._y=value.y;
			this._width=value.width;
			this._height=value.height;
			this.target_mc = value;
			_alfaTween = new Tween(target_mc,"alpha",Strong.easeOut,target.alpha,target.alpha,this.duracionSegundos,true);
		}
		/**
		 * Obtiene el <code>target</code> a usarse de tipo MovieClip.
		 *
		 */
		public function get target():MovieClip {
			return this.target_mc;
		}
		/**
		 * Establece el nuevo valor de <code>x</code> para Contenedor.<br />
		 * De forma predeterminada al crear un Contenedor,
		 * <code>x</code> tiene el mismo valor que <code>target.x</code>.
		 */
		public function set x(value:Number) {
			this._x=value;
		}
		/**
		 * Obtiene el valor de <code>x</code> para Contenedor.
		 */
		public function get x():Number {
			return this._x;
		}
		/**
		 * Establece el nuevo valor de <code>y</code> para Contenedor.<br />
		 * De forma predeterminada al crear un Contenedor,
		 * <code>y</code> tiene el mismo valor que <code>target.y</code>.
		 */
		public function set y(value:Number) {
			this._y = value;
		}
		/**
		 * Obtiene el valor de <code>y</code> para el Contenedor.
		 */
		public function get y():Number {
			return this._y;
		}
		/**
		 * Establece el nuevo valor de <code>width</code> para Contenedor.<br />
		 * De forma predeterminada al crear un Contenedor,
		 * <code>width</code> tiene el mismo valor que <code>target.width</code>.
		 */
		public function set width(value:Number) {
			this._width=value;
			//if(escalar != Estado.NULO){
				//var e:Event = new Event(Event.CHANGE);
				//onCentrar(e);
			//}
			escala = _escala
		}
		/**
		 * Obtiene el valor de <code>width</code> para el Contenedor.
		 */
		public function get width():Number {
			return this._width;
		}
		/**
		 * Establece el nuevo valor de <code>height</code> para Contenedor.<br />
		 * De forma predeterminada al crear un Contenedor,
		 * <code>height</code> tiene el mismo valor que <code>target.height</code>.
		 */
		public function set height(value:Number) {
			this._height = value;
			//if(escalar != Estado.NULO){
				//var e:Event = new Event(Event.CHANGE);
				//onCentrar(e);
			//}
			escala = _escala
		}
		/**
		 * Obtiene el valor de <code>height</code> para el Contenedor.
		 */
		public function get height():Number {
			return this._height;
		}
		
		/**
		 * Establece si se debe mostrar el cursor como <code>hand</code> en el evento
		 * <code>MOUSE_OVER</code>.
		 */
		public function set modoBoton(value:Boolean):void {
			target_mc.buttonMode = value;
			_modoBoton = value;
		}		
		/**
		 * Devuelve <code>true</code> si el Contenedor esta mostrando el cursor como <i>hand</i>.
		 */
		public function get modoBoton():Boolean { return _modoBoton; }

		/**
		 * Obtiene el <code>Tween</code> usado para el metodo desvanecer().
		 * @see desvanecer
		 */
		public function get alfaTween():Tween { return _alfaTween; }
			
	}
}