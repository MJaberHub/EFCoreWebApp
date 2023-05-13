namespace EFCoreWebApp.Helper
{
    public class SettingsConfigurationHelper
    {
        private static SettingsConfigurationHelper _appSettings;

        private string appSettingValue { get; set; }

        public static string AppSetting(string Key)
        {
            _appSettings = GetCurrentSettings("ApplicationSettings", Key);
            return _appSettings.appSettingValue;
        }

        public static string GetConnectionString(string Key)
        {
            _appSettings = GetCurrentSettings("ConnectionStrings", Key);
            return _appSettings.appSettingValue;
        }

        private SettingsConfigurationHelper(IConfiguration config, string Key)
        {
            this.appSettingValue = config.GetValue<string>(Key);
        }

        // Get a valued stored in the appsettings.
        // Pass in a key like TestArea:TestKey to get TestValue
        private static SettingsConfigurationHelper GetCurrentSettings(string section, string Key)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var settings = new SettingsConfigurationHelper(configuration.GetSection(section), Key);

            return settings;
        }
    }
}
