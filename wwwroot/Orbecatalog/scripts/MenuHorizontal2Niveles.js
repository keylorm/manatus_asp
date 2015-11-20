
	/***********************************************************************************************
	
	Copyright (c) 2005 - Alf Magne Kalleland post@dhtmlgoodies.com
	
	Get this and other scripts at www.dhtmlgoodies.com
	
	You can use this script freely as long as this copyright message is kept intact.
	
	***********************************************************************************************/	
	var topMenuSpacer = 0; // Horizontal space(pixels) between the main menu items	
	var activateSubOnClick = false; // if true-> Show sub menu items on click, if false, show submenu items onmouseover	
	var activeMenuItem = false;	// Don't change this option. It should initially be false	
	var MSIE = navigator.userAgent.indexOf('MSIE')>=0?true:false;
	var Opera = navigator.userAgent.indexOf('Opera')>=0?true:false;
	var navigatorVersion = navigator.appVersion.replace(/.*?MSIE ([0-9]\.[0-9]).*/g,'$1')/1;
	var subMenu = "";
	var activo;
	var activeMenuByPage;
	var var_timer;
		
	/*
	These cookie functions are downloaded from 
	http://www.mach5.com/support/analyzer/manual/html/General/CookiesJavaScript.htm
	*/		
	function showHide()
	{	    
	    try{
	    cancelar();
		    if(activeMenuItem){			    
			    var theId = activeMenuItem.id.replace(/[^0-9]/g,'');
			    document.getElementById(subMenu + "_" +theId).style.display = 'none';			    			    			
		    }			    
		    activeMenuItem = this;				    
		    var theId = this.id.replace(/[^0-9]/g,'');
		    document.getElementById(subMenu + "_" +theId).style.display='block';    						    
	    }catch(e){
    	
	    }
	}
	
	function waitTime()
	{	
	    clearTimeout(var_timer);
	    var_timer = setTimeout("autoShowHide()",2000);
	}
	
	function cancelar(){
	    clearTimeout(var_timer);
	}
	
	function autoShowHide()
	{	
	    try{
		    if(activeMenuItem){			    
			    var theId = activeMenuItem.id.replace(/[^0-9]/g,'');
			    document.getElementById(subMenu + "_" +theId).style.display = 'none';			    			    			
		    }		    
		    activeMenuItem = activeMenuByPage;				    
		    var theId = activeMenuItem.id.replace(/[^0-9]/g,'');
		    document.getElementById(subMenu + "_" +theId).style.display='block';    						    
	    }catch(e){
    	
	    }
	}
	
	function initMenu(nombre,elSubMenu)
	{
	    subMenu = elSubMenu;
		var mainMenuObj = document.getElementById(nombre);
		var menuItems = mainMenuObj.getElementsByTagName('A');
		if(document.all){
			mainMenuObj.style.visibility = 'hidden';
			document.getElementById(subMenu).style.visibility='hidden';
		}				
		var currentLeftPos = 15;		
		for(var no=0;no<menuItems.length;no++){			
			if(activateSubOnClick)menuItems[no].onclick = showHide; else menuItems[no].onmouseover = showHide;			
			menuItems[no].onmouseout = waitTime;
			menuItems[no].id = 'mainMenuItem' + (no+1);											
			if(menuItems[no].className == "selected"){
			    activo = no;	
			    activeMenuItem = menuItems[no];
			    activeMenuByPage = menuItems[no];
			}							
			if(!document.all)menuItems[no].style.bottom = '-1px';
			if(MSIE && navigatorVersion < 6)menuItems[no].style.bottom = '-2px';
		}		
		
		var mainMenuLinks = mainMenuObj.getElementsByTagName('A');		
		var subCounter = 1;
		var parentWidth = mainMenuObj.offsetWidth;
		while(document.getElementById(subMenu + "_" + subCounter)){
			var subItem = document.getElementById(subMenu + "_" + subCounter);
			subItem.onmouseover = cancelar
			subItem.onmouseout = waitTime;				
			if(subCounter==(activo+1)){
				subItem.style.display='block';
			}else{
				subItem.style.display='none';
			}			
			subCounter++;
		}
		if(document.all){
			mainMenuObj.style.visibility = 'visible';
			document.getElementById(subMenu).style.visibility='visible';
		}		
		document.getElementById(subMenu).style.display='block';
	}
		
    