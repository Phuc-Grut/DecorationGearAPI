using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DecorGearInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateImage2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageList_KeyboardDetail_KeyboardDetailID",
                table: "ImageList");

            migrationBuilder.DropForeignKey(
                name: "FK_ImageList_MouseDetail_MouseDetailID",
                table: "ImageList");

            migrationBuilder.DropIndex(
                name: "IX_ImageList_KeyboardDetailID",
                table: "ImageList");

            migrationBuilder.DropIndex(
                name: "IX_ImageList_MouseDetailID",
                table: "ImageList");

            migrationBuilder.DropColumn(
                name: "KeyboardDetailID",
                table: "ImageList");

            migrationBuilder.DropColumn(
                name: "MouseDetailID",
                table: "ImageList");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KeyboardDetailID",
                table: "ImageList",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MouseDetailID",
                table: "ImageList",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ImageList",
                keyColumn: "ImageListID",
                keyValue: 1,
                columns: new[] { "KeyboardDetailID", "MouseDetailID" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "ImageList",
                keyColumn: "ImageListID",
                keyValue: 2,
                columns: new[] { "KeyboardDetailID", "MouseDetailID" },
                values: new object[] { null, null });

            migrationBuilder.CreateIndex(
                name: "IX_ImageList_KeyboardDetailID",
                table: "ImageList",
                column: "KeyboardDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_ImageList_MouseDetailID",
                table: "ImageList",
                column: "MouseDetailID");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageList_KeyboardDetail_KeyboardDetailID",
                table: "ImageList",
                column: "KeyboardDetailID",
                principalTable: "KeyboardDetail",
                principalColumn: "KeyboardDetailID");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageList_MouseDetail_MouseDetailID",
                table: "ImageList",
                column: "MouseDetailID",
                principalTable: "MouseDetail",
                principalColumn: "MouseDetailID");
        }
    }
}
