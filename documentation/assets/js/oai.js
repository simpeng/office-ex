$(document).ready(function(){
	$(".oai-chapter-row").click(function(){
		var chapterSectionClass = $(this).attr("Id") + "-section";
		var chapterNode = $( "." + chapterSectionClass );

		if ( chapterNode.is( ":hidden" ) ) {
			chapterNode.show( "slow" );
		} else {
			chapterNode.slideUp();
		}
	});
	
	var id = getQueryStrings()["s"];
	$(".oai-chapter-section-row").removeClass("activedSectionRow");
	$("#" + id).addClass("activedSectionRow");
});
  
function getQueryStrings() { 
  var assoc  = {};
  var decode = function (s) { return decodeURIComponent(s.replace(/\+/g, " ")); };
  var queryString = location.search.substring(1); 
  var keyValues = queryString.split('&'); 

  for(var i in keyValues) { 
    var key = keyValues[i].split('=');
    if (key.length > 1) {
      assoc[decode(key[0])] = decode(key[1]);
    }
  } 

  return assoc; 
} 
