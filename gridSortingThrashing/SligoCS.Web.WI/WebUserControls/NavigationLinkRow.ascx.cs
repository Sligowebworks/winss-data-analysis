using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SligoCS.Web.Base.WebServerControls.WI;
using SligoCS.Web.Base.PageBase.WI;

namespace SligoCS.Web.WI.WebUserControls
{
    [ToolboxData("<{0}:NavigationLinkRow runat=server>")]
    [PersistChildren(true)]
    public partial class NavigationLinkRow : System.Web.UI.UserControl
    {
        public NavigationLinkRow()
        {
            label = new PlaceHolder();
            links = new PlaceHolder();
            rowLabel = new Label();
        }

        private Label rowLabel;
        private NavigationLinks navigationLinks = new NavigationLinks();

        public event LinkControlAddedHandler LinkControlAdded;

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public string RowLabel 
        {
            get { return rowLabel.Text; }
            set {  rowLabel.Text = value; }
        }
        
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public NavigationLinks NavigationLinks
        {
            get { return navigationLinks; }
        }

       protected override void CreateChildControls()
        {
            label.Controls.Add(rowLabel);
            foreach (Control ctrl in navigationLinks)
            {
                links.Controls.Add(ctrl);
                if (navigationLinks.IndexOf((HyperLinkPlus)ctrl) == 0)
                {// remove the prefix from the first link of the row
                    ((HyperLinkPlus)ctrl).Prefix = null;
                }
                //raise event
                if (LinkControlAdded != null) LinkControlAdded((HyperLinkPlus) ctrl);  // NavigationLinkCallBackEvent(ctrl);
            }
        }
               
    }

    public partial class NavigationLinks : CollectionBase
    {
        public HyperLinkPlus this[int index]
        {
            get { return (HyperLinkPlus)this.List[index]; }
            set { this.List[index] = value; }
        }
        public void Add(HyperLinkPlus link)
        {
            List.Add(link);
        }
        public void AddAt(int index, HyperLinkPlus item)
        {
            this.List.Insert(index, item);
        }

        public void Remove(HyperLinkPlus item)
        {
            this.List.Remove(item);
        }

        public bool Contains(HyperLinkPlus item)
        {
            return this.List.Contains(item);
        }

        //Collection IndexOf method 
        public int IndexOf(HyperLinkPlus item)
        {
            return List.IndexOf(item);
        }

        public void CopyTo(HyperLinkPlus[] array, int index)
        {
            List.CopyTo(array, index);
        } 

    }

}