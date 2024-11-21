using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AlspecJobs.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[,]
                {
                    { "job-1", "Description for Job 1", "Job 1 Title" },
                    { "job-2", "Description for Job 2", "Job 2 Title" }
                });

            migrationBuilder.InsertData(
                table: "SubItems",
                columns: new[] { "ItemId", "Description", "JobId", "JobId1", "Status", "Title" },
                values: new object[,]
                {
                    { "item-1", "In progress sub item", "job-1", null, "InProgress", "SubItem 1" },
                    { "item-2", "Pending sub item", "job-1", null, "Pending", "SubItem 2" },
                    { "item-3", "Completed SubItem", "job-2", null, "Completed", "SubItem 3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SubItems",
                keyColumn: "ItemId",
                keyValue: "item-1");

            migrationBuilder.DeleteData(
                table: "SubItems",
                keyColumn: "ItemId",
                keyValue: "item-2");

            migrationBuilder.DeleteData(
                table: "SubItems",
                keyColumn: "ItemId",
                keyValue: "item-3");

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: "job-1");

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: "job-2");
        }
    }
}
