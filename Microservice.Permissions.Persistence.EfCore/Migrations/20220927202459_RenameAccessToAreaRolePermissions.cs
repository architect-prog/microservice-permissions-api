using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microservice.Permissions.Database.Migrations
{
    public partial class RenameAccessToAreaRolePermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_accesses_applications_application_id",
                table: "accesses");

            migrationBuilder.DropForeignKey(
                name: "fk_accesses_areas_area_id",
                table: "accesses");

            migrationBuilder.DropForeignKey(
                name: "fk_accesses_roles_role_id",
                table: "accesses");

            migrationBuilder.DropForeignKey(
                name: "fk_permissions_accesses_area_role_permissions_id",
                table: "permissions");

            migrationBuilder.DropPrimaryKey(
                name: "pk_accesses",
                table: "accesses");

            migrationBuilder.DropIndex(
                name: "ix_accesses_application_id",
                table: "accesses");

            migrationBuilder.DropColumn(
                name: "application_id",
                table: "accesses");

            migrationBuilder.RenameTable(
                name: "accesses",
                newName: "area_role_permissions");

            migrationBuilder.RenameIndex(
                name: "ix_accesses_role_id",
                table: "area_role_permissions",
                newName: "ix_area_role_permissions_role_id");

            migrationBuilder.RenameIndex(
                name: "ix_accesses_area_id",
                table: "area_role_permissions",
                newName: "ix_area_role_permissions_area_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_area_role_permissions",
                table: "area_role_permissions",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_area_role_permissions_areas_area_id",
                table: "area_role_permissions",
                column: "area_id",
                principalTable: "areas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_area_role_permissions_roles_role_id",
                table: "area_role_permissions",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_permissions_area_role_permissions_area_role_permissions_id",
                table: "permissions",
                column: "area_role_permissions_id",
                principalTable: "area_role_permissions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_area_role_permissions_areas_area_id",
                table: "area_role_permissions");

            migrationBuilder.DropForeignKey(
                name: "fk_area_role_permissions_roles_role_id",
                table: "area_role_permissions");

            migrationBuilder.DropForeignKey(
                name: "fk_permissions_area_role_permissions_area_role_permissions_id",
                table: "permissions");

            migrationBuilder.DropPrimaryKey(
                name: "pk_area_role_permissions",
                table: "area_role_permissions");

            migrationBuilder.RenameTable(
                name: "area_role_permissions",
                newName: "accesses");

            migrationBuilder.RenameIndex(
                name: "ix_area_role_permissions_role_id",
                table: "accesses",
                newName: "ix_accesses_role_id");

            migrationBuilder.RenameIndex(
                name: "ix_area_role_permissions_area_id",
                table: "accesses",
                newName: "ix_accesses_area_id");

            migrationBuilder.AddColumn<int>(
                name: "application_id",
                table: "accesses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "pk_accesses",
                table: "accesses",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_accesses_application_id",
                table: "accesses",
                column: "application_id");

            migrationBuilder.AddForeignKey(
                name: "fk_accesses_applications_application_id",
                table: "accesses",
                column: "application_id",
                principalTable: "applications",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_accesses_areas_area_id",
                table: "accesses",
                column: "area_id",
                principalTable: "areas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_accesses_roles_role_id",
                table: "accesses",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_permissions_accesses_area_role_permissions_id",
                table: "permissions",
                column: "area_role_permissions_id",
                principalTable: "accesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
