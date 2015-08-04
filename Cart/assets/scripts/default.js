$( document ).ready(function() {
	if ($(this).scrollTop() > 200) {
		$("#scroll_to_top").fadeIn(300);
	}else{
		$("#scroll_to_top").fadeOut(300);
	}
});

$(window).scroll(function(){
  	if ($(this).scrollTop() > 200) {
		$("#scroll_to_top").fadeIn(300);
	}else{
		$("#scroll_to_top").fadeOut(300);
	}
});

$('#scroll_to_top').on("click",function() {
    $('html, body').animate({
    	scrollTop: "100px"
	}, 350);
});