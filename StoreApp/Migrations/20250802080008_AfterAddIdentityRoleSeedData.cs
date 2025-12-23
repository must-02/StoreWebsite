using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StoreApp.Migrations
{
    /// <inheritdoc />
    public partial class AfterAddIdentityRoleSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8ff8d138-578c-4bbb-a32b-d43f52e7ba69", null, "User", "USER" },
                    { "d4953f4f-667e-40c9-8c1f-1bef757f6de2", null, "Editor", "EDITOR" },
                    { "ffa4b7c4-10b7-4d97-b586-c663780f763d", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ff8d138-578c-4bbb-a32b-d43f52e7ba69");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4953f4f-667e-40c9-8c1f-1bef757f6de2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ffa4b7c4-10b7-4d97-b586-c663780f763d");
        }
    }
}
