using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Migrations
{
    [Migration(1)]
    public class FirstMigration : Migration
    {
        public override void Down()
        {
            Delete.Table("cpumetrics");
            Delete.Table("dotnetmetrics");
            Delete.Table("hddmetrics");
            Delete.Table("networkmetrics");
            Delete.Table("rammetrics");
            Delete.Table("agents");
        }

        public override void Up()
        {
            Create.Table("cpumetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64()
                .WithColumn("agentId").AsInt32().ForeignKey("agents", "AgentId");
            Create.Table("dotnetmetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64()
                .WithColumn("agentId").AsInt32().ForeignKey("agents", "AgentId");
            Create.Table("hddmetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64()
                .WithColumn("agentId").AsInt32().ForeignKey("agents", "AgentId");
            Create.Table("networkmetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64()
                .WithColumn("agentId").AsInt32().ForeignKey("agents", "AgentId");
            Create.Table("rammetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64()
                .WithColumn("agentId").AsInt32().ForeignKey("agents", "AgentId");
            Create.Table("agents")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("AgentId").AsInt32()
                .WithColumn("AgentUrl").AsString().ForeignKey("agents", "AgentId")
                .WithColumn("Enabled").AsBoolean();
        }
    }
}
