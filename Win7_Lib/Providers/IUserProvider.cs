using Cloudsdale_Win7.Win7_Lib.Models;

namespace Cloudsdale_Win7.Win7_Lib.Providers
{
    public interface IUserProvider
    {
        User GetUser(string userId);
    }

    class DefaultUserProvider : IUserProvider
    {
        public User GetUser(string userId)
        {
            return null;
        }
    }
}