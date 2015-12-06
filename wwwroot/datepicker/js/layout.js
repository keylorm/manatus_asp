(function($){
	var initLayout = function() {
		var hash = window.location.hash.replace('#', '');
		var currentTab = $('ul.navigationTabs a')
							.bind('click', showTab)
							.filter('a[rel=' + hash + ']');
		if (currentTab.size() == 0) {
			currentTab = $('ul.navigationTabs a:first');
		}
		showTab.apply(currentTab.get(0));
		$('#date').DatePicker({
			flat: true,
			date: '2008-07-31',
			current: '2008-07-31',
			calendars: 1,
			starts: 1,
			view: 'years'
		});
		var now = new Date();
		now.addDays(-10);
		var now2 = new Date();
		now2.addDays(-5);
		now2.setHours(0,0,0,0);
		$('#date2').DatePicker({
			flat: true,
			date: ['2008-07-31', '2008-07-28'],
			current: '2008-07-31',
			format: 'Y-m-d',
			calendars: 1,
			mode: 'multiple',
			onRender: function(date) {
				return {
					disabled: (date.valueOf() < now.valueOf()),
					className: date.valueOf() == now2.valueOf() ? 'datepickerSpecial' : false
				}
			},
			onChange: function(formated, dates) {
			},
			starts: 0
		});
		$('#clearSelection').bind('click', function(){
			$('#date3').DatePickerClear();
			return false;
		});
		$('#date3').DatePicker({
			flat: true,
			date: ['2009-12-28','2010-01-23'],
			current: '2010-01-01',
			calendars: 3,
			mode: 'range',
			starts: 1
		});
		$('.inputDate').DatePicker({
			format:'m/d/Y',
			date: $('#inputDate').val(),
			current: $('#inputDate').val(),
			starts: 1,
			position: 'right',
			onBeforeShow: function(){
				$('#inputDate').DatePickerSetDate($('#inputDate').val(), true);
			},
			onChange: function(formated, dates){
				$('#inputDate').val(formated);
				if ($('#closeOnSelect input').attr('checked')) {
					$('#inputDate').DatePickerHide();
				}
			}
		});
		var now3 = new Date();
		now3.addDays(-4);
		var now4 = new Date();

		var futureDay = new Date();
		futureDay.addDays(+5);
		
		now.setHours(0,0,0,0); // todas las fechas a evaluar deben de estar establecidas en hora=0, min=0, seg=0, mil=0 para que la evaluacion tenga efecto
		futureDay.setHours(0,0,0,0); // todas las fechas a evaluar deben de estar establecidas en hora=0, min=0, seg=0, mil=0 para que la evaluacion tenga efecto


		var currentTime = new Date()
	    // returns the month (from 0 to 11)
		var month = currentTime.getMonth() + 1
	    // returns the year (four digits)
		var year = currentTime.getFullYear()

		var datesDisabled = [];
		$.post('FechasNoDisponibles.asmx/GetDates', { month: month, year: year, lang: 'es' })
            .done(function(data){
                $("#loader-calendar").addClass("hidden");
                var string = $(data).find('string');
                var fechasObj = string.text();
                var fechas = fechasObj.split(":");
                fechasStr = fechas[1].replace("}", "");
                fechasStr = fechasStr.replace("]", "");
                fechasStr = fechasStr.replace("[", "");

                var fechaArray = fechasStr.split(',');

                for (var i = 0; i < fechaArray.length; i++) {
                    var split = fechaArray[i].split("-");
                    var dia = split[0].replace('"', '');
                    var mes = split[1].replace('"', '');
                    var ano = split[2].replace('"', '');
                    var disabledDate = new Date(ano, mes - 1, dia, 0, 0, 0, 0)
                    datesDisabled[i] = disabledDate;
                }

                /* Extraer la fecha por metodo GET */

		        var checkin = '';
		        var checkout = '';
		        var param1var = getQueryVariable('rango_fecha');
		        if (param1var != false) {
		            param1var = unescape(param1var);  //escapar los caracteres especiales en el URL
		            var rango = param1var.split("+-+"); //separar el rango (checkin - checkout)
		            var checkin = rango[0];
		            var checkout = rango[1];
		            //$('#widgetField input[id="TxtCheckinCheckout"]').val(checkin + " - " + checkout);
		            $('#widgetCalendar').DatePicker({
		                flat: true,
		                format: 'm/d/Y',
		                date: [checkin, checkout],
		                calendars: 1,
                        current: checkin,
		                mode: 'range',
		                starts: 1,
		                onRender: function (date) {
		                    return {
		                        disabled: (compareDates(datesDisabled, date)), //utilizamos una funcion para comparar la fecha actual con un arreglo de las fechas a deshabilitar
		                        //className: date.valueOf() == now2.valueOf() ? 'datepickerSpecial' : false
		                    }
		                },
		                onChange: function (formated, dates) {
		                    //verificador del grupo de fechas seleccionado y los dias deshabilitados para determinar si estan en mi seleccion
		                    //habilitar boton booking
		                    $(".reservation-form input[type='submit'").removeAttr('disabled');				
					        if (validarFechasDisponibles(datesDisabled, dates)){
						        //mostrar mensaje de alerta
					            alert("Within days of his/her selection are 'not available', please select different range");
						        //deshabilitar boton
						        $(".reservation-form input[type='submit'").attr("disabled","disabled");
					        }
					        //obtener el ingreso (checkin)
					        $("span.checkin").html(formated[0]);
		                    $('#widgetField input[id="TxtCheckinCheckout"]').val(formated.join(' - '));
		                }
		            });
		        } else {
		            $('#widgetCalendar').DatePicker({
		                flat: true,
		                format: 'm/d/Y',
		                date: [],
		                calendars: 1,
		                mode: 'range',
		                starts: 1,
		                onRender: function (date) {
		                    return {
		                        disabled: (compareDates(datesDisabled, date)), //utilizamos una funcion para comparar la fecha actual con un arreglo de las fechas a deshabilitar
		                        //className: date.valueOf() == now2.valueOf() ? 'datepickerSpecial' : false
		                    }
		                },
		                onChange: function (formated, dates) {
		                    //verificador del grupo de fechas seleccionado y los dias deshabilitados para determinar si estan en mi seleccion
		                    //habilitar boton booking
		                    $(".reservation-form input[type='submit'").removeAttr('disabled');				
					        if (validarFechasDisponibles(datesDisabled, dates)){
						        //mostrar mensaje de alerta
					            alert("Within days of his/her selection are 'not available', please select different range");
						        //deshabilitar boton
						        $(".reservation-form input[type='submit'").attr("disabled","disabled");
					        }
					        //obtener el ingreso (checkin)
					        $("span.checkin").html(formated[0]);
		                    $('#widgetField input[id="TxtCheckinCheckout"]').val(formated.join(' - '));
		                }
		            });
		        }

		            function getQueryVariable(variable) {
		                var query = window.location.search.substring(1);
		                var vars = query.split("&");
		                for (var i = 0; i < vars.length; i++) {
		                    var pair = vars[i].split("=");
		                    if (pair[0] == variable) {
		                        return pair[1];
		                    }
		                }
		                return false;
		            }

		 	

			        var styles = {
				        position: 'absolute',
				        top: '90px',
				        left: '45px',
				        'z-index': 9,
			        }
			        $('#widgetCalendar div.datepicker').css(styles);

		});
	
		var state = false;
		$('#widgetField>input[id="TxtCheckinCheckout"]').live('click', function () {
			/*$('#widgetCalendar').stop().animate({height: state ? 0 : $('#widgetCalendar div.datepicker').get(0).offsetHeight}, 500);
			state = !state;
			return false;*/
			//mostrar DatePicker
			var state = $('#widgetCalendar').attr("class");
			if(state == "hidden"){
				$('#widgetCalendar').removeClass("hidden");
			} else {
				$('#widgetCalendar').attr("class","hidden");
			}
		});

		$('#AplicarSeleccion').live('click', function () {
			$('#widgetCalendar').attr("class","hidden");
		});
		
	};
	
	var showTab = function(e) {
		var tabIndex = $('ul.navigationTabs a')
							.removeClass('active')
							.index(this);
		$(this)
			.addClass('active')
			.blur();
		$('div.tab')
			.hide()
				.eq(tabIndex)
				.show();
	};
	
	//comparador del rango de fechas seleccionado con las fechas no posibles en la seleccion, la idea es mostrar un mensaje al usuario si algun dia en la seleccion no es posible
	function validarFechasDisponibles(fechasNoDisponibles, seleccion){
		var encontrado = false;
		var inicio = seleccion[0];
		var fin = seleccion[seleccion.length-1];
		fin.setHours(0,0,0,0);

		for (var i = 0; i < fechasNoDisponibles.length; i++) {
			if((fechasNoDisponibles[i] >= inicio) && (fechasNoDisponibles[i] <= fin)){
				encontrado = true;
				break;
			}
		};

		return encontrado;
	}

	//funcion que permite comparar la fecha de cada dia del calendario con los dias deshabilitados en el calendario
	function compareDates(array, date){
		for (var i = 0; i < array.length; i++) {
			if(array[i].getTime() === date.getTime()){
				return true;
			}
		};
	}

	EYE.register(initLayout, 'init');
})(jQuery);