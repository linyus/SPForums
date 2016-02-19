using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
namespace SPForumsSourceCode.SPForumsVWPNotice
{
    [ToolboxItemAttribute(false)]
    public partial class SPForumsVWPNotice : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public SPForumsVWPNotice()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetUnRead();
            }
        }

        public void GetUnRead()
        {
            string recordHtml = "<div><a href='{2}' style='color:#0072c6'>[View Post]</a>&nbsp;&nbsp;<a href='{0}' {3}>{1}</a></div>";
            string resultHtml = string.Empty;
            using (SPSite site = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                {
                    SPList list = web.Lists.TryGetList("SPForumsMessageList");
                    SPQuery query = new SPQuery();
                    query.Query = @"<Where>
                                        <Eq>
                                            <FieldRef Name='Author' />
                                            <Value Type='Integer'>
                                                <UserID Type='Integer' />
                                            </Value>
                                        </Eq>
                                    </Where>";
                    SPListItemCollection items = list.GetItems(query);
                    foreach (SPListItem item in items)
                    {
                        string typeStr = string.Empty;
                        if (item["IsRead"].ToString() == "No")
                            typeStr = "style='font-weight:bold; color:black'";
                        else
                            typeStr = "style='color:#0072c6'";

                        resultHtml += string.Format(recordHtml, list.DefaultDisplayFormUrl + "?ID=" + item.ID.ToString() + "&Source" + web.ServerRelativeUrl + "/SitePages/SPForumsMessage.aspx", item["Title"].ToString(), item["MessageLink"].ToString(), typeStr);
                    }
                }
            }
            SPForumsNoticeZone.InnerHtml = resultHtml;
        }
    }
}
