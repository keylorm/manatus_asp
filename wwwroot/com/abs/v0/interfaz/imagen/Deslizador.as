package com.abs.v0.interfaz.imagen{
	import flash.display.MovieClip;
	public class Deslizador{
		public static const DERECHA:String = "derecha";
		public static const IZQUIERDA:String = "izquierda";
		public static const ARRIBA:String = "arriba";
		public static const ABAJO:String = "abajo";
		
		
		
		private var arriba_il:ImageLoader;
		private var abajo_il:ImageLoader;
		private var my_xml:ZXML;
		private var nomAtributoMedia:String;
		private var nomAtributoURL:String;
		private var _direccion:String;
		
		//private var ruta_xml:String;
		function Deslizador(ruta_xml:String, nomAtributoMedia:String, nomAtributoURL:String,loader_abajo_mc:MovieClip,loader_arriba_mc:MovieClip){
			this.nomAtributoMedia = nomAtributoMedia;
			this.nomAtributoURL = nomAtributoURL;
			my_xml = new ZXML(ruta_xml, onLoadXML);
			arriba = new ImageLoader()
			arriba.target = loader_arriba_mc;
			abajo.target = loader_abajo_mc;
			
		}
		private function onLoadXML(){
			//abajo.cargar(my_xml.firstChild.childNodes[0].attributes[nomAtributoMedia])
			trace(my_xml.firstChild.childNodes[0].attributes[nomAtributoMedia])
		}
		public function siguiente(){
		
		}
		public function anterior(){
			
		}
		
		public function get arriba():ImageLoader { return arriba_il; }
		
		public function set arriba(value:ImageLoader):void {
			arriba_il = value;
		}
		
		public function get abajo():ImageLoader { return abajo_il; }
		
		public function set abajo(value:ImageLoader):void {
			abajo_il = value;
		}
		
		public function get direccion():String { return _direccion; }
		
		public function set direccion(value:String):void 
		{
			_direccion = value;
		}
	}
}