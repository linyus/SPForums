using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebPartPages;

namespace SPForumsSourceCode.Features.SPForumsSourceCodeInitSettingsFeature
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("0414fa68-c739-4362-83b7-2fcf0dc4b69a")]
    public class SPForumsSourceCodeInitSettingsFeatureEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPSite site = properties.Feature.Parent as SPSite;
            using (SPWeb web = site.OpenWeb())
            {
                SPList list = web.Lists.TryGetList("SPForumsPostList");
                list.DisableGridEditing = true;
                using (SPLimitedWebPartManager wpManager = web.GetLimitedWebPartManager(web.ServerRelativeUrl + "/SitePages/SPForums.aspx", System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared))
                {
                    //add ListViewWebPart to SPForums.aspx
                    XsltListViewWebPart xWP = new XsltListViewWebPart();
                    xWP.ListName = list.ID.ToString();
                    xWP.ListId = list.ID;
                    xWP.Title = "SPForumsPostList";
                    xWP.ChromeType = System.Web.UI.WebControls.WebParts.PartChromeType.None;
                    wpManager.AddWebPart(xWP, "Body", 1);

                    //add LeftMenu to SPForums.aspx
                    SPForumsSourceCode.SPForumsVWPLeftMenu.SPForumsVWPLeftMenu lMenu = new SPForumsVWPLeftMenu.SPForumsVWPLeftMenu();
                    lMenu.Title = "SPForumsLeftMenu";
                    lMenu.ChromeType = System.Web.UI.WebControls.WebParts.PartChromeType.None;
                    wpManager.AddWebPart(lMenu, "LeftColumn", 1);


                }

                using (SPLimitedWebPartManager wpManager = web.GetLimitedWebPartManager(web.ServerRelativeUrl + "/Lists/SPForumsPostList/DispForm.aspx", System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared))
                {
                    //add ReplyWebPart to DispForm.aspx
                    SPForumsSourceCode.SPForumsVWPReply.SPForumsVWPReply rWebPart = new SPForumsVWPReply.SPForumsVWPReply();
                    rWebPart.Title = "SPForumsReply";
                    rWebPart.ChromeType = System.Web.UI.WebControls.WebParts.PartChromeType.None;
                    wpManager.AddWebPart(rWebPart, "Main", 2);
                }

                using (SPLimitedWebPartManager wpManager = web.GetLimitedWebPartManager(web.ServerRelativeUrl + "/SitePages/SPForumsMessage.aspx", System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared))
                {
                    SPForumsSourceCode.SPForumsVWPNotice.SPForumsVWPNotice nWebPart = new SPForumsVWPNotice.SPForumsVWPNotice();
                    nWebPart.Title = "SPForumsNotice";
                    nWebPart.ChromeType = System.Web.UI.WebControls.WebParts.PartChromeType.None;
                    wpManager.AddWebPart(nWebPart, "Main", 2);
                }

                using (SPLimitedWebPartManager wpManager = web.GetLimitedWebPartManager(web.ServerRelativeUrl + "/Lists/SPForumsMessageList/DispForm.aspx", System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared))
                {
                    SPForumsSourceCode.SPForumsWPModifyUnReadState.SPForumsWPModifyUnReadState unWebPart = new SPForumsWPModifyUnReadState.SPForumsWPModifyUnReadState();
                    unWebPart.Title = "SPForumsModifyUnReadState";
                    unWebPart.ChromeType = System.Web.UI.WebControls.WebParts.PartChromeType.None;
                    wpManager.AddWebPart(unWebPart, "Main", 2);
                }
            }
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
