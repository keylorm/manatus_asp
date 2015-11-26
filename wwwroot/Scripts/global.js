$(document).ready(function () {
    //    $("#book_now_1").live('click', function () {
    //        $("#paso1").hide("slow", function () {
    //            if ($(".step").hasClass("active")) {
    //                $(".step").removeClass("active");

    //            }
    //            $(".step-2").addClass("active");


    //        });

    //        $("#paso2").show();
    //    });

    var topCalendar = $("input#TxtCheckinCheckout").position().top;
    var leftCalendar = $("input#TxtCheckinCheckout").position().left;

    var heightInput = $("input#TxtCheckinCheckout").height();
    $("#widgetCalendar").css("top", topCalendar + heightInput + 15);
    $("#widgetCalendar").css("left", leftCalendar);


});