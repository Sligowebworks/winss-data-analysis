// cookies.js contains code and functions for: 
//	- writing the Guide Header based on the refering page 
//	- WINSS survey popup

// domain used by all scripts
// var mydomain = "dpi.state.wi.us";
var mydomain = "127.0.0.1";

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
	var color;
	var xclose;
	if (url.indexOf("assessment") != -1){
		color = "#E1F1F3";
		xclose = "/sig/images/bclose.gif";
	}
	else if (url.indexOf("practices") != -1){
		color = "#CBE9DD";
		xclose = "/sig/images/cclose.gif";
	}
	else if (url.indexOf("improvement") != -1){
		color = "#FAE9BF";
		xclose = "/sig/images/aclose.gif";
	}
	else if (url.indexOf("data") != -1){
		color = "#F5C7D5";
		xclose = "/sig/images/dclose.gif";
	}
	document.write(
'<div style="margin-top: -0px;  margin-right: -0px;  margin-left: -0px;  margin-bottom: 0px;  padding: 0">',
'<table width="100%" border="0" cellspacing="0" cellpadding="0">',
'<form action="" method="POST">',
'<tr valign="top"  bgcolor="',color,'">',
'<td width="100">&nbsp;&nbsp;</td>',
'<td width="185"><a href="/sig/index.html"><img src="/sig/images/wins_sm.gif" alt="" border="0"><img src="/sig/images/mortar.gif" alt="" border="0" name=mortar><img src="/sig/images/guide_sm.gif" alt="" border="0"></a><BR><img src="/sig/images/mortartip_sm.gif" alt="" border="0"></td>',
'<td width="24">&nbsp;&nbsp;</td>',
'<td width="100%" valign="middle"><img src="/sig/images/spacer.gif" width=1 height=1 alt="" border="0"><br>',
'<b><font face="Verdana, Arial, Helvetica" size="2"><a href="', url, '">', question, '</a></font></b>',
'</font></td>',
'<td width="220" align="right" valign="middle"><nobr><select name="section" onchange="a = this.options[this.selectedIndex].value; location=a;">',
'<option value="" selected>Go to:</option>',
'<option value="/">DPI Home</option>',
'<option value="/sig/">WINSS Home</option>',
'<option value="http://',mydomain,':31489/survey_popup.html">comments / feedback</option>',
'<option value="/sig/assessment"> --- Standards and Assessment</option>',
'<option value="http://',mydomain,':31489/selschool.aspx"> --- Data Analysis</option>',
'<option value="/sig/improvement/"> --- Continuous Improvement</option>',
'<option value="/sig/practices"> --- Best Practices</option>',
'<option value="/dpi/search.html">Site Search</option></select>',
'<a href="javascript:deleteCookie();document.location.reload();"><img src="',xclose,'" width=16 height=16 vspace=2 hspace=2 alt="" border="0"></a></nobr></td>',
'</tr>',
'<tr valign="TOP"><td width="100%" height="2" colspan="5"><img src="/sig/images/black.gif" width="100%" height=1 alt="" border="0"><br>&nbsp;</td></tr>',
'</form>',
'</table>',
'</div>');
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
