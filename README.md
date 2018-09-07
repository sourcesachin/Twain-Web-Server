OPEN SOURCE TWAIN WEB SERVER
=============================
WORK WITH ALL LANGUAGES ONLY FOR WINDOWS OPERATING SYSTEM..

    $.ajax({
    	url: "http://localhost:8055",
    	data: {mode: 'SCAN'},
    	type: 'POST',
    	complete: function(){
    		//your action
    	},
    	success: function (data) {
    		var imgpath = "data:image/png;base64," + data;
    		//Base 64 bit string imgpath
    	},
    	error: function (xhr, textStatus, errorThrown) {
    		alert("No Server found!");
    	}
    });
