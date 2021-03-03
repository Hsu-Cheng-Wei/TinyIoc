using TinyIoc.Core;

namespace TinyIoc.Utility
{
    internal class ServiceInfoUtility
    {
        public static ServiceRegistrationInfo CreateServiceInfo(Service service)
        {
            var info = new ServiceRegistrationInfo(service);

            return info;
        }
    }
}
