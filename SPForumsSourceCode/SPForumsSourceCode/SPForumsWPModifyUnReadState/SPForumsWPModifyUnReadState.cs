using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace SPForumsSourceCode.SPForumsWPModifyUnReadState
{
    [ToolboxItemAttribute(false)]
    public class SPForumsWPModifyUnReadState : WebPart
    {
        protected override void CreateChildControls()
        {
        }

        protected override void Render(HtmlTextWriter writer)
        {
            using (SPSite site = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                {
                    web.AllowUnsafeUpdates = true;
                    SPList list = web.Lists.TryGetList("SPForumsMessageList");
                    SPListItem item = list.GetItemById(Convert.ToInt32(Page.Request.QueryString["ID"]));
                    if (item["IsRead"].ToString() != "Yes")
                    {
                        if (item["Author"].ToString().Split(';')[0] == SPContext.Current.Web.CurrentUser.ID.ToString())
                        {
                            item["IsRead"] = "Yes";
                            item.Update();
                            web.AllowUnsafeUpdates = false;
                            Page.Response.Redirect(Page.Request.Url.ToString());
                        }
                        else
                        {
                            Page.Response.Redirect(web.ServerRelativeUrl + "/SitePages/SPForums.aspx");
                        }
                    }
                }
            }
        }
    }
}
