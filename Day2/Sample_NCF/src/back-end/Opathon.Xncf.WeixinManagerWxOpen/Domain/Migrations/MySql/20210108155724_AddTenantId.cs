using Microsoft.EntityFrameworkCore.Migrations;

namespace Opathon.Xncf.WeixinManagerWxOpen.Migrations.Migrations.MySql
{
    public partial class AddTenantId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Opathon_WeixinManagerWxOpen_Color",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Opathon_WeixinManagerWxOpen_Color");
        }
    }
}
