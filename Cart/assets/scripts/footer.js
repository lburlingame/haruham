var counter = 0;
$('#click_me').on("click", function() {
	counter++;
	if (counter == 7) {
		$( "#footer-body" ).append('<img src="assets/images/hello.gif">');
		var a1 = new Audio('assets/audio/hello.ogg');
		a1.play();
	}else if (counter > 7) {
		$( "#footer-body" ).append('<img src="assets/images/hello.gif">');
		var a2 = new Audio('assets/audio/there.ogg');
		a2.play();
	}
});
