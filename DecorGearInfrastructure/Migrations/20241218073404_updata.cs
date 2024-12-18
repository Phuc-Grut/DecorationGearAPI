using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DecorGearInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductSubCategory",
                columns: new[] { "ProductID", "SubCategoryID", "CreatedBy", "CreatedTime", "Deleted", "DeletedBy", "DeletedTime", "ModifiedBy", "ModifiedTime" },
                values: new object[] { 3, 3, null, null, false, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductSubCategory",
                keyColumns: new[] { "ProductID", "SubCategoryID" },
                keyValues: new object[] { 3, 3 });
        }
    }
}
