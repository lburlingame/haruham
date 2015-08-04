$( document ).ready(function() {
	$("#nav-bar").css("top",Math.max(0,100-$(this).scrollTop()));
    $("#account_menu").css("top",Math.max(40,140-$(this).scrollTop()));
    $("#help_menu").css("top",Math.max(40,140-$(this).scrollTop()));
});

$(window).scroll(function(){
    $("#nav-bar").css("top",Math.max(0,100-$(this).scrollTop()));
    $("#account_menu").css("top",Math.max(40,140-$(this).scrollTop()));
    $("#help_menu").css("top",Math.max(40,140-$(this).scrollTop()));
});

//var $menu = $("#account_menu");

$('#account_button').on('click',function(e){
    var menu = document.getElementById("account_menu")
	menu.style.borderLeft = "1px solid black";	
	menu.style.borderRight = "1px solid black";
	$("#account_menu").animate({height: $("#account_menu")[0].scrollHeight+'px'}, 150, function() {
    	menu.style.borderBottom = "1px solid black";
	});;
});

$('#help_button').on('click',function(e){
	var menu = document.getElementById("help_menu")
	menu.style.borderLeft = "1px solid black";
	menu.style.borderRight = "1px solid black";
	$("#help_menu").animate({height: $("#help_menu")[0].scrollHeight+'px'}, 150, function() {
    	menu.style.borderBottom = "1px solid black";
	});;
});

$(document).click(function(e) { 
	if(!$(event.target).closest('#account_button').length && !$(event.target).closest('#account_menu').length) {
		if($('#account_menu').is(":visible")) {
		   // $('#account_menu').hide()

	    	$("#account_menu").animate({height: 0}, 150, function() {
		    	document.getElementById("account_menu").style.border = "none";
			});

		}
	}
	if(!$(event.target).closest('#help_button').length) {
		if($('#help_menu').is(":visible")) {
		   // $('#account_menu').hide()

	    	$("#help_menu").animate({height: 0}, 150, function() {
		    	document.getElementById("help_menu").style.border = "none";
			});

		}
	} 

});