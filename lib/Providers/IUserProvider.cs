using System.Net.Http;
using CloudsdaleWin7.lib.Helpers;
using CloudsdaleWin7.lib.Models;
using Newtonsoft.Json;

namespace CloudsdaleWin7.lib.Providers
{
    public interface IUserProvider
    {
        User GetUser(string userId);
    }

    class DefaultUserProvider : IUserProvider
    {
        public User GetUser(string userId)
        {
            var client = new HttpClient().AcceptsJson();
            return
                JsonConvert.DeserializeObjectAsync<WebResponse<User>>(
                    client.GetStringAsync(Endpoints.User.Replace("[:id]", userId)).Result).Result.Result;
        }
    }
}