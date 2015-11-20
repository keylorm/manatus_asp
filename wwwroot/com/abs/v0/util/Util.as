package com.abs.v0.util{

	//import fl.video.FLVPlayback;
	//import fl.video.*;
	import flash.display.DisplayObjectContainer;
	import flash.display.MorphShape;
	import flash.display.MovieClip;
	import flash.display.Stage;
	import flash.events.MouseEvent;
	import flash.geom.Point;
	import flash.net.URLRequest;
	import flash.net.navigateToURL;
	import flash.display.DisplayObject;
	import flash.display.Sprite;
	import flash.display.LoaderInfo;
	import flash.display.StageDisplayState;
	import flash.geom.ColorTransform;
	import flash.display.Bitmap;
	import flash.utils.Timer;
	import flash.events.TimerEvent;
	import fl.transitions.Tween;
	import fl.transitions.easing.*
	import fl.transitions.TweenEvent;
	import flash.filters.BitmapFilter;
    import flash.filters.BitmapFilterQuality;
	import flash.filters.GlowFilter;
	import flash.filters.DropShadowFilter;
	import flash.filters.BlurFilter;


	/**
	 * Clase con definicion de metodos est&aacute;ticos para ser usados f&aacute;cilmente.
	 */
	public class Util extends Sprite {
		
		private static var timer:Timer
		private static var my_mc:MovieClip;
		private static var reverza:Boolean;
		private static var contenedor_mc:MovieClip;
		public function Util() {

		}
		/**
		 * Obtiene una variable pasada por FlashVars desde HTML.
		 * @param	stage Intancia actual desde donde se llama al m&eacute;todo.
		 * @param	nombreFlashVar Nombre de la variable pasada desde HTML.
		 * @param	defaultValue Valor predeterminado si la variable viene con contenido <code>undefined</code>
		 * @return  El valor resultante contenido por <code>nombreFlashVar</code> o por defecto <code>defaultValue</code>.
		 */
		public static function getFlashVar(stage:DisplayObject,nombreFlashVar:String, defaultValue:String):String {
			var paramObj:Object = LoaderInfo(stage.root.loaderInfo).parameters;
			var value:String = String(paramObj[nombreFlashVar]);
			if (value.toString() == "undefined") {
				value = defaultValue;
			}
			return value;
		}
		/**
		 * Redirecciona la p&aacute;gina web donde se produjo la llamada hacia <code>url</code>, o en caso que
		 * el swf no se encuentre en una p&aacute;gina web, abrir&aacute; el explorador predeterminado.
		 * @param	url URL a la que se quiere navegar.
		 * @param	target Puede ser: <code>_self</code>, <code>_parent</code>, <code>_blank</code>, etc.
		 */
		public static function getURL(url:String,target:String="_self") {
			var request:URLRequest = new URLRequest(url);
			try {
				navigateToURL(request,target);// second argument is target
			} catch (e:Error) {
				throw new Error("Error al Intentar Navegar hacia " + url);
			}
		}
		/**
		 * Cambia el color a un <code>MovieClip</code>
		 * @param	target_mc <code>MovieClip</code> al que queremos cambiarle el color.
		 * @param	color Nuevo color.
		 * @example
		 * <code>
		 * import com.abs.v0.util.Util;<br />
		 * Util.cambiarColor(my_mc:MovieClip, 0xFFCC00);
		 * </code>
		 */
		public static function cambiarColor(target_mc:MovieClip, color:uint) {
			var newColorTransform:ColorTransform = target_mc.transform.colorTransform;
			newColorTransform.color = color;
			//newColorTransform.alphaMultiplier = .5;
			//newColorTransform.alphaOffset = 51;
			target_mc.transform.colorTransform = newColorTransform;

		}
		/**
		 * Retira el color que se haya agregado a un <code>MovieClip</code> por medio de <code>cambiarColor()</code>
		 * o por medio de un <code>ColorTransform</code>.
		 * @param	target_mc <code>MovieClip</code> a retirarle el color.
		 */
		public static function quitarColor(target_mc:MovieClip) {
			var newColorTransform:ColorTransform = new ColorTransform;
			target_mc.transform.colorTransform = newColorTransform;
		}
		
		/**
		 * Especialmente dise&ntilde;ado para imagenes cargadas por medio de <code>Loader</code> en un <code>MovieClip</code>,
		 * permite que la imagen se suavice y no se vea pixelada por efecto de la trama.
		 * @param	my_mc
		 * @param	value
		 */
		public static function suavizar(my_mc:MovieClip, value:Boolean=true){
			Bitmap(my_mc).smoothing = value;
		
		}
		//public static function smoothFLVPlayback(my_flvpb:FLVPlayback,value:Boolean) {
			//my_flvpb.getVideoPlayer(my_flvpb.activeVideoPlayerIndex).smoothing = value;
		//}
		
		/**
		 * Establece que el swf actual se vea en modo de pantalla completa.
		 * @param	stage <code>Stage</code> en contexto actual.
		 * @return  Valor <code>Boolean</code> que representa el estado actual de pantalla completa.
		 */
		public static function pantallaCompleta(stage:DisplayObject):Boolean{
			var enPantallaCompleta:Boolean = false;
			if (stage.root.stage.displayState != StageDisplayState.FULL_SCREEN) {
				stage.root.stage.displayState = StageDisplayState.FULL_SCREEN;
				enPantallaCompleta = true;
			} else {
				stage.root.stage.displayState = StageDisplayState.NORMAL;
			}
			return enPantallaCompleta;
		}
		
		/**
		 * Establece que un <code>MovieClip</code> se vea <i>borroso</i>.
		 * @param	my_mc <code>MovieClip</code> que se afectar&aacute;.
		 * @param	x Distancia en <code>x</code> del efecto.
		 * @param	y Distancia en <code>y</code> del efecto.
		 */
		public static function blur(my_mc:MovieClip, x:Number, y:Number){
			var blur:BlurFilter = new BlurFilter();
			blur.blurX = x;
			blur.blurY = y;
			blur.quality = BitmapFilterQuality.MEDIUM;
			my_mc.filters = [blur];
		}
		
		public static function glow(my_mc:MovieClip,blurX:Number=35,blurY:Number=35,color:Number=0xFFFFFF,alpha:Number=1,strength:Number = 5,inner:Boolean = false,knockout:Boolean = false,quality:Number = BitmapFilterQuality.HIGH) {
			var filter:BitmapFilter =  new GlowFilter(color, alpha, blurX, blurY, strength, quality, inner, knockout);
            var myFilters:Array = new Array();
            myFilters.push(filter);
            my_mc.filters = myFilters;
		}
		
		public static function shadow(my_mc:MovieClip,color:uint=0x000000,distancia:Number=5,angulo:Number=45) {
				var shadow:DropShadowFilter = new DropShadowFilter();
				shadow.color = color
				shadow.distance = distancia;
				shadow.angle = angulo;
				my_mc.filters = [shadow];
		}
		
		/**
		 * Crea una nueva referencia o referencia duplicada de un <code>MovieClip</code> pasado como par&aacute;metro.
		 * @param	target
		 * @return Nueva referencia del objeto a duplicar.
		 */
		public static function duplicateMovieClip(target:DisplayObject):DisplayObject{
			var targetClass:Class;
			targetClass = Object(target).constructor;
			var duplicado:DisplayObject = new targetClass();
			

			duplicado.transform = target.transform;
			duplicado.filters = target.filters;
			duplicado.cacheAsBitmap = target.cacheAsBitmap;
			duplicado.opaqueBackground = target.opaqueBackground;

			target.parent.addChild(duplicado);
			return duplicado;
		}
		
		
		public static function hacerEnChildren(my_mc:MovieClip){
			for(var k=0;k< my_mc.numChildren;k++){
				trace(my_mc.getChildAt(k).name)
			}
		}
		
		/**
		 * Por eliminar para crear class <code>Greybox</code>. Establece una pantalla gris en frente de todos los niveles o layers de flash
		 * y coloca un contenido dentro para visualizarlo fuera del contexto del escenario.
		 * @param	stage <code>Stage</code> en contexto actual.
		 * @param	objeto <code>MovieClip</code> a mostrar sobre la pantalla gris.
		 * @param	cerrarEncima Establece si se debe cerrar la pantalla al tocar <code>objeto</code>.
		 * @param	botonCerrar Si <code>cerrarEncima</code> es <code>true</code>, este es usado como boton para cerrar el contenido.
		 * @param	mascara &aacute;rea para cortar y centrar el <code>objeto</code>.
		 */
		public static function greybox(stage:DisplayObject,objeto:MovieClip,cerrarEncima:Boolean = true,botonCerrar:MovieClip = null, mascara:MovieClip = null) {
			var square:Sprite = new Sprite();
			contenedor_mc = new MovieClip();
			var pantallaGris:MovieClip = new MovieClip();
			var X:Number = stage.root.stage.x;
			var Y:Number = stage.root.stage.y;
			var W:Number = stage.root.stage.stageWidth;
			var H:Number = stage.root.stage.stageHeight;
			square.graphics.lineStyle(0,0x333333);
			square.graphics.beginFill(0x333333);
			square.graphics.drawRect(X,Y,W,H);
			square.graphics.endFill();
			square.x = 0
			square.y = 0
			pantallaGris.addChild(square)
			pantallaGris.alpha = .75
			pantallaGris.x = X;
			pantallaGris.y = Y;
			contenedor_mc.addChild(pantallaGris);
			if (cerrarEncima) {
				contenedor_mc.addEventListener(MouseEvent.CLICK, onClickCerrar)
			}else {
				pantallaGris.addEventListener(MouseEvent.CLICK, onClickCerrar)
				botonCerrar.addEventListener(MouseEvent.CLICK, onClickCerrar);
			}
			
			var mascara:MovieClip;
			
			contenedor_mc.addChild(objeto)
			contenedor_mc.tweenA = new Tween(contenedor_mc, "alpha", Strong.easeOut, 0, 1, 3, true);
			if (mascara != null) {
				objeto.x = (W - mascara.width) / 2
				objeto.y = (H - mascara.height) / 2
			}else{
				objeto.x = (W - objeto.width) / 2
				objeto.y = (H - objeto.height) / 2
			}
			//if(raiz){
				stage.root.stage.addChild(contenedor_mc);
			//}else {
				//stage.addChild(contenedor);
			//}
			
			contenedor_mc.buttonMode = true;
		
			
		}
		/**
		 * Obtiene la instancia del parent o child como <code>MovieClip</code> de estos anidados.
		 * @param	my_mc Generalemte <code>this</code> como punto de inicio.
		 * @param	level Niveles hacia arriba o abajo en la jerarqu&iacute;a.
		 * @return <code>MovieClip</code> con la referencia del parent o child resultante.
		 */
		public static function  getParent(my_mc:MovieClip, level:uint) :MovieClip{
			var my_parent:MovieClip = my_mc;
			for (var k = 0; k < level; k++ ) {
				my_parent = my_parent.parent as MovieClip;
			}
			return my_parent;
		}
		
		/**
		 * Obtiene la raiz del swf.
		 * @param	my_mc Referencia inicial. Generalmente <code>this</code>.
		 * @return El top o root como <code>MovieClip</code>.
		 */
		public static function getRoot(my_mc:MovieClip):MovieClip {
			return my_mc.parent.root as MovieClip;
		}
		
		private static function onClickCerrar(e:MouseEvent) {
			contenedor_mc.parent.removeChild(contenedor_mc.parent.getChildByName(contenedor_mc.name))
		}
		
		/**
		 * Lanza todos los childs dentro de un <code>MovieClip</code> deade la derecha hasta su posici&oacute;n inicial. Este debe tener
		 * todos sus childs de tipo <code>MovieClip</code>
		 * @param	my_mc <code>MovieClip</code> contenedor.
		 * @param	tiempo Tiempo de respuesta de la animaci&oacute;n.
		 * @param	reverza Si debe ejecutarse en orden inverso.
		 */
		public static function lanzar(my_mc:MovieClip, tiempo:Number = .1, reverza:Boolean = false) {
			Util.my_mc = my_mc;
			Util.reverza = reverza;
			
			
			for (var k=0; k< my_mc.numChildren; k++) {
				var child:MovieClip = my_mc.getChildAt(k) as MovieClip;
				child.visible = false;
			}
			try {
				timer.stop();
			}catch (err:Error) {
				
			}
			timer = new Timer(tiempo*1000);
			timer.addEventListener(TimerEvent.TIMER,lanzarTween);
			timer.start();
		}
		private static function lanzarTween(e:TimerEvent) {
			
			if (timer.currentCount <= my_mc.numChildren) {
				var child:MovieClip 
				if (reverza) {
					child = my_mc.getChildAt(my_mc.numChildren - (timer.currentCount ) ) as MovieClip;
				}else{
					child = my_mc.getChildAt(timer.currentCount -1 ) as MovieClip;
				}
				child.tweenX = new Tween(child,"x",Strong.easeOut,child.x + child.parent.stage.stageWidth,child.x,.5,true);
				child.visible = true;
			} else {
				timer.stop();
			}
		}
		
		/**
		 * Agrega espacios entre los caract&eacute;res de un <code>String</code>.
		 * @param	texto Texto a modificar.
		 * @return Texto con espacios.
		 * @example 
		 * <code>
		 * 		var res:String = Util.agregarEspacios("Mi texto");<br />
		 * 		trace(res);//salida: "M i  t e x t o"
		 * </code>
		 */
		public static function agregarEspacios(texto:String):String {
			var res:String;
			for (var k = 0; k < texto.length; k++ ) {
				res += texto.charAt(k) + " "
			
			}
			return res;
		}
		/**
		 * Espera un tiempo determinado y ejecuta <code>f</code>.
		 * @param	segundos Tiempo de espera o respuesta.
		 * @param	f <code>Function</code> con el c&oacute;digo necesario que
		 * se ejecutar&aacute; al finalizar el tiempo de espera.
		 */
		public static function esperar(segundos:Number, f:Function) {
			try {
				timer.stop();
			}catch (err:Error) {
			}
			timer = new Timer(segundos * 1000, 1);
			timer.addEventListener(TimerEvent.TIMER, onTimerEsperar);
			timer.start();
			function onTimerEsperar(e:TimerEvent) {
				if (f != null) {
					f();
				}
			}
			
		}
		
		/**
		 * Alinea dos <code>MovieClip</code> tomando como referencia a <code>estatico</code>.
		 * @param	estatico <code>MovieClip</code> al que se le alinear&aacute; <code>mover</code>.
		 * @param	mover <code>MovieClip</code> que se debe alinar a <code>estatico</code>.
		 * @param	X Indica si se debe alinear en el eje <code>x</code>.
		 * @param	Y Indica si se debe alinear en el eje <code>y</code>.
		 */
		public static function alinear(estatico:MovieClip, mover:MovieClip, X:Boolean=true, Y:Boolean=false) {
			if (X) {
				mover.x = estatico.x + ((estatico.width - mover.width) / 2);
			}
			if (Y) {
				mover.y = estatico.y + ((estatico.height - mover.height) / 2);
			}
			
			
		}
		
		/**
		 * Utilizado para sacar con respecto a al eje <code>x</code> y <code>x</code>
		 * el valor de las coordenadas que tiene el centro de un <code>MovieClip</code>.
		 * @param	my_mc <code>MovieClip</code> del cual queremos la coordenada central.
		 * @return <code>Point</code> con la coordenada.
		 */
		public static function getCentro(my_mc:MovieClip):Point {
			var punto:Point = new Point(my_mc.x + (my_mc.width/2) , my_mc.y + (my_mc.height/2));
			return punto;
		}
		
		/**
		 * Obtiene el nombre o referencia de la clase de la que proviene un <code>Object</code>.
		 * @param	o Cualquier objeto en la jerarqu&iacute;a.
		 * @return Un <code>Class</code> que se puede comparar con cualquier tipo de clase como <code>MovieClip</code>
		 */
		public static function obtenerClase(o:Object):Class { 
			var targetClass:Class;
			targetClass = Object(o).constructor;	
			return targetClass;
		}
		
	}
}

/*
this.root.loaderInfo.addEventListener(ProgressEvent.PROGRESS, onLoading);
this.root.loaderInfo.addEventListener(Event.COMPLETE, onComplete);

var p:Number = 0;
function onLoading(e:Event) {
	preloader_mc.rotor_mc.rotation+=10;
	//trace(p = e.target.bytesLoaded / e.target.bytesTotal);
	p_txt.text = Math.round(p*100).toString();
}
function onComplete(e:Event) {
	nextFrame();
}
 * */




