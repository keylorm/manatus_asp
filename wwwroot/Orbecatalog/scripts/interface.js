var flecha = ""
var panelBusqueda = ""

/*
Funcion para verificar el estdo de visibilidad del panel de busqueda, se revisando cada uno de los clicks
que se dan en la pantalla.
Inicio: Se capturan todos los clicks que se realizan
Resultado: Si el panel de busqueda esta block se esconde
*/

if (document.addEventListener)
{
    document.addEventListener('click',
    function (event) {
        // handle event here e.g.
        
        if((event.target.id  != flecha) && (panelBusqueda != ""))  {        
            if (panelBusqueda.style.display == 'block'){
                panelBusqueda.style.display = 'none';
	        }               
        }        
   },false);
}
else
    if (document.attachEvent) {
        document.attachEvent('onclick',
            function (event) {
                // handle event here e.g.
                try{                    
                    if((event.srcElement.id  != flecha) && (panelBusqueda != ""))  {        
                        if (panelBusqueda.style.display == 'block'){
                            panelBusqueda.style.display = 'none';
	                    }               
                    }
                }catch(Error){
                
                }
            }
        );
    }

function toggleColumnaDerecha(sender){
    var lockedElement = document.getElementById('Contenido');
    var toggleElement = document.getElementById('ColumnaDerecha');
    var parent = toggleElement.parentNode;
    alert('parent.style.width ' + parent.style.width);
    alert('toggleElement.clientWidth ' + toggleElement.clientWidth);
    
    var anchoPadre = 1024;
    
    if (toggleElement.style.display == 'none') {
        toggleElement.style.display = 'block';
		lockedElement.style.width = (anchoPadre - toggleElement.clientWidth) + 'px';
		sender.innerHTML = 'Ocultar Columna Derecha';
	} else {
		toggleElement.style.display = 'none';
		lockedElement.style.width = anchoPadre + 'px';
		sender.innerHTML = 'Ver Columna Derecha';
	}       
	return false;
}

function loadURLonIframe(sender, hostIframe_id, guestURL){
    if(guestURL != ""){
        var hostIframe = document.getElementById(hostIframe_id);
        if (hostIframe.src != undefined){
            hostIframe.src = guestURL;   
        }
        if (hostIframe.data != undefined){
            hostIframe.data = guestURL;   
        }
        return true;
    }else{
        return false;
    }
}



function verificarTab(sender, args){
    var funcionEvaluar = 'arrayFunciones[' + sender.get_activeTabIndex() + '];';    
    var algo= eval(funcionEvaluar);    
    eval(algo);
    return true;
}


/*
 elementBase_id: id del objeto que se quiere utilizar como base para colocar el elemento a mostrar
 elementToLink_id: Id del control que se quiere colocar
 elementOrigen_id: id del objeto que activo el evento
*/
function linkElementPosition(elementoBase_id, elementToLink_id, elementOrigen_id){
    var base = document.getElementById(elementoBase_id);
    var elementToLink = document.getElementById(elementToLink_id);
    var origen = document.getElementById(elementOrigen_id);
    flecha = origen.id //propio del proyecto
    
    var curleft = curtop = 0;  
    curtop = base.clientHeight;  
    if (base.offsetParent) {
        do {
			curleft += base.offsetLeft;
			curtop += base.offsetTop;
        } while (base = base.offsetParent);
        curleft -= 3;//Propio del proyecto para alinearlo
        elementToLink.style.top = curtop + 'px';
        elementToLink.style.left = curleft + 'px';
    }    
    toggleElement(elementToLink_id);
    return false;
}

function changeImage(elOriginal_id, elementToChange_id, none_id){
    var original = document.getElementById(elOriginal_id);
    var elementToChange = document.getElementById(elementToChange_id);
    var noneField = document.getElementById(none_id);
    elementToChange.src = original.src;
    
    var elCodigo = original.alt;
    elementToChange.alt = elCodigo;
    noneField.value = elCodigo;
    toggleElement('searchList');     
}

function toggleElement(element_id){
    var element = document.getElementById(element_id);
    panelBusqueda = element
    if (element.style.display == 'none'){
        element.style.display = 'block';
	} else {
		element.style.display = 'none';
	}
	return false;
}

function showPopUp(element_id){
    var element = $find(element_id);
    element.show();
}

function IAmSelected(source, eventArgs ) {
   var elValue = eventArgs.get_value();
   var elemento = document.getElementById(source.get_id());
   elemento.value = elValue;
}
