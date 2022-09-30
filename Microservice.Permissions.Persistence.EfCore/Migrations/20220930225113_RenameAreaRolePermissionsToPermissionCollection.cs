using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Microservice.Permissions.Database.Migrations
{
    public partial class RenameAreaRolePermissionsToPermissionCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_permissions_area_role_permissions_area_role_permissions_id",
                table: "permissions");

            migrationBuilder.DropTable(
                name: "area_role_permissions");

            migrationBuilder.CreateTable(
                name: "permission_collections",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    area_id = table.Column<int>(type: "integer", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permission_collections", x => x.id);
                    table.ForeignKey(
                        name: "fk_permission_collections_areas_area_id",
                        column: x => x.area_id,
                        principalTable: "areas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_permission_collections_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_permission_collections_area_id",
                table: "permission_collections",
                column: "area_id");

            migrationBuilder.CreateIndex(
                name: "ix_permission_collections_role_id",
                table: "permission_collections",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "fk_permissions_permission_collections_area_role_permissions_id",
                table: "permissions",
                column: "area_role_permissions_id",
                principalTable: "permission_collections",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_permissions_permission_collections_area_role_permissions_id",
                table: "permissions");

            migrationBuilder.DropTable(
                name: "permission_collections");

            migrationBuilder.CreateTable(
                name: "area_role_permissions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    area_id = table.Column<int>(type: "integer", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_area_role_permissions", x => x.id);
                    table.ForeignKey(
                        name: "fk_area_role_permissions_areas_area_id",
                        column: x => x.area_id,
                        principalTable: "areas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_area_role_permissions_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_area_role_permissions_area_id",
                table: "area_role_permissions",
                column: "area_id");

            migrationBuilder.CreateIndex(
                name: "ix_area_role_permissions_role_id",
                table: "area_role_permissions",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "fk_permissions_area_role_permissions_area_role_permissions_id",
                table: "permissions",
                column: "area_role_permissions_id",
                principalTable: "area_role_permissions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
