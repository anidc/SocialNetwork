using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialNetwork.Migrations
{
    /// <inheritdoc />
    public partial class UserLikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23cb866e-a80f-40b7-843b-dfe3d5479976");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4391203f-e474-4859-969f-3349403e1332");

            migrationBuilder.AlterColumn<string>(
                name: "Likes",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "262bbbec-38ae-4d97-9123-54f0c8628f9e", null, "Admin", "ADMIN" },
                    { "b8dccb2c-21bd-45bb-8551-d15af384549e", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "262bbbec-38ae-4d97-9123-54f0c8628f9e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8dccb2c-21bd-45bb-8551-d15af384549e");

            migrationBuilder.AlterColumn<int>(
                name: "Likes",
                table: "Posts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "23cb866e-a80f-40b7-843b-dfe3d5479976", null, "User", "USER" },
                    { "4391203f-e474-4859-969f-3349403e1332", null, "Admin", "ADMIN" }
                });
        }
    }
}
