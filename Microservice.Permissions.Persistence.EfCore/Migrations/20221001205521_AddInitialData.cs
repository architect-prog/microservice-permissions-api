using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microservice.Permissions.Database.Migrations
{
    public partial class AddInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "roles",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "permissions",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(32)",
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "areas",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "applications",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "applications",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 1, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", "microservice-messaging" },
                    { 2, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", "microservice-permissions" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Client" },
                    { 2, "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "areas",
                columns: new[] { "id", "application_id", "name" },
                values: new object[,]
                {
                    { 1, 1, "plans" },
                    { 2, 1, "messages" },
                    { 3, 2, "values" },
                    { 4, 2, "charts" }
                });

            migrationBuilder.InsertData(
                table: "permission_collections",
                columns: new[] { "id", "area_id", "role_id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 1 },
                    { 4, 4, 1 },
                    { 5, 1, 2 },
                    { 6, 2, 2 },
                    { 7, 3, 2 },
                    { 8, 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id", "have_access", "name", "permission_collection_id" },
                values: new object[,]
                {
                    { 1, true, "can_download", 1 },
                    { 2, false, "can_download", 5 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "permission_collections",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "permission_collections",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "permission_collections",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "permission_collections",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "permission_collections",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "permission_collections",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "areas",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "areas",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "areas",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "permission_collections",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "permission_collections",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "applications",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "areas",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "applications",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "roles",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "permissions",
                type: "character varying(32)",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "areas",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "applications",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);
        }
    }
}
