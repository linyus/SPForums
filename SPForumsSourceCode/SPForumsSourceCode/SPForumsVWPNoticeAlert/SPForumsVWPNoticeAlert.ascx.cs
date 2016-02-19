using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

namespace SPForumsSourceCode.SPForumsVWPNoticeAlert
{
    [ToolboxItemAttribute(false)]
    public partial class SPForumsVWPNoticeAlert : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public SPForumsVWPNoticeAlert()
        {
        }

        public string Link = string.Empty;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                {
                    using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        int alertNum = 0;
                        SPList list = web.Lists.TryGetList("SPForumsMessageList");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where>
                                          <And>
                                             <Eq>
                                                <FieldRef Name='Author' />
                                                <Value Type='Integer'>
                                                   <UserID Type='Integer' />
                                                </Value>
                                             </Eq>
                                             <Eq>
                                                <FieldRef Name='IsRead' />
                                                <Value Type='Text'>No</Value>
                                             </Eq>
                                          </And>
                                       </Where>";
                        SPListItemCollection items = list.GetItems(query);
                        alertNum = items.Count;

                        string Links = "<span style='background:#83BFEA; line-height: 30px; padding-right: 10px; padding-left: 10px; display: inline-block;'>You have <a style='color: red;' href='{0}'>{1}</a> message{2} unread</span>";
                        string alertLink = web.ServerRelativeUrl + "/SitePages/SPForumsMessage.aspx";

                        if (alertNum > 1)
                        {
                            Link = string.Format(Links, alertLink, alertNum.ToString(), "s");
                        }
                        else
                        {
                            Link = string.Format(Links, alertLink, alertNum.ToString(), "");
                        }
                    }
                }
            }
        }
    }
}
