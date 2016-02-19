using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace SPForumsSourceCode.SPForumsReplyList.EventReceiverForReplyList
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class EventReceiverForReplyList : SPItemEventReceiver
    {
        /// <summary>
        /// An item was added.
        /// </summary>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            SPFieldUserValueCollection users = (SPFieldUserValueCollection)properties.ListItem["Inviter"];
            foreach (SPFieldUserValue user in users)
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(properties.Site.ID, user.User.UserToken))
                    {
                        using (SPWeb web = site.OpenWeb(properties.Web.ID))
                        {
                            SPList list = web.Lists.TryGetList("SPForumsMessageList");
                            SPList post = web.Lists.TryGetList("SPForumsPostList");
                            SPListItem postItem = post.GetItemById(Convert.ToInt32(properties.ListItem["Title"]));
                            SPListItem itemAdd = list.Items.Add();
                            itemAdd["Title"] = "[Reply Invite]Post Title:" + postItem["Title"];
                            itemAdd["MessageLink"] = post.DefaultDisplayFormUrl + "?ID=" + properties.ListItem["Title"];
                            itemAdd["ReplyContent"] = properties.ListItem["Reply"];
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