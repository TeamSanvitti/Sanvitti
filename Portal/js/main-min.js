

/*	For animation 
---------------------------------------------------------*/
$(document).ready(function() {
 wow = new WOW(
			{
			  boxClass:     'wow',      // default
			  animateClass: 'animated', // default
			  offset:       0,          // default
			  mobile:       true,       // default
			  live:         true        // default
			}
		  )
		  wow.init();
 wow_atom = new WOW(
			{
			  boxClass:     'wow_atom',      // default
			  animateClass: 'active-state', // default
			  offset:       0,          // default
			  mobile:       true,       // default
			  live:         true        // default
			}
		  )
		  wow_atom.init();
});
 
	
	
$(function() {
  $('button.navbar-toggle').click(function(){ 
    $('body').toggleClass('out');
    $('nav.navbar-fixed-top').toggleClass('out');
    if ($('body').hasClass('out')) {
      $(this).focus();
    } else {
      $(this).blur();
    }
  });
});

$(document).click(function(e) {
  if (!$(e.target).closest('.navbar-collapse, button.navbar-toggle').length && $('body').hasClass('out')) {
    e.preventDefault();
    $('button.navbar-toggle').trigger('click');
  }
}).keyup(function(e) {
  if (e.keyCode == 27 && $('body').hasClass('out')) {
    $('button.navbar-toggle').trigger('click');
  } 
});
