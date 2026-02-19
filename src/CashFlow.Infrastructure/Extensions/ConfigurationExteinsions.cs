using Microsoft.Extensions.Configuration;

namespace CashFlow.Infrastructure.Extensions
{
    public static class ConfigurationExteinsions
    {
        public static bool IsTestiEnviroment(this IConfiguration configuration )
        {
            return configuration.GetValue<bool>("InMemoryTest"); 
        }
    }
}
