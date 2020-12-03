$('#laptops').click(function(event) {
	var x = 5;
	var html = '<h2>This is Laptops Page '+x+'</h2>';
	html+=			'<h2>This is Laptops Page</h2>';
	html+=			'<h2>This is Laptops Page</h2>';
	html+=			'<h2>This is Laptops Page</h2>';
	html+=			'<h2>This is Laptops Page</h2>';
	$('#content').html(html);
});


$(document).ready(function(){$(".megamenu").megamenu();});

        $(document).ready(function() {
            $(".dropdown img.flag").addClass("flagvisibility");

            $(".dropdown dt a").click(function() {
                $(".dropdown dd ul").toggle();
            });
                        
            $(".dropdown dd ul li a").click(function() {
                var text = $(this).html();
                $(".dropdown dt a span").html(text);
                $(".dropdown dd ul").hide();
                $("#result").html("Selected value is: " + getSelectedValue("sample"));
            });
                        
            function getSelectedValue(id) {
                return $("#" + id).find("dt a span.value").html();
            }

            $(document).bind('click', function(e) {
                var $clicked = $(e.target);
                if (! $clicked.parents().hasClass("dropdown"))
                    $(".dropdown dd ul").hide();
            });


            $("#flagSwitcher").click(function() {
                $(".dropdown img.flag").toggleClass("flagvisibility");
            });
        });

/*jQuery(document).ready(function(){
    $(".dropdown").hover(
        function() { $('.dropdown-menu', this).slideToggle("fast");
        },
        function() { $('.dropdown-menu', this).stop().fadeOut("fast");
    });
});*/