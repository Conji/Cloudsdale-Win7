using Cloudsdale_Win7.Win7_Lib.Models;

namespace Cloudsdale_Win7.Win7_Lib.Providers
{
    public interface IStatusProvider
    {
        Status StatusForUser(string userId);
    }

    internal class DefaultStatusProvider : IStatusProvider
    {
        public Status StatusForUser(string userId)
        {
            return Status.Offline;
        }
    }
}