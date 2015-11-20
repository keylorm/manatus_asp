// JScript File


function setCenter_FromDDL(sender, theMap, theLatAttribute, theLngAttribute){
    var theOption = null;
	var realLat = null;
	var realLng = null;
	
	if (sender.options != null){
        theOption = sender.options[sender.selectedIndex];
    }

    for( var x = 0; x < theOption.attributes.length; x++ ) {
	    if( theOption.attributes[x].nodeName.toLowerCase() == theLatAttribute.toLowerCase()) {
			realLat = theOption.attributes[x].nodeValue;
	    }
    }
    
    for( var x = 0; x < theOption.attributes.length; x++ ) {
	    if( theOption.attributes[x].nodeName.toLowerCase() == theLngAttribute.toLowerCase()) {
			realLng = theOption.attributes[x].nodeValue;
	    }
    }

	if (realLat != null && realLng != null){
	    theMap.setCenter(new GLatLng(realLat, realLng), 14);
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