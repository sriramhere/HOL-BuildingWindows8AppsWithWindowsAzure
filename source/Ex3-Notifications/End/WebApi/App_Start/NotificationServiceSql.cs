[assembly: WebActivator.PostApplicationStartMethod(typeof(WebApi.App_Start.NotificationServiceSql), "PostStart")]

namespace WebApi.App_Start
{
    using WebApi.CloudServices.Notifications;
    using WebApi.CloudServices.Notifications.Sql;

    public static class NotificationServiceSql
    {
        public static void PostStart()
        {
            // Configure the SQL database as the storage for the Push Notifications Registration Service.
            NotificationServiceContext.Current.Configure(
                c =>
                {
                    var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
					c.StorageProvider = new SqlEndpointRepository(connectionString);
                });
        }
    }
}