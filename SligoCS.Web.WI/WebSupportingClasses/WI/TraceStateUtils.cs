using System;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
    public class TraceStateUtils
    {
        [Flags()]
        // jdj: used for on-page debugging - controls what displays at the top of the page - 0 is default
        public enum TraceLevel
        {
            None = 0,
            Querystring = 1,
            Globals = 2,
            SQLStatement = 4,
            Graph = 8,
            Session = 16,
            Sort = 32,
        }
        public static string GetTrace(GlobalValues globals)
        {
            StringBuilder sb = new StringBuilder();
            int sum = 0;

            sb.Append(SHOW_HIDE_SCRIPT);
            foreach (TraceStateUtils.TraceLevel level in Enum.GetValues(typeof(TraceStateUtils.TraceLevel)))
            {
                sb.Append(String.Format("<a href=\"{1}\">{0}</a> | ", 
                    Enum.GetName(typeof(TraceStateUtils.TraceLevel), level),
                    globals.Page.Request.FilePath + 
                    globals.GetQueryString(new String[] {"TraceLevels=" + level})));
                sum += (int)level;
            }
            sb.Append(String.Format("<a href=\"{1}\">{0}</a> <br /> ",
                    "ALL",
                    globals.Page.Request.FilePath +
                    globals.GetQueryString(new String[] { "TraceLevels=" + sum.ToString() })));

            sb.Append(PrintGridVisibleColumns(globals.Page));

            if ((globals.TraceLevels & TraceStateUtils.TraceLevel.Querystring)
                    == TraceStateUtils.TraceLevel.Querystring)
                sb.Append(PrintQuerystring().ToString());

            if ((globals.TraceLevels & TraceStateUtils.TraceLevel.Globals)
                    == TraceStateUtils.TraceLevel.Globals)
                sb.Append(PrintGlobalValues(globals).ToString());

            if ((globals.TraceLevels & TraceStateUtils.TraceLevel.Session)
                    == TraceStateUtils.TraceLevel.Session)
                sb.Append(PrintSession().ToString());

            if ((globals.TraceLevels & TraceStateUtils.TraceLevel.SQLStatement)
                    == TraceStateUtils.TraceLevel.SQLStatement)
                sb.Append("<br />" + globals.TraceSql);

            if ((globals.TraceLevels & TraceStateUtils.TraceLevel.Graph)
                    == TraceStateUtils.TraceLevel.Graph)
                sb.Append(PrintGraphProps(globals.Page));

            if ((globals.TraceLevels & TraceStateUtils.TraceLevel.Sort)
                    == TraceStateUtils.TraceLevel.Sort)
                sb.Append(PrintSortProps(globals.Page));

            return sb.ToString();
        }
        public static StringBuilder PrintSortProps(Page argPage)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<br />");

            //safety check:
            if (argPage == null) return null;
            if (!(argPage is SligoCS.Web.Base.PageBase.WI.PageBaseWI))
                throw new Exception("not PageBaseWI");

            SligoCS.Web.Base.PageBase.WI.PageBaseWI page = (SligoCS.Web.Base.PageBase.WI.PageBaseWI)argPage;

            sb.Append("[Default Order By]");
            sb.Append(
                String.Join(",",
                page.QueryMarshaller.BuildOrderByList(page.DataSet.Tables[0].Columns).ToArray()
                )
                ).Append("<br /><br />");

            if (page.DataGrid is WinssDataGrid)
            {
                WinssDataGrid grid = (WinssDataGrid)page.DataGrid;

                sb.Append("[Page.DataGrid.OrderBy]  ");
                sb.Append(grid.OrderBy).Append("<br /><br />");

            }
            else
            {
                sb.Append("Could not get Sort from DataGrid: not WinssDataGrid");
            }

            if (page.Graph is WebUserControls.GraphBarChart
                || page.Graph is WebUserControls.GraphHorizBarChart
                || page.Graph is WebUserControls.GraphScatterplot)
            {

                String orderBy = String.Empty;

                if (page.Graph is WebUserControls.GraphBarChart)
                    orderBy = ((WebUserControls.GraphBarChart)page.Graph).OrderBy;
                else if (page.Graph is WebUserControls.GraphHorizBarChart)
                    orderBy = ((WebUserControls.GraphHorizBarChart)page.Graph).OrderBy;
                else if (page.Graph is WebUserControls.GraphScatterplot)
                    orderBy = "Scatterplot Control doesn't support Sorting";

                sb.Append("[Page.Graph.OrderBy]  ");
                sb.Append((orderBy == null)? "NULL" : orderBy).Append("<br /><br />");

            }
            else
            {
                sb.Append("Could not get Sort from Graph.  Control is of unexpected type, " + page.Graph.GetType().ToString());
            }

            return sb;
        }
        public static StringBuilder PrintGridVisibleColumns(Page argPage)
        {
            StringBuilder sb = new StringBuilder();
            
            //safety check:
            if (argPage == null) return null;
            if (!(argPage is SligoCS.Web.Base.PageBase.WI.PageBaseWI))
                throw new Exception("not PageBaseWI");

            SligoCS.Web.Base.PageBase.WI.PageBaseWI page = (SligoCS.Web.Base.PageBase.WI.PageBaseWI)argPage;

            System.Collections.Generic.List<String> cols = page.GetVisibleColumns(
                        page.GlobalValues.Group,
                       page. GlobalValues.OrgLevel,
                        page.GlobalValues.CompareTo,
                        page.GlobalValues.STYP
                    );


            sb.Append("<br />VISIBLE COLUMNS:<br />");
            sb.Append(@"<a href=""javascript:ReverseDisplay('traceVisible')"">Click to show/hide.</a>");
            sb.Append(@"<div id=""traceVisible"" style=""display:none;"">");

            sb.Append(String.Join(",<br />", cols.ToArray()));

            sb.Append(@"</div>");

            return sb;            
        }
        public static StringBuilder PrintGraphProps(Page parPage)
        {
            StringBuilder sb = new StringBuilder();
            ChartFX.WebForms.Chart graph;
            SligoCS.Web.Base.PageBase.WI.PageBaseWI page;
            //safety check:
            if (parPage == null) return null;
            if (!(parPage is  SligoCS.Web.Base.PageBase.WI.PageBaseWI))
                throw new Exception("not PageBaseWI");
            page = (SligoCS.Web.Base.PageBase.WI.PageBaseWI)parPage;
            
            if (page.Graph == null) return null;
           // if (!(page.Graph is WebUserControls.GraphBarChart))
               // throw new Exception("not GraphBarChart");
            graph = (ChartFX.WebForms.Chart)page.Graph;


            sb.Append(SYNTAX_HIGHLIGHT_SCRIPT);
            sb.Append("<br />Graph:<br />");
            sb.Append(@"<a href=""javascript:ReverseDisplay('traceGraph')"">Click to show/hide.</a>");
            sb.Append(@"<div id=""traceGraph"" style=""display:none;"">");


            sb.Append("graph.Visible:" + graph.Visible + "<br />");
            sb.Append("graph.Enabled:" + graph.Enabled + "<br />");
            sb.Append("graph.ContentUrl:" + graph.ContentUrl + "<br />");
            sb.Append("graph.Dirty[??]:" + graph.Dirty + "<br />");
            
            sb.Append("graph.AxesStyle:" + graph.AxesStyle.ToString() + "<br />");
            sb.Append("graph.Points.Count:" + graph.Points.Count + "<br />");

            try
            {
                sb.Append("graph.Series.Count:" + graph.Series.Count + "<br />");
            }
            catch (Exception x)
            {
                sb.Append(x.Message + "<br />");
            }

            if ((page.Graph is WebUserControls.GraphBarChart))
            {
                WebUserControls.GraphBarChart slGraph = (WebUserControls.GraphBarChart)graph;
                sb.Append("graph.Type:" + slGraph.Type.ToString() + "<br />");
                sb.Append("graph.DisplayColumnName:" + slGraph.DisplayColumnName + "<br />");
                sb.Append("graph.LabelColumnName:" + slGraph.LabelColumnName + "<br />");
                sb.Append("graph.SeriesColumnName:" + slGraph.SeriesColumnName + "<br />");
                sb.Append("graph.MaxRateInResult:" + slGraph.MaxRateInResult + "<br />");
            }

            sb.Append("Data"); sb.Append("<pre>");
            sb.Append(GetGraphExport(graph, ChartFX.WebForms.FileFormat.Text));
            sb.Append("</pre>");

            //note: FileContents is a mask field
            // and only applies to the XML and Binary FileFormat Exports
            //http://support.softwarefx.com/OnlineDoc/CfxNet70//WinAPI/Chart_FileContents.htm
            graph.FileContents =
                ChartFX.WebForms.FileContents.All 
                //&~ ChartFX.WebForms.FileContents.Pictures
                //&~ ChartFX.WebForms.FileContents.ReuseExtensions
                //&~ ChartFX.WebForms.FileContents.PrinterInfo
                ;
            
            sb.Append("XML"); sb.Append("<pre name='code' class='brush: xml; light: true;'>");
            sb.Append(
                GetGraphExport(graph, ChartFX.WebForms.FileFormat.Xml)
                .Replace("<", "&lt;")
                );
            sb.Append("</pre>");
            //sb.Append("Metafile");
            //sb.Append(GetGraphExport(graph, ChartFX.WebForms.FileFormat.Metafile));
            //sb.Append("External");
            //sb.Append(GetGraphExport(graph, ChartFX.WebForms.FileFormat.External));

            sb.Append(@"</div>");
            
            return sb;
        }
        private static string GetGraphExport(ChartFX.WebForms.Chart graph, ChartFX.WebForms.FileFormat format)
        {
            System.IO.Stream strm = new System.IO.MemoryStream(10000);
            //sb.Append("pre-export" + strm.Length);
            graph.Export(format, strm);

            strm.Seek(0, System.IO.SeekOrigin.Begin);
            int max = (int)strm.Length;
            byte[] chrs = new byte[max];
            strm.Read(chrs, 0, max);

            return Encoding.ASCII.GetString(chrs, 0, (int)strm.Length);
        }
        private static StringBuilder PrintSession()
        {
            System.Web.SessionState.HttpSessionState Session = HttpContext.Current.Session.Contents;
            StringBuilder Response = new StringBuilder();

            Response.Append("<br />SESSION:<br />");
            Response.Append(@"<a href=""javascript:ReverseDisplay('traceSession')"">Click to show/hide.</a>");
            Response.Append(@"<div id=""traceSession"" style=""display:none;"">");

            foreach (String var in Session)
            {
                Response.Append(var + " | ");

                if (Session[var] != null && Session[var].GetType() == typeof(System.Collections.Specialized.NameValueCollection))
                {
                    foreach (String item in (System.Collections.Specialized.NameValueCollection)Session[var])
                    {
                        Response.Append(var + "::" + item + " | ");
                        Response.Append(
                            ((System.Collections.Specialized.NameValueCollection)Session[var])[item] + "<br />");
                    }
                }
                else
                {
                    Response.Append(
                         Session[var] + "<br />"
                        );
                }
            }

            Response.Append(@"</div>");
            return Response;
        }
        private static StringBuilder PrintQuerystring()
        {
            System.Web.HttpRequest Request = System.Web.HttpContext.Current.Request;
            StringBuilder Response = new StringBuilder();

            //Need a reference to a Page object since we can't use inQS as a static...
            /*Response.Append("<br />INQS COLLECTION:<br />");
            Response.Append(@"<a href=""javascript:ReverseDisplay('traceInQS')"">Click to show/hide.</a>");
            Response.Append(@"<div id=""traceInQS"" style=""display:none;"">");

            foreach (String key in StickyParameter.inQS)
            {
                Response.Append("<br />"+  key);
            }*/

            Response.Append(@"</div>");

            Response.Append("<br />QUERYSTRING:<br />");
            Response.Append(@"<a href=""javascript:ReverseDisplay('traceQueryString')"">Click to show/hide.</a>");
            Response.Append(@"<div id=""traceQueryString"" style=""display:none;"">");

            String[] qsnames = Request.QueryString.AllKeys;
            String msg;
            Array.Sort(qsnames);
            for (int n = 0; n < qsnames.Length; n++)
            {
                msg = String.Empty;
                if (n != 0)
                {//extra line break between letters of the alphabet
                    try
                    {
                        if (qsnames[n - 1].ToCharArray()[0]
                            != qsnames[n].ToCharArray()[0])
                            msg = "<br />";
                    }
                    catch (NullReferenceException x) { msg = msg + x.Message; }
                    catch (IndexOutOfRangeException x) {msg = msg + x.Message;}

                }
                Response.Append(msg + "<br />" + qsnames[n] + " | " + Request[qsnames[n]]);
            }
            Response.Append(@"</div>");

            return Response;
        }
        private static StringBuilder PrintGlobalValues(GlobalValues globals)
        {
            System.Web.HttpRequest Request = System.Web.HttpContext.Current.Request;
            StringBuilder Response = new StringBuilder();

            Response.Append("<br />GLOBALVALUES:<br />");
            Response.Append(@"<a href=""javascript:ReverseDisplay('traceGlobals')"">Click to show/hide.</a>");
            Response.Append(@"<div id=""traceGlobals"" style=""display:none;"">");

            PropertyInfo[] propertyInfos;
            propertyInfos = typeof(GlobalValues).GetProperties();

            String msg;
            Array.Sort(propertyInfos,
                delegate(PropertyInfo propertyInfo1, PropertyInfo propertyInfo2)
                    { return propertyInfo1.Name.CompareTo(propertyInfo2.Name); });

            for (int n = 0; n < propertyInfos.Length; n++)
            {
                msg = String.Empty;
                if (n != 0)
                {//extra line break between letters of the alphabet
                    try
                    {
                        if (propertyInfos[n - 1].Name.ToCharArray()[0]
                            != propertyInfos[n].Name.ToCharArray()[0])
                            msg = "<br />";
                    }
                    catch (NullReferenceException x) { msg = msg + x.Message; }
                }
                
                Response.Append(msg + "<br />" + propertyInfos[n].Name + " | ") ;

                try
                {
                    Response.Append(((ParameterValues)propertyInfos[n].GetValue(globals, null)).Value);
                }
                catch
                {
                    try
                    {
                        Response.Append(((int)propertyInfos[n].GetValue(globals, null)).ToString());
                    }
                    catch {
                        try
                        {
                            Response.Append(((string)propertyInfos[n].GetValue(globals, null)).ToString());
                        }
                        catch
                        {
                            Response.Append(propertyInfos[n].ToString());
                        }
                    }
                }
            }
            Response.Append(@"</div>");

            return Response;
        }

        private const String SHOW_HIDE_SCRIPT =
    @"
            <script type=""text/javascript"" language=""JavaScript""><!--
                function HideContent(d) {
                document.getElementById(d).style.display = ""none"";
                }
                function ShowContent(d) {
                document.getElementById(d).style.display = ""block"";
                }
                function ReverseDisplay(d) {
                if(document.getElementById(d).style.display == ""none"") { document.getElementById(d).style.display = ""block""; }
                else { document.getElementById(d).style.display = ""none""; }
                }
            //--></script>";

        private const String SYNTAX_HIGHLIGHT_SCRIPT =
            @"
                <script type=""text/javascript"" src=""highlighter/scripts/shCore.js""></script>
                <script type=""text/javascript"" src=""highlighter/scripts/shLegacy.js""></script>
                <script type=""text/javascript"" src=""highlighter/scripts/shBrushXml.js""></script>
                <link type=""text/css"" rel=""stylesheet"" href=""highlighter/styles/shCore.css""/>
                <link type=""text/css"" rel=""stylesheet"" href=""highlighter/styles/shThemeDefault.css""/>
            <script language=""javascript"">
                window.onload = function () {
                 dp.SyntaxHighlighter.ClipboardSwf = 'highlighter/scripts/clipboard.swf';
                dp.SyntaxHighlighter.HighlightAll('code');
                }
            </script>";

    }
}
