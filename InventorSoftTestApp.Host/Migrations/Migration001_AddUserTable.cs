using FluentMigrator;
using Microsoft.Data.SqlClient;

namespace InventorSoftTestApp.Migrations;

[Migration(1)]
public class Migration001_AddUserTable() : Migration
{
    public override void Up()
    {
        Create.Table("user")
            .WithColumn("id").AsInt32().Identity().PrimaryKey("user_pk")
            .WithColumn("name").AsString(50).NotNullable().Unique();
    }

    public override void Down()
    {
        Delete.Table("user");
    }
}