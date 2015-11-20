// Orbelink JScript Events

var theProgressDiv = "progressBar";


/** Sobre Navegacion **/
function getURL(theUrl){
    window.location = theUrl;
}


/** Pone a la fila el CSS de "Over"
 * param:   sender (objeto):        Elemento que llamo al evento.
 * param:   indice (int):           Indice de la fila en la tabla.
 **/
function setRowFocus(sender, indice){
    sender.className = "tablaResultados_Over";
    sender.style.cursor='hand';
}


/** Quita a la fila el CSS de "Over"
 * param:   sender (objeto):        Elemento que llamo al evento.
 * param:   indice (int):           Indice de la fila en la tabla.
 * param:   selected (bool):        Si originalmente estaba seleccionado.
 **/
function unsetRowFocus(sender, indice){
    if (arguments[2] == true){
        sender.className = "tablaResultados_Selected";
    } else {
        if (indice % 2 == 0){
            sender.className = "tablaResultados_Item";
        } else {
            sender.className = "tablaResultados_Alternate";
        }
    }
}


/** Muestra el objeto con la propiedad del display
 * param:   theObject (objeto):        Elemento a mostrar.
 **/
function showObject(theObject){
    if (theObject != null){
        if (theObject.style != null && theObject.style.display != null){
	        theObject.style.display = "block";
	    }
    }
}


/** Oculta el objeto con la propiedad del display
 * param:   theObject (objeto):        Elemento a mostrar.
 **/
function hideObject(theObject){
    if (theObject != null){
        if (theObject.style != null && theObject.style.display != null){
	        theObject.style.display = "none";
	    }
    }
}


/** Aplica el color del borde del control
 * param:   sender (objeto):        Elemento que llamo al evento.
 **/
function setFocus(sender){
    sender.style.borderColor = "blue";
    showTextProgress(sender);
}


/** Quita el color del borde del control
 * param:   sender (objeto):        Elemento que llamo al evento.
 **/
function setBlur(sender){
    sender.style.borderColor = "";
    showTextProgress(sender);
    if (theProgressDiv != null){
        hideObject(document.getElementById(theProgressDiv));
    }
}


/** Muestra un mensaje de alerta
 * param:   sender (objeto):        Elemento que llamo al evento.
 * param:   elMensaje (String):     Mensaje a mostrar en el cuadro de alerta
 **/
function showAlert(sender, elMensaje){
    alert(elMensaje);
}


/** Hace click a un button.
 * param:   sender (objeto):        Elemento que llamo al evento.
 * param:   buttonId (string):      Id del button a hacerle click.
 **/
function doClick(sender, buttonId){
    var button = document.getElementById(buttonId);
    if (button != null){
        button.click();
    }
}

/*function GetXmlHttpObject(handler)
{ 
    var objXmlHttp = null;
    if (!window.XMLHttpRequest)
    {
        // Microsoft
        objXmlHttp = GetMSXmlHttp();
        if (objXmlHttp != null)
        {
            objXmlHttp.onreadystatechange = handler;
        }
    } 
    else
    {
        // Mozilla | Netscape | Safari
        objXmlHttp = new XMLHttpRequest();
        if (objXmlHttp != null)
        {
            objXmlHttp.onload = handler;
            objXmlHttp.onerror = handler;
        }
    } 
    return objXmlHttp; 
} 

function GetMSXmlHttp()
{
    var xmlHttp = null;
    var clsids = ["Msxml2.XMLHTTP.6.0","Msxml2.XMLHTTP.5.0",
                 "Msxml2.XMLHTTP.4.0","Msxml2.XMLHTTP.3.0", 
                 "Msxml2.XMLHTTP.2.6","Microsoft.XMLHTTP.1.0", 
                 "Microsoft.XMLHTTP.1","Microsoft.XMLHTTP"];
    for(var i=0; i<clsids.length && xmlHttp == null; i++) {
        xmlHttp = CreateXmlHttp(clsids[i]);
    }
    return xmlHttp;
}

function CreateXmlHttp(clsid) {
    var xmlHttp = null;
    try {
        xmlHttp = new ActiveXObject(clsid);
        lastclsid = clsid;
        return xmlHttp;
    }
    catch(e) {}
}*/

function doRequest(objectToReciveResponse, theURL){
    //alert(theURL);
    var xmlHttp = null;
    try {
        // Firefox, Opera 8.0+, Safari
        xmlHttp= new XMLHttpRequest();
    } catch (e)  {
        // Internet Explorer
        try{
            xmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
        } catch (e) {
            try{
                xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
            } catch (e) {
                alert("Your browser does not support AJAX!");
                return false;
            }
        }
    }
    
    if (xmlHttp != null){
        xmlHttp.onreadystatechange = function(){
            if(xmlHttp.readyState == 4){
                if (objectToReciveResponse != null){
                    objectToReciveResponse.innerHTML = xmlHttp.responseText;
                    scrollDownObject(objectToReciveResponse);
                }
            }
        }
        xmlHttp.open("GET", theURL, true);
        xmlHttp.send(null);
    }
}

function scrollDownObject(objectToReciveResponse){
    if (objectToReciveResponse != null){
        if (objectToReciveResponse.scrollHeight > objectToReciveResponse.clientWidth){
            objectToReciveResponse.scrollTop = objectToReciveResponse.scrollHeight;
        }
    }
}


/** Refrescar el innerHtml de un elemento con lo respondido desde el url 
 * param:   theElement_Id (string):     Cual elemento a cambiar.
 * param:   theURL (string):            URL para hacer el request
 * param:   appendRandom (bool):        Si se le agregara un numero random al url
 **/
function updateElementWithRequest(theElement_Id, theURL, appendRandom){
    if (appendRandom) {
        theURL += "&rm=" + Math.random();
    }
    theElement_Obj = document.getElementById(theElement_Id);
    if (theElement_Obj != null){
        doRequest(theElement_Obj, theURL);
    } else {
        //alert(theElement_Id + " not fount.")
    }
}



/** Habilita o deshabilita un control 
 * param:   sender (objeto):         Elemento que llamo al evento.
 * param:   elObjetoId (string):     Cual elemento a cambiar el estado de disable.
 **/
function toggleDisable(sender, elObjetoId){
    var elObjeto = document.getElementById(elObjetoId);
    if (elObjeto != null){
        var actDisabled = elObjeto.disabled;
        elObjeto.disabled = !(actDisabled);
    }
}



function swapToogle(sender, objectIdA, objectIdB){
    toggleLayer(objectIdA, sender);
    toggleLayer(objectIdB, sender);
}


/** Muestra o oculta un elemento html con la propiedad de display 
 * param:   whichLayer (string):     Cual elemento a cambiar.
 * param:   sender (objeto):         Elemento que llamo al evento.
 **/
function toggleLayer(whichLayer, sender)
{
    var style = null;
    var objectToToogle = document.getElementById(whichLayer);
	if (objectToToogle != null){
		style = objectToToogle.style;
	}
	
	if (style != null && style.display != null){
	    if (style.display == "none"){
	        style.display = "block";
	    } else if (style.display == "" || style.display == "block"){
	        style.display = "none";
	    }
	}
	
	if (sender != null && sender.src != null){
	    var srcTemp = sender.src.substring(0, sender.src.lastIndexOf('-')+1);
	    var sufijo = sender.src.substring(sender.src.lastIndexOf('-')+1, sender.src.length);
	    if (sufijo == 'collapsed.gif'){
	        sender.src = srcTemp + 'expanded.gif';
	    } else {
	        sender.src = srcTemp + 'collapsed.gif';
	    }
	}
}


function showTextProgress(sender){
    var progressDiv_Obj;
    var maxCharacters = 0;
    if (theProgressDiv != null){
        progressDiv_Obj = document.getElementById(theProgressDiv);
    }
    showObject(progressDiv_Obj);

    if (sender != null && progressDiv_Obj != null){
    
        //Cantidad de caracteres actuales
        var actuales;
        if (sender.value != null){
            actuales = sender.value.length;
        } else {
            actuales = 0;
        }
        
        //Obtiene el maximo de caracteres permitidos
        if (sender.attributes.maxlength != null){
            maxCharacters = sender.attributes.maxlength.value;
            if (maxCharacters >= 0){
                if (actuales >= maxCharacters){
                    sender.value = sender.value.substring(0, maxCharacters - 1);
                    actuales = sender.value.length;
                }
                
                //Calcula porcentaje
                if (actuales > 0 && maxCharacters > 0){
                    var percent = Math.round((actuales * 100) / maxCharacters);
                } else {
                    var percent = 0;        
                }
                
                //Aplica el largo y el texto.
                if (progressDiv_Obj.style != null && progressDiv_Obj.style.width != null){
                    progressDiv_Obj.style.width = percent + '%';
                }
                if (progressDiv_Obj.innerHTML != null){
                    progressDiv_Obj.innerHTML = percent + '%';
                }
            }
        }
    }
}


/** Cambia el fondo de un elemento tomando el "value" del indice seleccionado del DropDownList
 * param:   sender (objeto):        Elemento que llamo al evento.
 * param:   theElementID (string):  ID del elemento a cambiarle el fondo.
 **/
function cambiarFondoDDL(sender, theElementID){
    var theElement = null;
    var colorFondo = '#000000';
        
    if (document.getElementById){
		theElement = document.getElementById(theElementID);
	}
	
	if (sender.options != null){
        var optionSeleccionado = sender.options[sender.selectedIndex];
        if (optionSeleccionado.value){
            colorFondo = optionSeleccionado.value;
        }
    }

	if (theElement != null && theElement.style != null){
	    theElement.style.backgroundColor = colorFondo;
	}
}







function setLatLng(latInput, lngInput, theLat, thelng){
    var latInputObj;
    var lngInputObj;
    if (latInput != null && lngInput != null){
        latInputObj = document.getElementById(latInput);
        lngInputObj = document.getElementById(lngInput);
    }
    
    if (latInputObj.value != null && lngInputObj.value != null){
        latInputObj.value = theLat;
        lngInputObj.value = thelng;
    }

}
