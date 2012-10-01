namespace WebApi.CloudServices.Notifications.Sql
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class SqlEndpointContext : DbContext
    {
        public SqlEndpointContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        protected SqlEndpointContext()
        {
        }

        public virtual IDbSet<SqlEndpointTableRow> Endpoints { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder != null)
            {
                modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            }
        }
    }
}
