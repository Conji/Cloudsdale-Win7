using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudsdaleWin7.lib.Models;
using CloudsdaleWin7.lib.Providers;

namespace CloudsdaleWin7.lib.Controllers
{
    public class ModelController : IUserProvider, ICloudProvider
    {
        public readonly Dictionary<string, User> Users = new Dictionary<string, User>();
        public readonly Dictionary<string, Cloud> Clouds = new Dictionary<string, Cloud>();

        public User this[string value, SearchParameter param = SearchParameter.Id]
        {
            get
            {
                switch (param)
                {
                    case SearchParameter.Id:
                        return Users[value];
                    case SearchParameter.Name:
                        return Users.Values.First(u => String.Equals(u.Name, value, StringComparison.CurrentCultureIgnoreCase));
                    case SearchParameter.Username:
                        return Users.Values.First(u => String.Equals(u.Name, value, StringComparison.CurrentCultureIgnoreCase));
                }
                if (param == SearchParameter.Id)
                {
                    GetUser(value);
                }
                return null;
            }
        }

        public async Task<User> GetUserAsync(string id)
        {
            if (!Users.ContainsKey(id))
            {
                var user = new User(id);
                await user.ForceValidate();
                Users[id] = user;
            }
            else
            {
                await Users[id].Validate();
            }
            return Users[id];
        }

        public User GetUser(string id)
        {
            if (!Users.ContainsKey(id))
            {
                var user = new User(id);
                user.ForceValidate();
                Users[id] = user;
            }
            else
            {
                Users[id].ForceValidate();
            }
            return Users[id];
        }

        public async Task<User> UpdateDataAsync(User user, bool validate = false)
        {
            if (!Users.ContainsKey(user.Id))
            {
                await user.ForceValidate();
                Users[user.Id] = user;
            }
            else
            {
                if (validate)
                {
                    await user.ForceValidate();
                }
                user.CopyTo(Users[user.Id]);
            }
            return Users[user.Id];
        }

        public async Task<Cloud> UpdateCloudAsync(Cloud cloud)
        {
            if (!Clouds.ContainsKey(cloud.Id))
            {
                await cloud.ForceValidate();
                Clouds[cloud.Id] = cloud;
            }
            else
            {
                cloud.CopyTo(Clouds[cloud.Id]);
            }

            return Clouds[cloud.Id];
        }

        public Cloud UpdateCloud(Cloud cloud)
        {
            if (!Clouds.ContainsKey(cloud.Id))
            {
                cloud.ForceValidate();
                Clouds[cloud.Id] = cloud;
            }
            else
            {
                var cacheCloud = Clouds[cloud.Id];
                cloud.CopyTo(cacheCloud);
            }

            return Clouds[cloud.Id];
        }

        public Cloud GetCloud(string cloudId)
        {
            if (!Clouds.ContainsKey(cloudId))
            {
                var cloud = new Cloud(cloudId);
                cloud.ForceValidate();
                Clouds[cloud.Id] = cloud;
            }
            else
            {
                Clouds[cloudId].Validate();
            }

            return Clouds[cloudId];
        }
    }

    public enum SearchParameter
    {
        Id,
        Username,
        Name,
    }
}
