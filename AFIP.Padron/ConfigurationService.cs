using System.Configuration;

namespace AFIP.Padron
{
    public static class ConfigurationService
    {
        public static string GetConfigValue(string key)
        {
            var expression = string.Empty;
            var configuredExpression = ConfigurationManager.AppSettings[key];
            if (configuredExpression != null)
            {
                expression = configuredExpression.ToString();
            }

            return expression;
        }
    }
}
