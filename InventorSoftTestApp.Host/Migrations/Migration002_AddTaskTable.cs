using System.Data;
using FluentMigrator;
using Microsoft.EntityFrameworkCore;

namespace InventorSoftTestApp.Migrations;

[Migration(2)]
public class Migration002_AddTaskTable : Migration
{
    public override void Up()
    {
        Create.Table("task")
            .WithColumn("id").AsInt32().Identity().PrimaryKey("task_pk")
            .WithColumn("user_id").AsInt32().Nullable()
            .WithColumn("description").AsString(2000).NotNullable()
            .WithColumn("state").AsString().NotNullable()
            .WithColumn("transfer_counter").AsInt32().NotNullable()
            .WithColumn("created_at").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime);


        Create.ForeignKey("fk_task_user")
            .FromTable("task").ForeignColumn("user_id")
            .ToTable("user").PrimaryColumn("id").OnDelete(Rule.SetNull);
    }

    public override void Down()
    {
        Delete.Table("task");
    }
}