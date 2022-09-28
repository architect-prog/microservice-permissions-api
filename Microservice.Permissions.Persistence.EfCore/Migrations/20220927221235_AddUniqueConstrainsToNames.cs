using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microservice.Permissions.Database.Migrations
{
    public partial class AddUniqueConstrainsToNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_permissions_area_role_permissions_id",
                table: "permissions");

            migrationBuilder.DropIndex(
                name: "ix_areas_application_id",
                table: "areas");

            migrationBuilder.CreateIndex(
                name: "ix_roles_name",
                table: "roles",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_permissions_area_role_permissions_id_name",
                table: "permissions",
                columns: new[] { "area_role_permissions_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_areas_application_id_name",
                table: "areas",
                columns: new[] { "application_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_applications_name",
                table: "applications",
                column: "name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_roles_name",
                table: "roles");

            migrationBuilder.DropIndex(
                name: "ix_permissions_area_role_permissions_id_name",
                table: "permissions");

            migrationBuilder.DropIndex(
                name: "ix_areas_application_id_name",
                table: "areas");

            migrationBuilder.DropIndex(
                name: "ix_applications_name",
                table: "applications");

            migrationBuilder.CreateIndex(
                name: "ix_permissions_area_role_permissions_id",
                table: "permissions",
                column: "area_role_permissions_id");

            migrationBuilder.CreateIndex(
                name: "ix_areas_application_id",
                table: "areas",
                column: "application_id");
        }
    }
}
