package util{
    import flash.display.Sprite; 
    import flash.filters.BitmapFilter; 
    import flash.filters.BitmapFilterQuality; 
    import flash.filters.DropShadowFilter; 
    //------------------------------------------ 
    public class Sombra extends Sprite{ 
       private var _ruta:Sprite; 
       private var _clip:Object; 
       private var _filtros:Array; 
       private var _sombra:BitmapFilter; 
       private var _color:Number; 
       private var _angulo:Number; 
       private var _alfa:Number; 
       private var _blurX:Number; 
       private var _blurY:Number; 
       private var _distancia:Number; 
       private var _strength:Number;
       private var _inner:Boolean; 
       private var _knockout:Boolean; 
       private var _calidad:Number; 
       //------------------------------------------ 
       public function Sombra(clip:Object){ 
          _ruta = clip.parent; 
          _clip = clip; 
          iniSombra(); 
          update(); 
       }
       //------------------------------------------
       private function iniSombra():void
       {
          _color = 0x000000;
          _angulo = 45;
          _alfa = 0.7;
          _blurX = 5;
          _blurY = 5;
          _distancia = 5;
          _strength = 0.65;
          _inner = false;
          _knockout = false;
          _calidad = BitmapFilterQuality.HIGH;
       }
       //------------------------------------------
       private function update():void{
          _filtros = new Array();
          _sombra = new DropShadowFilter(_distancia,_angulo,_color,_alfa,_blurX,_blurY,_strength,_calidad,_inner,_knockout);
          _filtros.push(_sombra);
          _clip.filters = _filtros;
       }
       //------------------------------------------
       public function set color(color:Number):void{
          _color = color;
          update();
       }
       //------------------------------------------
       public function set angulo(angulo:Number):void{
          _angulo = angulo;
          update();
       }
       //------------------------------------------
       public function set alfa(alfa:Number):void{
          _alfa = alfa;
          update();
       }
       //------------------------------------------
       public function set blurX(blurX:Number):void{
          _blurX = blurX;
          update();
       }
       //------------------------------------------

 
       public function set blurY(blurY:Number):void{
          _blurY = blurY;
          update();
       }
       //------------------------------------------
       public function set distancia(distancia:Number):void{
          _distancia = distancia;
          update();
       }
       //------------------------------------------
       public function set strength(strength:Number):void{
          _strength = strength;
          update();
       }
       //------------------------------------------
       public function set inner(inner:Boolean):void{
          _inner = inner;
          update();
       }
       //------------------------------------------
       public function set knockout(knockout:Boolean):void{
          _knockout = knockout;
          update();
       }
       //------------------------------------------
       public function set calidad(calidad:Number):void{
          _calidad = calidad;
          update();
       }
       //------------------------------------------
    }
 }

 
 
