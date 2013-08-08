using System;
using System.Threading.Tasks;
using Cloudsdale_Win7.Win7_Lib.Helpers;

namespace Cloudsdale_Win7.Win7_Lib.Providers
{public interface IModelErrorProvider
        {
            Task OnError<T>(WebResponse<T> response);
        }

    internal class DefaultModelErrorProvider : IModelErrorProvider
    {
        public Task OnError<T>(WebResponse<T> response)
        {
            throw new NotImplementedException("Model error handler not implemented");
        }
    }
}