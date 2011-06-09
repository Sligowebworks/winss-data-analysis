<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CESA_Map_4.ascx.cs" Inherits="SligoCS.Web.WI.WebUserControls.CESA_Map_4" %>

<img src="<%= Request.ApplicationPath%>/images/cesa4map.gif" alt="cesa4map" usemap="#cesa4map" border="0" width="390" height="345" />

<map id="cesa4map" name="cesa4map">
<area shape="poly" alt="Blair-Taylor" coords="141,105,141,77,147,78,151,72,161,70,167,74,172,74,174,60,177,60,178,50,195,55,196,73,215,73,215,82,204,85,199,94,199,98,202,101,203,104,193,106,186,98,180,102,179,116,155,109,154,101,147,107" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04048503XXXX"})); %>" title="Blair-Taylor" onmouseover="setText('Blair-Taylor')" onmouseout="setText(' ')" />
<area shape="poly" alt="Melrose-Mindoro" coords="179,116,180,102,186,98,192,107,204,104,220,106,221,115,226,118,224,122,224,126,229,132,226,142,218,142,216,144,217,151,222,154,214,163,205,164,202,168,194,168,190,165,187,168,169,157,174,151,168,143,170,138,179,139" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04342803XXXX"})); %>" title="Melrose-Mindoro" onmouseover="setText('Melrose-Mindoro')" onmouseout="setText(' ')" />
<area shape="poly" alt="Sparta Area" coords="227,142,231,133,225,126,225,120,264,120,265,127,269,127,274,119,274,180,284,183,285,186,275,187,277,194,274,198,268,197,265,208,249,204,245,210,232,210,229,198,231,194,225,190" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04546003XXXX"})); %>" title="Sparta Area" onmouseover="setText('Sparta Area')" onmouseout="setText(' ')" />
<area shape="poly" alt="Norwalk-Ontario-Wilton" coords="269,205,267,204,268,197,274,198,278,194,277,187,285,187,285,196,291,197,305,192,313,193,314,212,308,235,302,245,294,245,297,252,285,255,270,245,267,236,275,224,270,218" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04399003XXXX"})); %>" title="Norwalk-Ontario-Wilton" onmouseover="setText('Norwalk-Ontario-Wilton')" onmouseout="setText(' ')" />
<area shape="poly" alt="Cashton" coords="229,202,231,211,245,211,249,204,264,209,268,206,270,219,274,224,266,238,268,246,247,246,236,232,206,228,205,224,214,223,222,216" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04098003XXXX"})); %>" title="Cashton" onmouseover="setText('Cashton')" onmouseout="setText(' ')" />
<area shape="poly" alt="Westby Area" coords="204,225,206,229,236,233,247,247,271,246,270,251,262,256,259,258,251,260,253,269,252,273,247,272,246,269,241,265,234,269,217,264,214,257,204,264,200,261,201,254,194,255,192,260,193,265,188,270,186,267,176,267,179,247,183,247,185,241,182,238,192,233,195,228,199,226" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04632103XXXX"})); %>" title="Westby Area" onmouseover="setText('Westby Area')" onmouseout="setText(' ')" />
<area shape="poly" alt="Bangor" coords="203,169,206,165,214,164,222,154,217,150,217,143,225,143,225,191,230,194,227,205,222,217,213,222,211,221,211,210,202,202" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04024503XXXX"})); %>" title="Bangor" onmouseover="setText('Bangor')" onmouseout="setText(' ')" />
<area shape="poly" alt="West Salem" coords="173,180,191,166,193,169,201,169,201,203,210,211,211,223,202,223,200,218,191,226,191,221,178,214,174,197,183,196,183,191,173,187" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04637003XXXX"})); %>" title="West Salem" onmouseover="setText('West Salem')" onmouseout="setText(' ')" />
<area shape="poly" alt="Onalaska" coords="171,185,183,191,183,196,162,196,159,190,159,180,162,181,166,188,168,185" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04409503XXXX"})); %>" title="Onalaska" onmouseover="setText('Onalaska')" onmouseout="setText(' ')" />
<area 
shape="poly" alt="Holmen" coords="167,142,174,151,168,157,187,169,173,179,172,185,167,186,166,189,161,181,141,184,127,165,146,165,142,155,148,144,154,144,156,141,161,146,166,146" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04256203XXXX"})); %>" title="Holmen" onmouseover="setText('Holmen')" onmouseout="setText(' ')" />
<area shape="poly" alt="Galesville-Ettrick-Trempealeau" coords="140,105,148,107,153,102,155,110,178,116,179,140,170,137,166,146,162,146,157,141,153,144,148,143,142,157,145,166,127,165,122,160,101,154,99,149,105,137,108,139,113,138,117,120,123,120,125,113,132,111,135,116,139,115" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04200903XXXX"})); %>" title="Galesville-Ettrick-Trempealeau" onmouseover="setText('Galesville-Ettrick-Trempealeau')" onmouseout="setText(' ')" />
<area shape="poly" alt="Cochrane-Fountain City" coords="105,136,98,149,100,156,42,107,36,82,44,82,45,86,65,79,70,74,70,82,76,84,80,82,81,93,87,97,88,102,93,102,94,111,91,115,101,121" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04115503XXXX"})); %>" title="Cochrane-Fountain City" onmouseover="setText('Cochrane-Fountain City')" onmouseout="setText(' ')" />
<area shape="poly" alt="Alma" coords="72,73,64,78,45,86,44,81,32,81,34,74,24,61,2,49,10,21,18,22,38,30,39,40,53,46,56,54,66,50,80,52,78,65" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04008403XXXX"})); %>" title="Alma" onmouseover="setText('Alma')" onmouseout="setText(' ')" />
<area shape="poly" alt="Arcadia" coords="139,83,141,105,139,105,138,114,135,116,132,110,124,113,122,119,116,120,113,138,108,138,105,136,101,120,91,114,94,111,93,102,88,102,87,96,80,92,79,81,75,83,70,81,70,74,79,64,80,51,97,46,98,62,101,62,100,70,108,70,111,71,114,74,116,77,125,79,129,76" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04015403XXXX"})); %>" title="Arcadia" onmouseover="setText('Arcadia')" onmouseout="setText(' ')" />
<area shape="poly" alt="Independence" coords="140,60,140,81,136,81,129,76,124,79,116,76,110,70,101,69,101,61,98,61,97,46,107,46,118,33,131,37,136,28,139,36,139,43,141,49,137,50,138,60" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04263203XXXX"})); %>" title="Independence" onmouseover="setText('Independence')" onmouseout="setText(' ')" />
<area shape="poly" alt="Whitehall" coords="194,53,179,49,177,60,174,60,172,74,166,74,161,69,151,72,147,78,140,76,140,59,138,59,137,49,141,49,139,35,135,24,147,25,149,31,157,34,173,29,198,25,198,35,201,37,201,45,190,46" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04642603XXXX"})); %>" title="Whitehall" onmouseover="setText('Whitehall')" onmouseout="setText(' ')" />
<area shape="poly" alt="Alma Center" coords="201,46,201,36,198,34,198,24,211,24,223,16,224,8,231,2,248,2,247,48,239,47,238,58,229,59,225,64,222,65,220,62,209,57" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04009103XXXX"})); %>" title="Alma Center" onmouseover="setText('Alma Center')" onmouseout="setText(' ')" />
<area shape="poly" alt="Black River Falls" coords="293,120,227,119,220,114,220,106,203,104,199,97,199,94,204,85,215,82,215,72,196,72,194,54,190,46,201,45,208,56,220,61,223,65,226,63,229,58,238,58,239,46,247,48,247,38,255,38,255,49,294,49,294,72,316,72,316,113,293,113" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04047603XXXX"})); %>" title="Black River Falls" onmouseover="setText('Black River Falls ')" onmouseout="setText(' ')" />
<area shape="poly" alt="Tomah Area" coords="316,189,308,188,291,196,285,195,284,183,274,180,274,121,294,120,293,113,317,113,317,85,338,85,338,96,362,96,362,132,339,131,339,156,340,161,353,161,355,180,353,185,341,186,332,191,338,194,331,201,328,201,325,196,321,201,319,201,317,198,316,188,308,188" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04574703XXXX"})); %>" title="Tomah Area" onmouseover="setText('Tomah Area')" onmouseout="setText(' ')" />
<area shape="poly" alt="Royall" coords="309,232,314,213,313,193,304,191,308,189,315,188,317,198,321,202,325,196,329,201,339,201,339,210,362,210,362,238,357,242,350,240,343,245,328,238,327,234" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04167303XXXX"})); %>" title="Royall" onmouseover="setText('Royall')" onmouseout="setText(' ')" />
<area shape="poly" alt="Wonewoc-Union Center" coords="343,246,350,240,357,242,363,238,362,229,383,243,383,258,375,262,375,268,349,284,338,281,342,274,339,266" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04671303XXXX"})); %>" title="Wonewoc-Union Center" onmouseover="setText('Wonewoc-Union Center')" onmouseout="setText(' ')" />
<area shape="poly" alt="Hillsboro" coords="289,255,298,252,294,245,303,245,310,232,327,234,327,238,343,245,339,266,342,274,337,284,330,282,324,288,308,290,309,281,304,280" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04254103XXXX"})); %>" title="Hillsboro" onmouseover="setText('Hillsboro')" onmouseout="setText(' ')" />
<area shape="poly" alt="La Farge" coords="253,274,254,268,251,261,265,256,270,250,271,246,285,255,289,255,297,269,296,279,287,278,285,283,273,284" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04286303XXXX"})); %>" title="La Farge" onmouseover="setText('La Farge')" onmouseout="setText(' ')" />
<area shape="poly" alt="Viroqua Area" coords="178,267,186,266,188,270,193,265,192,260,194,254,200,254,200,261,204,264,213,257,217,264,234,269,240,265,247,270,247,271,253,274,257,276,252,287,258,297,240,298,240,302,225,315,201,314,203,310,201,306,200,297,183,284,175,284" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04598503XXXX"})); %>" title="Viroqua Area" onmouseover="setText('Viroqua Area')" onmouseout="setText(' ')" />
<area shape="poly" alt="De Soto Area" coords="178,247,176,267,177,267,175,283,183,284,200,297,201,306,203,310,200,314,210,315,215,336,207,342,183,342,164,332,155,262,155,240,165,240,166,245,171,250" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04142103XXXX"})); %>" title="De Soto Area" onmouseover="setText('De Soto Area')" onmouseout="setText(' ')" />
<area shape="poly" alt="La Crosse" coords="141,185,158,183,160,193,166,199,172,196,177,207,178,214,190,220,191,226,200,218,203,225,195,228,192,233,182,238,184,241,183,247,176,247,171,250,166,244,164,240,155,240,159,221" href="<%= Request.ApplicationPath%>/SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=04284903XXXX"})); %>" title="La Crosse" onmouseover="setText('La Crosse')" onmouseout="setText(' ')" />
</map>

<div id="District" style="position:relative; top:-30px; left:250px;"> </div>