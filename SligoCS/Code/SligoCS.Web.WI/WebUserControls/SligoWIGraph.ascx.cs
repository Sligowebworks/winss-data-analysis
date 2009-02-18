using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ChartFX.WebForms;

namespace SligoCS.Web.WI.WebUserControls
{
    public partial class SligoWIGraph : WIUserControlBase
    {
        private string m_temp;
        private Constants  zYearCount;
        private Constants zPoints ;

        protected void Page_Load(object sender, EventArgs e)
        {
         
          MakeGraph();
            
        }
        
        public String TestString
        {
            get { return m_temp; }
            set { m_temp = value; }
        }

        public void MakeGraph()
        {
            ChartFX.WebForms.Chart graph1 = new ChartFX.WebForms.Chart();
            graph1.Gallery = ChartFX.WebForms.Gallery.Bar;
            graph1.AxisY.Min = 0;
            graph1.AxisY.Max = 100;
            graph1.AxisY.DataFormat.Decimals = 0;
            graph1.Width = 498;
            graph1.Height = 300;
            graph1.AxisY.Title.Text =  "Percentage of students retained";
           
            graph1.Data.Series = 3;
            graph1.Data.Points = 5;
            graph1.Data[0, 1] = 30;
            graph1.Data[0, 2] = 40;
            graph1.Data[0, 3] = 60;
            graph1.Data[1, 1] = 35;
            graph1.Data[1, 2] = 60;
            graph1.Data[1, 3] = 15;
            graph1.Data[2, 1] = 30;
            graph1.Data[2, 2] = 25;
            graph1.Data[2, 3] = 70;
            graph1.Visible = true;
            /* 
              graph1.Axis(AXIS_Y).Decimals = 0;
              graph1.Axis(AXIS_Y).Grid = TRUE;
              graph1.RgbBk = RGB(255,255,255);
              graph1.Chart3D = False;

              graph1.Axis(AXIS_Y).Title = " Retention ";
              graph1.SerLegBox = TRUE;
              graph1.SerLegBoxObj.Docked = TGFP_BOTTOM;
              graph1.Fonts(CHART_TOPFT) = CF_BOLD;
              graph1.Axis(AXIS_Y).Title = "Percentage of students retained";
              graph1.OpenDataEx COD_VALUES, zYearCount, zPoints*/
        }
    }
}
