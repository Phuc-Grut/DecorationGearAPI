using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DecorGearInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_database2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductSubCategory",
                keyColumns: new[] { "ProductID", "SubCategoryID" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Product");

            migrationBuilder.AlterColumn<string>(
                name: "AvatarProduct",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Material",
                table: "MouseDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Dimensions",
                table: "MouseDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "DPI",
                table: "MouseDetail",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Connectivity",
                table: "MouseDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "MouseDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "BatteryCapacity",
                table: "MouseDetail",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "MouseDetail",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "MouseDetail",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "MouseDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Switch",
                table: "MouseDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "MouseDetail",
                type: "float",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Switch",
                table: "KeyboardDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Layout",
                table: "KeyboardDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "KeyboardDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Case",
                table: "KeyboardDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "BatteryCapacity",
                table: "KeyboardDetail",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "KeyboardDetail",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "KeyboardDetail",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "KeyboardDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "KeyboardDetail",
                type: "float",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ImageList",
                keyColumn: "ImageListID",
                keyValue: 1,
                columns: new[] { "Description", "ImagePath", "ProductID" },
                values: new object[] { "", "/media/product/250-73-vien-.jpg", 1 });

            migrationBuilder.UpdateData(
                table: "ImageList",
                keyColumn: "ImageListID",
                keyValue: 2,
                columns: new[] { "Description", "ImagePath", "ProductID" },
                values: new object[] { "", "/media/product/250-73-1089_chu___t_ch__i_game_razer_deathaddder_essential_2_min.jpg", 1 });

            migrationBuilder.InsertData(
                table: "ImageList",
                columns: new[] { "ImageListID", "CreatedBy", "CreatedTime", "Deleted", "DeletedBy", "DeletedTime", "Description", "ImagePath", "ModifiedBy", "ModifiedTime", "ProductID" },
                values: new object[,]
                {
                    { 3, null, null, false, null, null, "", "/media/product/250-73-untitled-3.jpg", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 4, null, null, false, null, null, "", "/media/product/250-88-d1470721f92bc58866c35b39f7e83f5f.jpg", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 5, null, null, false, null, null, "", "/media/product/250-89-69a7ff353e943ca00e99246f76d3222c.jpg", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 6, null, null, false, null, null, "", "/media/product/250-90-b865462f9328e9130f5c420e14e492c5.jpg", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 7, null, null, false, null, null, "", "/media/product/250-4027-vien1.jpg", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3 },
                    { 8, null, null, false, null, null, "", "/media/product/250-4027-xanh-la-6.jpg", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3 },
                    { 9, null, null, false, null, null, "", "/media/product/250-4026-vein3.jpg", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3 },
                    { 10, null, null, false, null, null, "", "/media/product/250-4029-vien4.jpg", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3 }
                });

            migrationBuilder.UpdateData(
                table: "KeyboardDetail",
                keyColumn: "KeyboardDetailID",
                keyValue: 1,
                columns: new[] { "BatteryCapacity", "Color", "Layout", "Led", "Price", "Quantity", "Size", "Switch", "Weight" },
                values: new object[] { 0, "WHITE", "98 Phím", "LED RAINBOW nhiều chế độ", 100000.0, 10, "Kích thước bàn phím : 388*139*37 (mm)", "BLUE SWITCH", 500.0 });

            migrationBuilder.UpdateData(
                table: "KeyboardDetail",
                keyColumn: "KeyboardDetailID",
                keyValue: 2,
                columns: new[] { "BatteryCapacity", "Color", "Layout", "Led", "Price", "Quantity", "Size", "Switch", "Weight" },
                values: new object[] { null, "RED", "98 phím", "LED RAINBOW nhiều chế độ", 80000.0, 10, null, "RED SWITCH", null });

            migrationBuilder.InsertData(
                table: "KeyboardDetail",
                columns: new[] { "KeyboardDetailID", "BatteryCapacity", "Case", "Color", "CreatedBy", "CreatedTime", "Deleted", "DeletedBy", "DeletedTime", "KeycapMaterial", "Layout", "Led", "ModifiedBy", "ModifiedTime", "PCB", "Price", "ProductID", "Quantity", "SS", "Size", "Stabilizes", "Switch", "SwitchLife", "SwitchMaterial", "Weight" },
                values: new object[] { 3, null, "Nhựa", "GRAY", null, null, false, null, null, "ABS", "108 phím", "LED RAINBOW nhiều chế độ", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "PCB tiêu chuẩn", 100000.0, 3, 10, "VIA", null, "Không", "RED SWITCH", 60000000, "Nhựa", null });

            migrationBuilder.UpdateData(
                table: "MouseDetail",
                keyColumn: "MouseDetailID",
                keyValue: 1,
                columns: new[] { "BatteryCapacity", "LED", "Price", "Quantity", "Size", "Switch", "Weight" },
                values: new object[] { 0, "Đèn LED 1 màu WHITE Hỗ trợ Razer Synapse", 520000.0, 10, null, null, 105.0 });

            migrationBuilder.UpdateData(
                table: "MouseDetail",
                keyColumn: "MouseDetailID",
                keyValue: 2,
                columns: new[] { "BatteryCapacity", "Connectivity", "LED", "Price", "ProductID", "Quantity", "SS", "Size", "Switch", "Weight" },
                values: new object[] { 0, "USB", "Đèn LED 1 màu WHITE Hỗ trợ Razer Synapse", 520000.0, 1, 10, "Razer Synapse", null, null, 105.0 });

            migrationBuilder.InsertData(
                table: "MouseDetail",
                columns: new[] { "MouseDetailID", "BatteryCapacity", "Button", "Color", "Connectivity", "CreatedBy", "CreatedTime", "DPI", "Deleted", "DeletedBy", "DeletedTime", "Dimensions", "EyeReading", "LED", "Material", "ModifiedBy", "ModifiedTime", "Price", "ProductID", "Quantity", "SS", "Size", "Switch", "Weight" },
                values: new object[,]
                {
                    { 3, 0, 5, "Đen", "USB", null, null, 12000, false, null, null, "115mm x 58mm x 38mm", "500Hz", null, "Kim loại", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 190000.0, 1, 10, "Razer Synapse REP", null, null, 105.0 },
                    { 4, 930, 5, "Đen", "USB, 2.4G", null, null, 6000, false, null, null, "115mm x 58mm x 38mm", "500Hz", null, "Nhựa", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 790000.0, 2, 10, "Razer Synapse REP", "125.5*68.6*39.6mm", "DareU (10 triệu lần click)", 105.0 },
                    { 5, 930, 5, "Trắng Xanh", "USB, 2.4G", null, null, 6000, false, null, null, "115mm x 58mm x 38mm", "500Hz", "LED RGB", "Nhựa", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 790000.0, 2, 10, "Chuột DareU EM901X RGB Superlight Wireless", "125.5*68.6*39.6mm", "DareU (10 triệu lần click)", 105.0 },
                    { 6, 930, 5, "Trắng", "USB, 2.4G", null, null, 6000, false, null, null, "115mm x 58mm x 38mm", "500Hz", null, "Nhựa", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 590000.0, 2, 10, "Chuột DareU EM901X RGB Superlight Wireless", "125.5*68.6*39.6mm", "DareU (10 triệu lần click)", 105.0 }
                });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 1,
                columns: new[] { "AvatarProduct", "ProductName" },
                values: new object[] { "/media/product/250-73-vien-.jpg", "CHUỘT RAZER DEATHADDER ESSENTIAL" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 2,
                columns: new[] { "AvatarProduct", "ProductName" },
                values: new object[] { "/media/product/250-90-b865462f9328e9130f5c420e14e492c5.jpg", "CHUỘT KHÔNG DÂY DAREU EM901" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 3,
                columns: new[] { "AvatarProduct", "ProductName" },
                values: new object[] { "/media/product/250-4027-vien1.jpg", "BÀN PHÍM CƠ ESONNE K98" });

            migrationBuilder.UpdateData(
                table: "SubCategory",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "SubCategoryName",
                value: "Bàn Phím Cơ");

            migrationBuilder.UpdateData(
                table: "SubCategory",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "SubCategoryName",
                value: "Bàn Không Phím Dây");

            migrationBuilder.InsertData(
                table: "SubCategory",
                columns: new[] { "SubCategoryID", "CategoryID", "CreatedBy", "CreatedTime", "Deleted", "DeletedBy", "DeletedTime", "ModifiedBy", "ModifiedTime", "SubCategoryName" },
                values: new object[] { 7, 1, null, null, false, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Chuột DAREU" });

            migrationBuilder.InsertData(
                table: "ProductSubCategory",
                columns: new[] { "ProductID", "SubCategoryID", "CreatedBy", "CreatedTime", "Deleted", "DeletedBy", "DeletedTime", "ModifiedBy", "ModifiedTime" },
                values: new object[] { 2, 7, null, null, false, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ImageList",
                keyColumn: "ImageListID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ImageList",
                keyColumn: "ImageListID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ImageList",
                keyColumn: "ImageListID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ImageList",
                keyColumn: "ImageListID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ImageList",
                keyColumn: "ImageListID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ImageList",
                keyColumn: "ImageListID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ImageList",
                keyColumn: "ImageListID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ImageList",
                keyColumn: "ImageListID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "KeyboardDetail",
                keyColumn: "KeyboardDetailID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MouseDetail",
                keyColumn: "MouseDetailID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MouseDetail",
                keyColumn: "MouseDetailID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MouseDetail",
                keyColumn: "MouseDetailID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MouseDetail",
                keyColumn: "MouseDetailID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductSubCategory",
                keyColumns: new[] { "ProductID", "SubCategoryID" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "SubCategoryID",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "BatteryCapacity",
                table: "MouseDetail");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "MouseDetail");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "MouseDetail");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "MouseDetail");

            migrationBuilder.DropColumn(
                name: "Switch",
                table: "MouseDetail");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "MouseDetail");

            migrationBuilder.DropColumn(
                name: "BatteryCapacity",
                table: "KeyboardDetail");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "KeyboardDetail");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "KeyboardDetail");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "KeyboardDetail");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "KeyboardDetail");

            migrationBuilder.AlterColumn<string>(
                name: "AvatarProduct",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Product",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "Product",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "Material",
                table: "MouseDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Dimensions",
                table: "MouseDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DPI",
                table: "MouseDetail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Connectivity",
                table: "MouseDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "MouseDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Switch",
                table: "KeyboardDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Layout",
                table: "KeyboardDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "KeyboardDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Case",
                table: "KeyboardDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "ImageList",
                keyColumn: "ImageListID",
                keyValue: 1,
                columns: new[] { "Description", "ImagePath", "ProductID" },
                values: new object[] { "Hình ảnh của sản phẩm aulaf75", "/images/aulaf75_img2.jpg", 2 });

            migrationBuilder.UpdateData(
                table: "ImageList",
                keyColumn: "ImageListID",
                keyValue: 2,
                columns: new[] { "Description", "ImagePath", "ProductID" },
                values: new object[] { "Hình ảnh của sản phẩm razer deadth addzer v3", "/images/rzdav3_img2.jpg", 2 });

            migrationBuilder.UpdateData(
                table: "KeyboardDetail",
                keyColumn: "KeyboardDetailID",
                keyValue: 1,
                columns: new[] { "Color", "Layout", "Led", "Switch" },
                values: new object[] { "Red", "80%", "RGB", "Cherry MX Red" });

            migrationBuilder.UpdateData(
                table: "KeyboardDetail",
                keyColumn: "KeyboardDetailID",
                keyValue: 2,
                columns: new[] { "Color", "Layout", "Led", "Switch" },
                values: new object[] { "Black", "75%", "Đơn sắc", "Gateron Brown" });

            migrationBuilder.UpdateData(
                table: "MouseDetail",
                keyColumn: "MouseDetailID",
                keyValue: 1,
                column: "LED",
                value: "RGB");

            migrationBuilder.UpdateData(
                table: "MouseDetail",
                keyColumn: "MouseDetailID",
                keyValue: 2,
                columns: new[] { "Connectivity", "LED", "ProductID", "SS" },
                values: new object[] { "Bluetooth", "Đơn sắc", 2, "Logitech G HUB" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 1,
                columns: new[] { "AvatarProduct", "Price", "ProductName", "Quantity", "Size", "Weight" },
                values: new object[] { "/media/product/250-6041-1.jpg", 405.80000000000001, "Chuột gaming Razer death adder v3", 100, "M", 500.0 });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 2,
                columns: new[] { "AvatarProduct", "Price", "ProductName", "Quantity", "Size", "Weight" },
                values: new object[] { "/media/product/250-58-700c523eec2d560efd44f277bf6559ac.jpg", 2000000.0, "Chuột gaming không dây Razer mini pro 1", 100, "M", 350.0 });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 3,
                columns: new[] { "AvatarProduct", "Price", "ProductName", "Quantity", "Size", "Weight" },
                values: new object[] { "/media/product/250-6152-untitled-28_upscayl_2x_realesrgan-x4plus.png", 1000000.0, "Bàn phím cơ AulaF75", 100, "M", 400.0 });

            migrationBuilder.InsertData(
                table: "ProductSubCategory",
                columns: new[] { "ProductID", "SubCategoryID", "CreatedBy", "CreatedTime", "Deleted", "DeletedBy", "DeletedTime", "ModifiedBy", "ModifiedTime" },
                values: new object[] { 2, 1, null, null, false, null, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "SubCategory",
                keyColumn: "SubCategoryID",
                keyValue: 3,
                column: "SubCategoryName",
                value: "Bàn Phím Aula");

            migrationBuilder.UpdateData(
                table: "SubCategory",
                keyColumn: "SubCategoryID",
                keyValue: 4,
                column: "SubCategoryName",
                value: "Bàn Phím Rainy");
        }
    }
}
