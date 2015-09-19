<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CESA_Map_2_2.ascx.cs" Inherits="SligoCS.Web.WI.WebUserControls.CESA_Map_2_2" %>

        <asp:Label ID="Map_2_2_Head_Label" CssClass="title" Text="CESA 2: Click on a School District or hot link below" BackColor="#EFEFEF" runat="server" /><br />
        <a href="SchoolScript.aspx<%Response.Write(GetQueryString(new String[] {"SEARCHTYPE=CE", "L=2", "HS=0"}));%>">View map that includes elementary school districts</a>
                    
<img src="<%= Request.ApplicationPath%>/images/cesa2map2_445w.gif" alt="cesa2map2" usemap="#cesa2map2" border="0" />

<map id="cesa2map2" name="cesa2map2">
<area shape="poly" alt="Wisconsin Heights" coords="68,49,67,55,64,61,56,65,52,62,50,72,44,75,45,82,47,77,52,77,49,81,53,91,39,94,38,91,20,94,10,88,1,90,1,82,4,67,22,39,36,41,50,46,55,52" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02046903XXXX"})); %>" title="Wisconsin Heights" onmouseover="setText('Wisconsin Heights')" onmouseout="setText(' ')" />
<area shape="poly" alt="Mount Horeb Area" coords="13,91,21,95,38,92,40,94,54,91,56,88,63,90,61,95,66,97,67,107,61,110,64,112,62,118,58,121,58,130,65,127,65,135,62,142,57,144,53,138,51,140,52,146,43,147,37,144,27,153,13,156,8,149,7,139,13,138" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02379403XXXX"})); %>" title="Mount Horeb Area" onmouseover="setText('Mount Horeb Area')" onmouseout="setText(' ')" />
<area shape="poly" alt="New Glarus" coords="26,154,37,144,44,148,53,146,51,140,53,138,59,146,66,148,67,157,70,163,70,168,79,178,76,180,72,179,68,182,54,183,54,187,42,186,32,184,26,171"  href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02393403XXXX"})); %>" title="New Glarus" onmouseover="setText('New Glarus')" onmouseout="setText(' ')" />
<area shape="poly" alt="Monticello" coords="31,184,45,187,54,186,55,183,68,183,72,179,76,179,80,177,88,179,88,175,93,174,97,183,94,183,95,187,101,190,101,192,93,192,90,187,82,198,85,200,85,203,79,209,75,209,72,205,66,205,64,208,59,208,54,204,48,203,50,199,47,198,44,203,39,200,39,193,30,192" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02369603XXXX"})); %>" title="Monticello" onmouseover="setText('Monticello')" onmouseout="setText(' ')" />
<area shape="poly" alt="Monroe" coords="38,200,44,203,47,198,49,199,48,203,59,208,64,208,66,206,72,206,75,210,75,217,84,219,82,225,74,227,74,231,79,232,79,235,76,237,79,242,79,246,76,246,75,250,78,251,81,259,76,264,29,264,16,246,19,223,28,206" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02368203XXXX"})); %>" title="Monroe" onmouseover="setText('Monroe')" onmouseout="setText(' ')" />
<area shape="poly" alt="Middleton-Cross Plains" coords="67,104,66,97,61,94,63,89,56,88,54,90,49,81,52,77,47,77,45,80,45,77,45,74,50,71,52,63,56,65,65,61,69,48,73,48,87,66,92,68,93,73,100,75,98,79,93,80,89,87,86,86,80,91,88,98,87,102,76,101" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02354903XXXX"})); %>" title="Middleton-Cross Plains" onmouseover="setText('Middleton-Cross Plains')" onmouseout="setText(' ')" />
<area shape="poly" alt="Verona Area" coords="64,134,64,128,58,130,58,120,61,118,63,113,60,110,66,107,67,104,75,101,86,102,83,108,86,110,91,109,92,105,101,103,104,117,100,118,99,124,89,135,85,132,80,135" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02590103XXXX"})); %> " title="Verona Area" onmouseover="setText('Verona Area')" onmouseout="setText(' ')" />
<area shape="poly" alt="Belleville" coords="103,169,101,179,95,179,92,174,88,175,88,179,80,176,77,179,77,175,73,169,72,170,69,168,70,162,67,157,65,147,58,144,62,142,66,134,80,135,84,133,89,134,94,146,105,151,101,161" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02035003XXXX"})); %> " title="Belleville" onmouseover="setText('Belleville')" onmouseout="setText(' ')" />
<area shape="poly" alt="Waunakee Community" coords="101,75,93,73,92,67,87,66,72,47,70,43,82,43,94,33,101,34,101,37,106,36,109,46,119,47,119,62,116,61,111,65,112,69,107,69" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02618103XXXX"})); %>" title="Waunakee Community" onmouseover="setText('Waunakee Community')" onmouseout="setText(' ')" />
<area shape="poly" alt="Madison Metropolitan" coords="105,117,101,103,96,102,91,106,90,109,85,109,83,107,88,98,81,91,86,86,90,87,94,80,100,80,107,69,113,68,114,66,119,67,119,70,123,69,124,67,129,68,134,70,133,75,136,76,136,80,141,81,145,82,145,88,141,89,140,95,136,104,127,91,123,94,120,100,120,105,111,105,108,118" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02326903XXXX"})); %>" title="Madison Metropolitan" onmouseover="setText('Madison Metropolitan')" onmouseout="setText(' ')" />
<area shape="poly" alt="Oregon" coords="102,169,101,160,105,151,93,145,89,136,99,125,100,118,108,118,111,105,126,105,124,114,127,116,127,119,133,119,133,124,131,124,131,132,124,140,130,141,130,145,124,149,130,150,132,159,124,158,122,164,117,162,112,170,109,166" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02414403XXXX"})); %>" title="Oregon" onmouseover="setText('Oregon')" onmouseout="setText(' ')" />
<area shape="poly" alt="Albany" coords="89,218,75,217,75,210,79,209,85,203,85,200,82,197,90,187,93,192,102,192,101,190,95,186,94,183,97,183,103,172,103,168,109,167,112,171,115,165,117,174,114,176,115,182,119,183,119,198,123,199,122,207,119,208,110,208,105,213,94,214" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02006303XXXX"})); %>" title="Albany" onmouseover="setText('Albany')" onmouseout="setText(' ')" />
<area shape="poly" alt="Juda" coords="76,265,81,259,78,251,75,250,76,246,79,246,79,242,76,237,79,235,79,232,74,231,74,227,83,225,84,217,93,217,92,222,89,226,98,227,95,232,104,236,104,242,110,245,105,248,105,255,111,260,102,266" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02273703XXXX"})); %>" title="Juda" onmouseover="setText('Juda')" onmouseout="setText(' ')" />
<area shape="poly" alt="Brodhead" coords="108,265,111,260,106,257,105,248,111,245,105,242,103,235,96,231,98,227,90,225,94,214,106,214,110,209,120,209,124,204,131,204,131,210,134,211,134,216,132,216,132,227,130,231,137,236,138,239,129,239,135,245,139,245,141,256,136,257,136,259,144,266" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02070003XXXX"})); %>" title="Brodhead" onmouseover="setText('Brodhead')" onmouseout="setText(' ')" />
<area shape="poly" alt="Sun Prairie Area" coords="145,82,136,80,136,75,133,75,134,67,136,66,136,61,140,60,139,54,146,50,146,35,148,32,146,27,160,21,167,27,178,41,172,42,172,64,162,69,163,79,147,79" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02565603XXXX"})); %>" title="Sun Prairie Area" onmouseover="setText('Sun Prairie Area')" onmouseout="setText(' ')" />
<area shape="poly" alt="De Forest Area" coords="132,69,124,67,123,69,119,68,119,47,109,46,106,36,101,36,101,25,111,21,112,23,119,21,120,17,119,10,132,5,136,9,146,0,146,26,148,33,146,35,146,51,139,54,139,59,136,61,135,67,132,66,133,69" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02131603XXXX"})); %>" title="De Forest Area" onmouseover="setText('De Forest Area')" onmouseout="setText(' ')" />
<area shape="poly" alt="Monona Grove" coords="145,103,145,81,147,79,162,78,169,90,162,99,153,99,150,104" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02367503XXXX"})); %>" title="Monona Grove" onmouseover="setText('Monona Grove')" onmouseout="setText(' ')" />
<area shape="poly" alt="Monona Grove" coords="126,105,119,104,122,94,127,92,132,98,127,100" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02367503XXXX"})); %>" title="Monona Grove" onmouseover="setText('Monona Grove')" onmouseout="setText(' ')" />
<area shape="poly" alt="McFarland" coords="133,122,132,118,127,118,126,115,124,113,127,103,132,98,136,103,140,96,140,89,145,88,145,103,154,106,154,110,152,112,149,109,145,109,146,115,145,118" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02338103XXXX"})); %>" title="McFarland" onmouseover="setText('McFarland')" onmouseout="setText(' ')" />
<area shape="poly" alt="Stoughton Area" coords="132,158,130,150,124,149,131,145,130,141,125,139,132,132,131,125,147,116,146,109,148,109,152,112,154,110,154,106,150,103,153,98,161,98,164,102,168,100,170,110,173,114,171,117,174,119,172,123,181,125,178,127,179,133,178,134,178,141,171,144,172,150,168,151,167,154,159,156,158,161,155,160,154,167,145,167,146,160,133,158" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02562103XXXX"})); %>" title="Stoughton Area" onmouseover="setText('Stoughton Area')" onmouseout="setText(' ')" />
<area shape="poly" alt="Evansville Community" coords="136,203,123,204,123,197,119,198,119,182,115,181,114,176,117,174,116,165,119,164,122,165,125,159,146,161,145,167,154,168,159,174,160,186,166,185,178,189,179,196,168,197,167,207,141,206,140,213,134,204" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02169403XXXX"})); %>" title="Evansville Community" onmouseover="setText('Evansville Community')" onmouseout="setText(' ')" />
<area shape="poly" alt="Parkview" coords="144,266,135,257,141,257,140,246,135,245,129,238,139,238,138,236,130,230,132,229,133,215,138,210,140,213,142,207,167,207,168,197,173,197,173,212,177,212,177,237,173,240,173,264" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02415103XXXX"})); %>" title="Parkview" onmouseover="setText('Parkview')" onmouseout="setText(' ')" />
<area shape="poly" alt="Marshall" coords="165,83,163,78,162,69,172,64,173,42,187,37,188,45,192,45,193,48,198,51,198,78,181,78,180,82,174,81,173,78,170,83" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02333203XXXX"})); %>" title="Marshall" onmouseover="setText('Marshall')" onmouseout="setText(' ')" />
<area shape="poly" alt="Deerfield Community" coords="171,111,168,99,165,101,163,99,169,90,166,83,170,83,172,79,173,81,180,82,180,78,199,78,198,88,193,96,195,97,191,102,187,104,187,110,177,112" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02130903XXXX"})); %>" title="Deerfield Community" onmouseover="setText('Deerfield Community')" onmouseout="setText(' ')" />
<area shape="poly" alt="Cambridge" coords="179,133,178,127,182,125,172,123,174,119,170,116,173,112,187,110,186,103,192,102,197,89,203,91,206,88,207,97,204,103,214,110,213,118,216,121,204,138" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02089603XXXX"})); %>" title="Cambridge" onmouseover="setText('Cambridge')" onmouseout="setText(' ')" />
<area shape="poly" alt="Edgerton" coords="177,188,165,185,160,185,159,173,154,169,154,160,158,160,160,155,167,154,168,151,173,149,172,145,178,141,178,133,204,138,205,148,198,151,198,157,208,160,200,164,198,167,192,167,188,172,188,185,177,184" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02156803XXXX"})); %>" title="Edgerton" onmouseover="setText('Edgerton')" onmouseout="setText(' ')" />
<area shape="poly" alt="Waterloo" coords="199,78,198,50,193,48,192,44,188,44,187,36,203,27,222,19,232,19,242,29,242,34,238,34,225,52,224,57,223,63,212,70,212,74,207,74,206,78" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02611803XXXX"})); %>" title="Waterloo" onmouseover="setText('Waterloo')" onmouseout="setText(' ')" />
<area shape="poly" alt="Lake Mills Area" coords="215,111,204,105,207,98,205,87,203,91,198,88,200,78,206,79,207,74,212,74,213,69,223,63,226,54,233,56,238,54,238,57,244,57,242,70,249,69,250,72,244,78,241,77,243,83,240,85,244,90,234,92,232,97,229,97,227,100,228,102,222,109,216,107" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02289803XXXX"})); %>" title="Lake Mills Area" onmouseover="setText('Lake Mills Area')" onmouseout="setText(' ')" />
<area shape="poly" alt="Watertown" coords="251,71,248,68,245,71,242,71,244,57,238,56,238,54,227,54,227,50,238,34,243,34,243,29,236,22,253,17,261,15,272,18,292,25,304,39,305,49,292,60,285,63,285,71,301,85,303,95,299,103,296,95,292,99,285,100,286,103,280,103,283,94,289,94,286,85,282,84,277,75,271,76,269,72,257,68,251,71,248,68,241,71,243,65" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02612503XXXX"})); %>" title="Watertown" onmouseover="setText('Watertown')" onmouseout="setText(' ')" />
<area shape="poly" alt="Johnson Creek" coords="245,90,240,85,243,83,241,78,246,78,251,70,257,69,262,70,269,73,271,76,277,75,281,84,286,85,288,94,280,96,279,92,271,94,265,90,262,89,263,95,257,96,258,101,255,100,254,94,245,96" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02273003XXXX"})); %>" title="Johnson Creek" onmouseover="setText('Johnson Creek')" onmouseout="setText(' ')" />
<area shape="poly" alt="Jefferson" coords="214,111,221,109,228,102,227,100,228,97,233,97,233,92,244,91,246,97,253,96,255,100,258,101,258,99,257,98,258,95,263,95,265,91,273,94,278,93,281,97,279,103,286,104,286,101,292,99,295,95,299,103,297,107,302,113,300,118,291,118,292,121,277,129,270,127,273,123,253,123,251,125,246,124,247,120,227,121,228,114,221,117,220,113" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02270203XXXX"})); %>" title="Jefferson" onmouseover="setText('Jefferson')" onmouseout="setText(' ')" />
<area shape="poly" alt="Fort Atkinson" coords="207,159,198,156,198,151,205,148,205,137,216,122,213,118,214,111,220,113,221,116,227,117,227,122,237,123,247,120,246,124,253,125,254,123,271,123,270,127,277,130,280,134,280,139,277,141,270,143,257,137,256,152,249,151,247,156,226,162,226,158,215,157,215,152,210,153,207,155" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02188303XXXX"})); %>" title="Fort Atkinson" onmouseover="setText('Fort Atkinson')" onmouseout="setText(' ')" />
<area shape="poly" alt="Milton" coords="177,188,177,184,188,185,188,172,193,166,198,166,199,164,207,159,209,153,216,153,216,158,226,159,226,162,240,179,240,185,246,186,246,207,252,207,252,212,212,211,213,202,206,197,206,193,204,192,201,195,188,194,180,188" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02361203XXXX"})); %>" title="Milton" onmouseover="setText('Milton')" onmouseout="setText(' ')" />
<area shape="poly" alt="Whitewater" coords="252,207,246,207,245,185,240,185,240,179,227,162,247,156,248,150,256,151,256,137,271,143,277,142,278,159,291,159,291,163,301,165,302,172,297,177,292,178,290,184,285,189,277,189,277,193,271,193,270,201,267,200,263,203,255,203" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02646103XXXX"})); %>" title="Whitewater" onmouseover="setText('Whitewater')" onmouseout="setText(' ')" />
<area shape="poly" alt="Palmyra-Eagle Area" coords="301,165,292,163,291,158,278,158,277,141,280,139,280,133,277,130,293,120,291,119,301,118,305,118,313,132,323,129,333,137,330,145,326,145,324,158,308,158,308,155,305,154,305,158,300,158" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02422103XXXX"})); %>" title="Palmyra-Eagle Area" onmouseover="setText('Palmyra-Eagle Area')" onmouseout="setText(' ')" />
<area shape="poly" alt="Janesville" coords="177,237,177,212,174,211,174,197,180,196,179,188,188,194,196,195,200,197,204,193,207,195,207,198,213,202,212,211,218,213,217,234,202,233,201,237,177,237" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02269503XXXX"})); %>" title="Janesville" onmouseover="setText('Janesville')" onmouseout="setText(' ')" />
<area shape="poly" alt="Beloit Turner" coords="174,265,173,239,177,237,206,238,201,252,193,251,189,247,185,247,186,265" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02042203XXXX"})); %>" title="Beloit Turner" onmouseover="setText('Beloit Turner')" onmouseout="setText(' ')" />
<area shape="poly" alt="Beloit" coords="188,266,185,248,189,248,194,252,216,252,214,260,204,263,207,265" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02041303XXXX"})); %>" title="Beloit" onmouseover="setText('Beloit')" onmouseout="setText(' ')" />
<area shape="poly" alt="Clinton Community" coords="207,265,205,262,214,260,216,252,201,251,207,239,200,238,202,235,217,235,218,212,252,213,249,222,248,232,255,239,255,255,253,257,253,267" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02113403XXXX"})); %>" title="Clinton Community" onmouseover="setText('Clinton Community')" onmouseout="setText(' ')" />
<area shape="poly" alt="East Troy Community" coords="302,173,301,158,305,158,305,155,308,155,308,158,358,158,358,180,353,180,348,194,345,193,344,197,332,198,332,195,328,192,323,192,322,197,318,196,315,188,315,183,312,178,308,178,306,175" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02154003XXXX"})); %>" title="East Troy Community" onmouseover="setText('East Troy Community')" onmouseout="setText(' ')" />
<area shape="poly" alt="Elkhorn Area" coords="279,199,278,189,286,189,291,184,291,178,299,177,302,173,308,178,312,179,315,184,316,191,318,196,322,196,322,193,329,192,332,198,340,199,339,208,332,208,329,216,326,215,325,219,318,223,314,224,304,227,300,222,302,220,299,216,295,216,294,210,289,210,288,206,293,206,292,199" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02163803XXXX"})); %>" title="Elkhorn Area" onmouseover="setText('Elkhorn Area')" onmouseout="setText(' ')" />
<area shape="poly" alt="Delavan-Darien" coords="255,243,255,238,248,232,248,223,253,211,252,207,254,203,264,203,267,199,270,201,271,195,278,195,278,200,292,200,292,206,287,206,288,210,294,210,294,216,299,217,301,219,299,222,299,233,301,236,299,238,291,237,291,235,282,238,282,244,273,244,273,247,265,249,262,244" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02138003XXXX"})); %>" title="Delavan-Darien" onmouseover="setText('Delavan-Darien')" onmouseout="setText(' ')" />
<area shape="poly" alt="Big Foot UHS" coords="254,265,254,257,256,255,254,243,261,243,265,248,282,244,287,237,297,239,305,246,319,242,320,247,318,250,318,255,323,254,326,257,327,263" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02601303XXXX"})); %>" title="Big Foot UHS" onmouseover="setText('Big Foot UHS')" onmouseout="setText(' ')" />
<area shape="poly" alt="Williams Bay" coords="310,244,303,244,299,240,301,236,299,233,300,222,303,227,311,225,310,230,315,236,313,239" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02648203XXXX"})); %>" title="Williams Bay" onmouseover="setText('Williams Bay')" onmouseout="setText(' ')" />
<area shape="poly" alt="Linn J6" coords="303,264,303,258,307,257,308,253,307,251,306,245,319,243,319,248,317,248,318,255,323,255,327,258,327,264" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02309403XXXX"})); %>" title="Linn J6" onmouseover="setText('Linn J6')" onmouseout="setText(' ')" />
<area shape="poly" alt="Waterford UHS" coords="358,158,384,158,390,168,398,168,400,160,406,156,419,156,412,165,414,169,414,182,402,184,402,189,379,193,376,196,371,196,369,193,364,193,363,187,366,184,358,184" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02608303XXXX"})); %>" title="Waterford UHS" onmouseover="setText('Waterford UHS')" onmouseout="setText(' ')" />
<area shape="poly" alt="Lake Geneva-Genoa City UHS" coords="325,254,318,254,318,251,321,249,320,242,314,243,315,236,312,233,310,229,311,225,320,224,326,217,330,216,332,208,336,208,335,214,347,221,347,224,354,225,360,232,359,241,359,264,328,264,328,258" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02288403XXXX"})); %>" title="Lake Geneva-Genoa City UHS" onmouseover="setText('Lake Geneva-Genoa City UHS')" onmouseout="setText(' ')" />
<area shape="poly" alt="Burlington Area" coords="357,228,353,224,347,223,347,220,335,214,336,209,339,208,340,199,348,195,352,186,353,181,358,182,359,185,364,184,362,187,363,192,369,192,371,196,376,196,379,193,402,189,402,210,385,209,385,228" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02077703XXXX"})); %>" title="Burlington Area" onmouseover="setText('Burlington Area')" onmouseout="setText(' ')" />
<area shape="poly" alt="Wilmot UHS" coords="361,264,360,246,379,246,387,252,390,250,390,242,400,244,400,253,404,256,405,263" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02654503XXXX"})); %>" title="Wilmot UHS" onmouseover="setText('Wilmot UHS')" onmouseout="setText(' ')" />
<area shape="poly" alt="Union Grove UHS" coords="403,183,414,183,414,168,415,162,418,162,418,156,438,156,439,208,421,208,421,213,404,213" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02608303XXXX"})); %>" title="Union Grove UHS" onmouseover="setText('Union Grove UHS')" onmouseout="setText(' ')" />
<area shape="poly" alt="Central/Westosha UHS" coords="400,243,397,243,389,243,389,250,383,250,380,246,360,245,360,229,385,229,386,210,403,210,403,214,422,213,422,210,439,209,440,262,405,263,405,256,401,254" href="SchoolScript.aspx<% Response.Write(GetQueryString(new String[] {"SEARCHTYPE=SC","L=0","FULLKEY=02505403XXXX"})); %>" title="Central/Westosha UHS" onmouseover="setText('Central/Westosha UHS')" onmouseout="setText(' ')" />
</map>

<div id="District" style="position:relative; top:-200px; left:330px;"> </div>