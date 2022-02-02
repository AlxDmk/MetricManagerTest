using FluentMigrator;
namespace MetricAgent.DAL.Migrations
{
    [Migration(1)]
    public class FirstMigtation : Migration
    {
        public override void Down()
        {
            Delete.Table("cpumetrics");
            Delete.Table("rammetrics");
            Delete.Table("hddmetrics");
            Delete.Table("dotnetmetrics");
            Delete.Table("networkmetrics");
        }

        public override void Up()
        {            

            Create.Table("cpumetrics")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();

            Create.Table("hddmetrics")
                 .WithColumn("id").AsInt64().PrimaryKey().Identity()
                 .WithColumn("Value").AsInt32()
                 .WithColumn("Time").AsInt64();

            Create.Table("networkmetrics")
                 .WithColumn("id").AsInt64().PrimaryKey().Identity()
                 .WithColumn("Value").AsInt32()
                 .WithColumn("Time").AsInt64();

            Create.Table("dotnetmetrics")
                 .WithColumn("id").AsInt64().PrimaryKey().Identity()
                 .WithColumn("Value").AsInt32()
                 .WithColumn("Time").AsInt64();

            Create.Table("rammetrics")
                 .WithColumn("id").AsInt64().PrimaryKey().Identity()
                 .WithColumn("Value").AsInt32()
                 .WithColumn("Time").AsInt64();
        }
    }
}
