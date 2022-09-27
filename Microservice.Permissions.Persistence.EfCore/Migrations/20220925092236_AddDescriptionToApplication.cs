using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microservice.Permissions.Database.Migrations
{
    public partial class AddDescriptionToApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_permissions_accesses_access_id",
                table: "permissions");

            migrationBuilder.RenameColumn(
                name: "access_id",
                table: "permissions",
                newName: "area_role_permissions_id");

            migrationBuilder.RenameIndex(
                name: "ix_permissions_access_id",
                table: "permissions",
                newName: "ix_permissions_area_role_permissions_id");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "applications",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_permissions_accesses_area_role_permissions_id",
                table: "permissions",
                column: "area_role_permissions_id",
                principalTable: "accesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_permissions_accesses_area_role_permissions_id",
                table: "permissions");

            migrationBuilder.DropColumn(
                name: "description",
                table: "applications");

            migrationBuilder.RenameColumn(
                name: "area_role_permissions_id",
                table: "permissions",
                newName: "access_id");

            migrationBuilder.RenameIndex(
                name: "ix_permissions_area_role_permissions_id",
                table: "permissions",
                newName: "ix_permissions_access_id");

            migrationBuilder.AddForeignKey(
                name: "fk_permissions_accesses_access_id",
                table: "permissions",
                column: "access_id",
                principalTable: "accesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
