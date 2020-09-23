using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinDataGrabber.Enums;
using XamarinDataGrabber.Interfaces;

namespace XamarinDataGrabber.Interfaces
{
    public interface IServerService
    {
        Task<string> HandleGetRequest(HttpRequestsTypes requestType);
        Task<string> HandlePostRequest(IList<ILedConfiguration> data, HttpRequestsTypes requestType);
    }
}