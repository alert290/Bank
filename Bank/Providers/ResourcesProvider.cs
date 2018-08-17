using Bank.Domain.Providers;
using Bank.Resources;
using System.Web;

namespace Bank.Providers
{
    public class ResourcesProvider : IResourcesProvider
    {
        public string GetGeneralResource(string key)
        {
            return General.ResourceManager.GetString(key);
        }
    }
}