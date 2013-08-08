using Cloudsdale_Win7.Win7_Lib.Models;

namespace Cloudsdale_Win7.Win7_Lib.Providers
{
    public interface ICloudServicesProvider
    {
        IStatusProvider StatusProvider(string cloudId);
        User GetBackedUser(string userId);
    }

    internal class DefaultCloudServicesProvider : ICloudServicesProvider
    {
        private static readonly DefaultStatusProvider DefaultStatusProvider = new DefaultStatusProvider();

        public IStatusProvider StatusProvider(string cloudId)
        {
            return DefaultStatusProvider;
        }

        public User GetBackedUser(string userId)
        {
            return new User(userId);
        }
    }
}
