// cookies.js contains code and functions for: 
//	- writing the Guide Header based on the refering page 
//	- WINSS survey popup

// domain used by all scripts
var mydomain = "www2.dpi.state.wi.us";
// var mydomain = "127.0.0.1";

// functions for Guide Header
var question, url; // globals 

function setCookie(question, url){
	document.cookie = 'dpi=question:' + escape(question) + '&url:' + escape(url) + '; path=/; domain=' + mydomain; 
}
function readCookie(){
	var allcookies = document.cookie;
	var pos = allcookies.indexOf("dpi=");
	if (pos != -1){
		var start = pos + 4;
		var end = allcookies.indexOf(";",start);
		if (end == -1) end = allcookies.length;
		var value = allcookies.substring(start,end);
		var a = value.split('&');
		for (var i=0; i < a.length; i++){
			a[i] = a[i].split(':');
		}
		question = unescape(a[0][1]);
		url = unescape(a[1][1]);
	}
}
function deleteCookie(){
	document.cookie = 'dpi="whatever";expires=Fri, 02-Jan-1970 00:00:00 GMT; path=/ domain=' + mydomain;
}

function writeGuideHeader(){
    alert('Error in cookies.js,  writeGuideHeader()');
}

// following code used for WINSS survey popup

// process survey cookie if in sig or question is set
// ie survey will popup when in sig tree or just left sig tree
process_survey_cookie();

function process_survey_cookie(){
	var surveyURL = "http://" + mydomain + "/sig/feedback/comments/survey_popup.html"; // URL of survey pop-up
	var hours = 24; // number of hours the cookie should last
	var triger = 2; // popup survey when hits = triger
	var allcookies = document.cookie;
	var pos = allcookies.indexOf("survey=");
	if (pos == -1){
		// write cookie survey=1
		var expires = new Date((new Date()).getTime() + hours*3600000);
		document.cookie = "survey=1; path=/; domain=" + mydomain + "; expires=" + expires.toGMTString(); 
	}
	else{
		// check value
		var start = pos + 7;
		var end = allcookies.indexOf(";",start);
		if (end == -1) end = allcookies.length;
		var hits = allcookies.substring(start,end); // hits
		var num = parseInt(hits); // convert to number
		if (num == triger){
			// increment cookie
			num = num + 1;
			var expires = new Date((new Date()).getTime() + hours*3600000);
			document.cookie = "survey=" + num + "; path=/; domain=" + mydomain + "; expires=" + expires.toGMTString(); 
			// display survey popup
			openPopup(surveyURL,'survey');
		}
		else if (num < 10){ 
			// increment cookie
			num = num + 1;
			var expires = new Date((new Date()).getTime() + hours*3600000);
			document.cookie = "survey=" + num + "; path=/; domain=" + mydomain + "; expires=" + expires.toGMTString(); 
		}
	}
}

function openPopup(file,window) {
	var winFeatures = 'width=390,height=580,scrollbars=yes,resizable=yes,menubar=no';
	var pg = file;
	win = open(pg,window,winFeatures);
	self.focus(); 
}

function manualPopup(file,window) {
	var winFeatures = 'width=390,height=580,scrollbars=yes,resizable=yes,menubar=no';
	var pg = file;
	win = open(pg,window,winFeatures);
	 if (window.focus) {window.focus()};
}
