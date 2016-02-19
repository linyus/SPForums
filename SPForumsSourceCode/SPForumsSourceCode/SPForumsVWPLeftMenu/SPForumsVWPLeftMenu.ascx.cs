using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebPartPages;

namespace SPForumsSourceCode.SPForumsVWPLeftMenu
{
    [ToolboxItemAttribute(false)]
    public partial class SPForumsVWPLeftMenu : System.Web.UI.WebControls.WebParts.WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public SPForumsVWPLeftMenu()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        public string typeId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    string LeftMenuHtml = string.Empty;
                    string CurrentUrl = string.Empty;
                    CurrentUrl = Page.Request.Url.ToString();
                    if (CurrentUrl.IndexOf('?') > 0)
                    {
                        CurrentUrl = CurrentUrl.Substring(0, CurrentUrl.IndexOf('?'));
                    }
                    if (CurrentUrl.IndexOf('#') > 0)
                    {
                        CurrentUrl = CurrentUrl.Substring(0, CurrentUrl.IndexOf('#'));
                    }

                    string TempHtml = "<a onclick='LMenuChoose(this)' href='" + CurrentUrl + "#InplviewHash{2}=FilterField1%3DPostType-FilterValue1%3D{0}' target='_self' title='{1}'>{1}</a>";

                    using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                        {
                            SPList SPForumsPostType = web.Lists.TryGetList("SPForumsPostTypeList");
                            SPList SPForumsPost = web.Lists.TryGetList("SPForumsPostList");
                            typeId = SPForumsPostType.ID.ToString().ToUpper();
                            if (SPForumsPostType != null)
                            {
                                SPListItemCollection SPForumsTypes = SPForumsPostType.Items;
                                string ViewId = GetViewId();
                                LeftMenuHtml = "<table Id='LeftMenuTableZone' style='width: 100%' cellpadding='0' cellspacing='10'>";
                                LeftMenuHtml += "<tr><td><a onclick='LMenuChoose(this)' style='color:black;background-color:#8EC1E5' href='" + CurrentUrl + "#InplviewHash" + ViewId + "' target='_self' title='All Type'>All Type</a></td></tr>";

                                foreach (SPListItem item in SPForumsTypes)
                                {
                                    LeftMenuHtml += "<tr><td>" + string.Format(TempHtml, item["Title"], item["Title"], ViewId) + "</td></tr>";
                                }

                                LeftMenuHtml += "</table>";
                            }

                        }
                    }
                    SPForumsLeftMenuZone.InnerHtml = LeftMenuHtml;

                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
        }

        public string GetViewId()
        {
            string ViewId = string.Empty;
            using (SPSite site = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                {
                    SPFile thePage = web.GetFile(Page.Request.Url.ToString());
                    SPLimitedWebPartManager theWebPartManager = thePage.GetLimitedWebPartManager(System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared);
                    SPLimitedWebPartCollection WebPartColl = theWebPartManager.WebParts;
                    for (int i = 0; i < WebPartColl.Count; i++)
                    {
                        if (WebPartColl[i].GetType().ToString() == "Microsoft.SharePoint.WebPartPages.XsltListViewWebPart")
                            ViewId += WebPartColl[i].ClientID.ToString().Replace('_', '-').Substring(2);
                    }
                }
            }
            SPForumsLeftMenuZone.InnerHtml += ViewId;
            return ViewId;
        }
    }
}
