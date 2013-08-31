using CloudsdaleWin7.lib.Controllers;
using CloudsdaleWin7.lib.Models.Client;

namespace CloudsdaleWin7.lib.CloudsdaleLib
{
    public class CloudsdaleApp : App
    {
        public readonly ErrorController ErrorController = new ErrorController();
        public readonly MessageController MessageController = new MessageController();
        public readonly ModelController ModelController = new ModelController();
        public MessageHandler Faye;
        public CloudsdaleApp()
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
    }
}
