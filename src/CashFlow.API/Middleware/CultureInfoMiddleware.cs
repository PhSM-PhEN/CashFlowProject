using System.Globalization;

namespace CashFlow.API.Middleware
{
    public class CultureInfoMiddleware
    {
        private readonly RequestDelegate _next;

        public CultureInfoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();
            var acceptLanguageHeader = context.Request.Headers.AcceptLanguage.FirstOrDefault();

            var cultureInfo = new CultureInfo("en");

            if (!string.IsNullOrEmpty(acceptLanguageHeader))
            {
                // Pega o primeiro idioma da lista, ignorando pesos (q=)
                var requestedCulture = acceptLanguageHeader.Split(',').FirstOrDefault()?.Split(';').FirstOrDefault();

                if (!string.IsNullOrEmpty(requestedCulture) &&
                    supportedLanguages.Exists(l => l.Name.Equals(requestedCulture, StringComparison.OrdinalIgnoreCase)))
                {
                    cultureInfo = new CultureInfo(requestedCulture);
                }
            }

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            await _next(context);
        }
    }
}
