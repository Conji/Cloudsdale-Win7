using Cloudsdale_Win7.Win7_Lib.Models;

namespace Cloudsdale_Win7.Win7_Lib.Providers
{
    public interface ICloudProvider
    {
        Cloud GetCloud(string cloudId);
        Cloud UpdateCloud(Cloud cloud);
    }

    class DefaultCloudProvider : ICloudProvider
    {
        public Cloud GetCloud(string cloudId)
        {
            return null;
        }

        public Cloud UpdateCloud(Cloud cloud)
        {
            return cloud;
        } 
    }
}