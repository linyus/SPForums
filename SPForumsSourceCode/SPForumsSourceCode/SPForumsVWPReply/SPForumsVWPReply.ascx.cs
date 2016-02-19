using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

namespace SPForumsSourceCode.SPForumsVWPReply
{
    [ToolboxItemAttribute(false)]
    public partial class SPForumsVWPReply : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public SPForumsVWPReply()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            initAllReply();
        }

        public void initAllReply()
        {
            string allReplyHtml = string.Empty;
            using (SPSite site = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                {
                    SPList list = web.Lists.TryGetList("SPForumsReplyList");
                    SPQuery query = new SPQuery();
                    query.Query = @"<Where>
                                      <Eq>
                                         <FieldRef Name='Title' />
                                         <Value Type='Text'>" + Page.Request.QueryString["ID"] + @"</Value>
                                      </Eq>
                                   </Where>";
                    SPListItemCollection itemcoll = list.GetItems(query);
                    int Num = 1;
                    foreach (SPListItem item in itemcoll)
                    {
                        string User = item["Inviter"] == null ? string.Empty : item["Inviter"].ToString();
                        if (!string.IsNullOrEmpty(User))
                        {
                            string[] Users = User.Split('#');
                            User = "Inviter:";
                            for (int i = 1; i < Users.Length; i = i + 2)
                            {
                                User += Users[i];
                            }
                            User += "<br/>";
                        }
                        allReplyHtml += "Floor " + Num.ToString() + ":<br/>" + item["Reply"].ToString() + "<br/>" + User + "<br/>";
                        Num++;
                    }
                }
            }
            AllReply.InnerHtml = allReplyHtml;
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            using (SPSite site = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                {
                    SPList list = web.Lists.TryGetList("SPForumsReplyList");
                    SPListItem itemAdd = list.Items.Add();
                    itemAdd["Title"] = Page.Request.QueryString["ID"].ToString();
                    itemAdd["Reply"] = tb_Reply.Text;
                    string users = string.Empty;

                    for (int i = 0; i < pplPickerSiteRequestor.AllEntities.Count; i++)
                    {
                        SPUser user = web.EnsureUser(pplPickerSiteRequestor.AllEntities[i].DisplayText);
                        users += user.ID.ToString() + ";#" + pplPickerSiteRequestor.AllEntities[i].DisplayText + ";#";
                    }

                    if (!string.IsNullOrEmpty(users))
                    {
                        users = users.Substring(0, users.Length - 2);
                        itemAdd["Inviter"] = users;
                    }

                    itemAdd.Update();
                    Page.Response.Redirect(Page.Request.Url.ToString());
                }
            }
        }
    }
}
