[assembly: WebActivator.PostApplicationStartMethod(typeof($rootnamespace$.App_Start.NotificationServiceSql), "PostStart")]

namespace $rootnamespace$.App_Start
{
    using $rootnamespace$.CloudServices.Notifications;
    using $rootnamespace$.CloudServices.Notifications.Sql;

    public static class NotificationServiceSql
    {
        public static void PostStart()
        {
            // Configure the SQL database as the storage for the Push Notifications Registration Service.
            NotificationServiceContext.Current.Configure(
                c =>
                {
                    // TODO: Replace with your own Windows Azure SQL Database or SQL Server connection string, or read it from a configuration file (Web.config or ServiceConfiguration.cscfg).
                    var connectionString = @"Data Source=.\SQLEXPRESS;Integrated Security=true;User Instance=true;AttachDBFilename=|DataDirectory|\EndpointsDB.$rootnamespace$.mdf;Initial Catalog=EndpointsDB.$rootnamespace$;MultipleActiveResultSets=True;";
					// var connectionString = @"Server=(LocalDB)\v11.0;Integrated Security=true;AttachDBFilename=|DataDirectory|\EndpointsDB.$rootnamespace$.mdf;";
					c.StorageProvider = new SqlEndpointRepository(connectionString);
                });
        }
    }
}