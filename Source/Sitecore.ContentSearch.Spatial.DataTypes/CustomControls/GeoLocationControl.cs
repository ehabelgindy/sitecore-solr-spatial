using System;
using Sitecore.Diagnostics;
using Sitecore.Shell.Applications.ContentEditor;
using Sitecore.Web.UI.Sheer;

namespace Sitecore.ContentSearch.Spatial.DataTypes.CustomControls
{
    public class GeoLocationControl : Sitecore.Shell.Applications.ContentEditor.Text, IContentField
    {

        public readonly string MapUrl = "/sitecore/shell/applications/geolocation/map.aspx";

        public string Source
        {
            get
            {
                return this.GetViewStateString("Source");
            }
            set
            {
                Assert.ArgumentNotNull(value, "value");
                string str = MainUtil.UnmapPath(value);
                if (str.EndsWith("/", StringComparison.InvariantCulture))
                    str = str.Substring(0, str.Length - 1);
                SetViewStateString("Source", str);
            }
        }

        public string ItemVersion
        {
            get
            {
                return this.GetViewStateString("Version");
            }
            set
            {
                Assert.ArgumentNotNull(value, "value");
                SetViewStateString("Version", value);
            }
        }



        public GeoLocationControl()
        {
            this.Class = "scContentControl";
            this.Activation = true;
        }


        public override void HandleMessage(Message message)
        {
            base.HandleMessage(message);
            if (message["id"] != this.ID)
                return;
            switch (message.Name)
            {
                case "geolocation:refresh":
                    RefreshMap();
                    break;
            }
        }

        private void RefreshMap()
        {
            var src = GetMapUrl();
            SheerResponse.SetAttribute(this.ID + "_map", "src", src);
            SheerResponse.Eval("scContent.startValidators()");
        }

        protected string GetMapUrl()
        {
            var latlon = Value.Split(',');
            if (latlon.Length == 2)
            {
                return string.Format("{0}?lat={1}&lon={2}&ctrlid={3}",
                                     MapUrl, latlon[0], latlon[1],this.ID);
            }
            return string.Format("{0}?ctrlid={1}",
                                     MapUrl, this.ID);
        }

        protected override void DoRender(System.Web.UI.HtmlTextWriter output)
        {
            base.DoRender(output);
            Assert.ArgumentNotNull((object)output, "output");
            string src = " src=\"" + GetMapUrl()
                            + "\"";
            string iframeId = " id=\"" + this.ID + "_map\"";
            output.Write("<div id=\"" + this.ID + "_pane\" class=\"scContentControlImagePane\" style=\"width:503px\">");
            string clientEvent = Sitecore.Context.ClientPage.GetClientEvent(this.ID + ".Browse");
            output.Write("<div class=\"scContentControlImageImage\" onclick=\"" + clientEvent + "\">");
            output.Write("<iframe" + iframeId + src +
                            " frameborder=\"0\" marginwidth=\"0\" marginheight=\"0\" width=\"100%\" height=\"380px\" allowtransparency=\"allowtransparency\"></iframe>");
            output.Write("</div>");
            output.Write("</div>");
            

        }

        public string GetValue()
        {
            return this.Value;
        }

        public void SetValue(string value)
        {
            this.Value = value;
        }

        
    }
}