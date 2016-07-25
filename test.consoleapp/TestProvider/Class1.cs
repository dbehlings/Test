using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Elvis.Provider.Contract;

namespace TestProvider
{
    public class Class1 : IBackgroundCheckProvider
    {
        public IEnumerable<string> Capabilities {
            get {
                yield return "One";
                yield return "Two";
                yield return "THISISNEW";
            }
        }

        public string Name {
            get {
                return "Test Provider";
            }
        }

        public Guid ProviderId {
            get {
                return new Guid("9D7E118A-DECC-4072-856E-F25E2B9A1058");
            }
        }

        public double Weight {
            get {
                return 1d;
            }
        }

        public IProviderSettings GetSettingsTemplate()
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> PostResponse(string payload)
        {
            return null;
        }

        public IWorkOrder RequestBackgroundCheck(IRequestData data)
        {
            return WorkOrder.Create("workorder", data.RequestId, data.ReferenceId, ProviderId, Name);
        }

        public IWorkOrder RequestBackgroundCheckRedress(IRequestRedressData data)
        {
            return null;
        }
    }
}
