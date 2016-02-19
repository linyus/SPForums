using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace SPForumsSourceCode.SPForumsPostList.EventReceiverForMessageList
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class EventReceiverForMessageList : SPItemEventReceiver
    {
        /// <summary>
        /// An item was added.
        /// </summary>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            SPFieldUserValueCollection users = (SPFieldUserValueCollection)properties.ListItem["Inviters"];
            foreach (SPFieldUserValue user in users)
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(properties.Site.ID, user.User.UserToken))
                    {
                        using (SPWeb web = site.OpenWeb(properties.Web.ID))
                        {
                            SPList list = web.Lists.TryGetList("SPForumsMessageList");
                            SPListItem itemAdd = list.Items.Add();
                            itemAdd["Title"] = "[Post Invite]Post Title:" + properties.ListItem["Title"];
                            itemAdd["MessageLink"] = properties.List.DefaultDisplayFormUrl + "?Id=" + properties.ListItemId.ToString();
                            itemAdd["IsRead"] = "No";
                            itemAdd.SystemUpdate();
                        }
                    }
                });
            }
            //base.ItemAdded(properties);
        }


    }
}