package com.abs.v0.interfaz.menu{
	import flash.display.MovieClip;
	import flash.events.MouseEvent;
	import com.abs.v0.util.Util;
	/**
	 * Facilita el trabajo repetitivo de trabajar con menus simples, que son aquellos
	 * que no tienen un menu desplegable, o subnodos, solo una lista de botones.<br />
	 * Estos menus son a base de una lista de <code>MovieClip</code>, ya que no se 
	 * podr&iacute;an acceder los objetos dentro de cada pieza del menu si fueran
	 * <code>SimpleButton</code>. De forma nativa los <code>SimpleButton</code> 
	 * limitan el <i>scope</i> a s&iacute; mismos.<br />
	 * Esta clase hereda de <code>Array</code>, por lo que mantiene sus m&eacute;todos,
	 * m&aacute;s los nuevos implementados.
	 * 
	 */
	dynamic public class MenuSimple extends Array {
		private var _modoBoton:Boolean = false;
		/**
		 * Utilizada para agregar una <code>Function</code> que se ejecutar&aacute;
		 * cuando se produzca el evento <code>MouseEvent.ROLL_OVER</code>
		 * 
		 * @param	f <code>Function</code> con el c&oacute;digo necesario para
		 * ejecutarse en el evento.
		 */
		public function agregarRollOver(f:Function):void {
			for (var k:uint=0; k<length; k++) {
				var boton:MovieClip = this[k] as MovieClip;
				boton.addEventListener(MouseEvent.ROLL_OVER,f);
			}
		}
		/**
		 * Utilizada para agregar una <code>Function</code> que se ejecutar&aacute;
		 * cuando se produzca el evento <code>MouseEvent.ROLL_OUT</code>
		 * 
		 * @param	f <code>Function</code> con el c&oacute;digo necesario para
		 * ejecutarse en el evento.
		 */
		public function agregarRollOut(f:Function):void  {
			for (var k:uint=0; k<length; k++) {
				var boton:MovieClip = this[k] as MovieClip;
				boton.addEventListener(MouseEvent.ROLL_OUT,f);
			}			
		}
		/**
		 * Utilizada para agregar una <code>Function</code> que se ejecutar&aacute;
		 * cuando se produzca el evento <code>MouseEvent.CLICK</code>
		 * 
		 * @param	f <code>Function</code> con el c&oacute;digo necesario para
		 * ejecutarse en el evento.
		 */		
		public function agregarClick(f:Function):void  {
			for (var k:uint=0; k<length; k++) {
				var boton:MovieClip = this[k] as MovieClip;
				boton.addEventListener(MouseEvent.CLICK,f);
			}		
		}
		
		
		override AS3 function push(... args):uint {
			for (var k:int = 0; k < args.length; k++ ) { 
				if (Util.obtenerClase(args[k]) == MovieClip) { 
					args[k].buttonMode = _modoBoton;
					args[k].indice = length;
					trace(args[k].indice)
				}else {
					throw new Error("El argumento '" + args[k] + "' debe ser MovieClip");
				}
				
			}
			return super.push(args)
		}
		
		/**
		 * Obtiene si se esta usando el <code>MovieClip</code> como <code>buttonMode = true</code>.<br />
		 * Cuando sea <code>true</code> se visualizar&aacute; un cursor <i>hand</i> sobre
		 * el <code>MovieClip</code>; en caso contrario, el cursor estandard de flecha.
		 */
		public function get modoBoton():Boolean { return _modoBoton; }
		
		/**
		 * Establece si se debe usar un cursor <i>hand</i> en lugar de un cursor
		 * de flecha para indicar que se trata de un boton.
		 */
		public function set modoBoton(value:Boolean):void {
			_modoBoton = value;
			for (var k:uint=0; k<length; k++) {
				var boton:MovieClip = this[k] as MovieClip;
				boton.buttonMode = value;
			}				
		}
		

	
	}
}