namespace WebApi.CloudServices.Notifications.Sql
{
    using System.ComponentModel.DataAnnotations;

    public class SqlEndpointTableRow : Endpoint
    {
        [Key, Column(Order = 0)]
        public override string ApplicationId { get; set; }

        [Key, Column(Order = 1)]
        public override string TileId { get; set; }

        [Key, Column(Order = 2)]
        public override string ClientId { get; set; }
    }
}
