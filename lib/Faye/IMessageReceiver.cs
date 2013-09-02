using Newtonsoft.Json.Linq;

namespace CloudsdaleWin7.lib.Faye
{
    public interface IMessageReceiver
    {
        void OnMessage(JObject message);
    }
}
