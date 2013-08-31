using System;
using System.Windows.Controls;
using CloudsdaleWin7.lib.CloudsdaleLib;
using CloudsdaleWin7.lib.Models;
using CloudsdaleWin7.lib.Models.Client;

namespace CloudsdaleWin7.lib.Controllers
{
    public class ConnectionController
    {

        public Frame MainFrame { get; set; }

        public readonly ErrorController ErrorController = new ErrorController();
        public readonly MessageController MessageController = new MessageController();
        public readonly ModelController ModelController = new ModelController();

        public ConnectionController()
        {
            Cloudsdale.ModelErrorProvider = ErrorController;
            Cloudsdale.CloudServicesProvider = MessageController;
            Cloudsdale.UserProvider = ModelController;
            Cloudsdale.CloudProvider = ModelController;

            Cloudsdale.MetadataProviders["Selected"] = new BooleanMetadataProvider();
            Cloudsdale.MetadataProviders["CloudController"] = new CloudControllerMetadataProvider();
            Cloudsdale.MetadataProviders["Status"] = new UserStatusMetadataProvider();
            Cloudsdale.MetadataProviders["IsOnline"] = new UserOnlineMetadataProvider();
        }

        public MessageHandler Faye;

        public void Navigate(Type pageType)
        {
            MainFrame.Navigate(pageType);
        }

        public void ConnectSession(Session session)
        {
            Faye.Subscribe("/users/" + session.Id + "/private");

            foreach (var cloud in session.Clouds)
            {
                Faye.Subscribe("/clouds/" + cloud.Id);
                Faye.Subscribe("/clouds/" + cloud.Id + "/chat/messages");
            }
        }
    }
}
