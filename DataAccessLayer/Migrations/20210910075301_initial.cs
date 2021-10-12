using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "shared");

            migrationBuilder.CreateSequence<int>(
                name: "Code",
                schema: "shared");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    passwordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    passwordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryID = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy", "firstName", "lastName", "password", "passwordHash", "passwordSalt", "token", "username" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "User1", "User1", "password123", new byte[] { 226, 10, 71, 255, 0, 247, 233, 169, 146, 105, 140, 235, 162, 89, 228, 7, 239, 239, 228, 185, 201, 135, 125, 90, 127, 201, 109, 199, 1, 4, 203, 92, 185, 91, 220, 81, 207, 28, 135, 102, 8, 83, 171, 146, 45, 43, 86, 59, 141, 114, 148, 90, 149, 154, 81, 128, 64, 115, 167, 155, 9, 82, 20, 162 }, new byte[] { 3, 34, 27, 243, 186, 121, 116, 25, 202, 42, 95, 133, 70, 68, 16, 71, 178, 157, 88, 248, 53, 149, 2, 108, 51, 174, 58, 144, 61, 189, 233, 43, 208, 166, 184, 115, 159, 135, 221, 41, 49, 60, 27, 47, 3, 118, 92, 125, 82, 39, 140, 137, 14, 31, 118, 165, 196, 8, 22, 238, 81, 156, 21, 52, 101, 197, 245, 202, 219, 157, 207, 192, 27, 20, 214, 9, 194, 90, 53, 43, 75, 18, 95, 102, 204, 187, 151, 85, 230, 160, 38, 120, 178, 97, 190, 60, 156, 91, 2, 199, 31, 218, 1, 74, 138, 68, 128, 14, 112, 201, 11, 255, 145, 122, 79, 234, 226, 35, 133, 52, 64, 50, 96, 169, 104, 144, 209, 40 }, null, "User1@User1" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "User2", "User2", "password123", new byte[] { 226, 10, 71, 255, 0, 247, 233, 169, 146, 105, 140, 235, 162, 89, 228, 7, 239, 239, 228, 185, 201, 135, 125, 90, 127, 201, 109, 199, 1, 4, 203, 92, 185, 91, 220, 81, 207, 28, 135, 102, 8, 83, 171, 146, 45, 43, 86, 59, 141, 114, 148, 90, 149, 154, 81, 128, 64, 115, 167, 155, 9, 82, 20, 162 }, new byte[] { 3, 34, 27, 243, 186, 121, 116, 25, 202, 42, 95, 133, 70, 68, 16, 71, 178, 157, 88, 248, 53, 149, 2, 108, 51, 174, 58, 144, 61, 189, 233, 43, 208, 166, 184, 115, 159, 135, 221, 41, 49, 60, 27, 47, 3, 118, 92, 125, 82, 39, 140, 137, 14, 31, 118, 165, 196, 8, 22, 238, 81, 156, 21, 52, 101, 197, 245, 202, 219, 157, 207, 192, 27, 20, 214, 9, 194, 90, 53, 43, 75, 18, 95, 102, 204, 187, 151, 85, 230, 160, 38, 120, 178, 97, 190, 60, 156, 91, 2, 199, 31, 218, 1, 74, 138, 68, 128, 14, 112, 201, 11, 255, 145, 122, 79, 234, 226, 35, 133, 52, 64, 50, 96, 169, 104, 144, 209, 40 }, null, "User2@User2" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "User3", "User3", "password123", new byte[] { 226, 10, 71, 255, 0, 247, 233, 169, 146, 105, 140, 235, 162, 89, 228, 7, 239, 239, 228, 185, 201, 135, 125, 90, 127, 201, 109, 199, 1, 4, 203, 92, 185, 91, 220, 81, 207, 28, 135, 102, 8, 83, 171, 146, 45, 43, 86, 59, 141, 114, 148, 90, 149, 154, 81, 128, 64, 115, 167, 155, 9, 82, 20, 162 }, new byte[] { 3, 34, 27, 243, 186, 121, 116, 25, 202, 42, 95, 133, 70, 68, 16, 71, 178, 157, 88, 248, 53, 149, 2, 108, 51, 174, 58, 144, 61, 189, 233, 43, 208, 166, 184, 115, 159, 135, 221, 41, 49, 60, 27, 47, 3, 118, 92, 125, 82, 39, 140, 137, 14, 31, 118, 165, 196, 8, 22, 238, 81, 156, 21, 52, 101, 197, 245, 202, 219, 157, 207, 192, 27, 20, 214, 9, 194, 90, 53, 43, 75, 18, 95, 102, 204, 187, 151, 85, 230, 160, 38, 120, 178, 97, 190, 60, 156, 91, 2, 199, 31, 218, 1, 74, 138, 68, 128, 14, 112, 201, 11, 255, 145, 122, 79, 234, 226, 35, 133, 52, 64, 50, 96, 169, 104, 144, 209, 40 }, null, "User3@User3" },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "User4", "User4", "password123", new byte[] { 226, 10, 71, 255, 0, 247, 233, 169, 146, 105, 140, 235, 162, 89, 228, 7, 239, 239, 228, 185, 201, 135, 125, 90, 127, 201, 109, 199, 1, 4, 203, 92, 185, 91, 220, 81, 207, 28, 135, 102, 8, 83, 171, 146, 45, 43, 86, 59, 141, 114, 148, 90, 149, 154, 81, 128, 64, 115, 167, 155, 9, 82, 20, 162 }, new byte[] { 3, 34, 27, 243, 186, 121, 116, 25, 202, 42, 95, 133, 70, 68, 16, 71, 178, 157, 88, 248, 53, 149, 2, 108, 51, 174, 58, 144, 61, 189, 233, 43, 208, 166, 184, 115, 159, 135, 221, 41, 49, 60, 27, 47, 3, 118, 92, 125, 82, 39, 140, 137, 14, 31, 118, 165, 196, 8, 22, 238, 81, 156, 21, 52, 101, 197, 245, 202, 219, 157, 207, 192, 27, 20, 214, 9, 194, 90, 53, 43, 75, 18, 95, 102, 204, 187, 151, 85, 230, 160, 38, 120, 178, 97, 190, 60, 156, 91, 2, 199, 31, 218, 1, 74, 138, 68, 128, 14, 112, 201, 11, 255, 145, 122, 79, 234, 226, 35, 133, 52, 64, 50, 96, 169, 104, 144, 209, 40 }, null, "User4@User4" },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "User5", "User5", "password123", new byte[] { 226, 10, 71, 255, 0, 247, 233, 169, 146, 105, 140, 235, 162, 89, 228, 7, 239, 239, 228, 185, 201, 135, 125, 90, 127, 201, 109, 199, 1, 4, 203, 92, 185, 91, 220, 81, 207, 28, 135, 102, 8, 83, 171, 146, 45, 43, 86, 59, 141, 114, 148, 90, 149, 154, 81, 128, 64, 115, 167, 155, 9, 82, 20, 162 }, new byte[] { 3, 34, 27, 243, 186, 121, 116, 25, 202, 42, 95, 133, 70, 68, 16, 71, 178, 157, 88, 248, 53, 149, 2, 108, 51, 174, 58, 144, 61, 189, 233, 43, 208, 166, 184, 115, 159, 135, 221, 41, 49, 60, 27, 47, 3, 118, 92, 125, 82, 39, 140, 137, 14, 31, 118, 165, 196, 8, 22, 238, 81, 156, 21, 52, 101, 197, 245, 202, 219, 157, 207, 192, 27, 20, 214, 9, 194, 90, 53, 43, 75, 18, 95, 102, 204, 187, 151, 85, 230, 160, 38, 120, 178, 97, 190, 60, 156, 91, 2, 199, 31, 218, 1, 74, 138, 68, 128, 14, 112, 201, 11, 255, 145, 122, 79, 234, 226, 35, 133, 52, 64, 50, 96, 169, 104, 144, 209, 40 }, null, "User5@User5" },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "User6", "User6", "password123", new byte[] { 226, 10, 71, 255, 0, 247, 233, 169, 146, 105, 140, 235, 162, 89, 228, 7, 239, 239, 228, 185, 201, 135, 125, 90, 127, 201, 109, 199, 1, 4, 203, 92, 185, 91, 220, 81, 207, 28, 135, 102, 8, 83, 171, 146, 45, 43, 86, 59, 141, 114, 148, 90, 149, 154, 81, 128, 64, 115, 167, 155, 9, 82, 20, 162 }, new byte[] { 3, 34, 27, 243, 186, 121, 116, 25, 202, 42, 95, 133, 70, 68, 16, 71, 178, 157, 88, 248, 53, 149, 2, 108, 51, 174, 58, 144, 61, 189, 233, 43, 208, 166, 184, 115, 159, 135, 221, 41, 49, 60, 27, 47, 3, 118, 92, 125, 82, 39, 140, 137, 14, 31, 118, 165, 196, 8, 22, 238, 81, 156, 21, 52, 101, 197, 245, 202, 219, 157, 207, 192, 27, 20, 214, 9, 194, 90, 53, 43, 75, 18, 95, 102, 204, 187, 151, 85, 230, 160, 38, 120, 178, 97, 190, 60, 156, 91, 2, 199, 31, 218, 1, 74, 138, 68, 128, 14, 112, 201, 11, 255, 145, 122, 79, 234, 226, 35, 133, 52, 64, 50, 96, 169, 104, 144, 209, 40 }, null, "User6@User6" },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "User7", "User7", "password123", new byte[] { 226, 10, 71, 255, 0, 247, 233, 169, 146, 105, 140, 235, 162, 89, 228, 7, 239, 239, 228, 185, 201, 135, 125, 90, 127, 201, 109, 199, 1, 4, 203, 92, 185, 91, 220, 81, 207, 28, 135, 102, 8, 83, 171, 146, 45, 43, 86, 59, 141, 114, 148, 90, 149, 154, 81, 128, 64, 115, 167, 155, 9, 82, 20, 162 }, new byte[] { 3, 34, 27, 243, 186, 121, 116, 25, 202, 42, 95, 133, 70, 68, 16, 71, 178, 157, 88, 248, 53, 149, 2, 108, 51, 174, 58, 144, 61, 189, 233, 43, 208, 166, 184, 115, 159, 135, 221, 41, 49, 60, 27, 47, 3, 118, 92, 125, 82, 39, 140, 137, 14, 31, 118, 165, 196, 8, 22, 238, 81, 156, 21, 52, 101, 197, 245, 202, 219, 157, 207, 192, 27, 20, 214, 9, 194, 90, 53, 43, 75, 18, 95, 102, 204, 187, 151, 85, 230, 160, 38, 120, 178, 97, 190, 60, 156, 91, 2, 199, 31, 218, 1, 74, 138, 68, 128, 14, 112, 201, 11, 255, 145, 122, 79, 234, 226, 35, 133, 52, 64, 50, 96, 169, 104, 144, 209, 40 }, null, "User7@User7" },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "User8", "User8", "password123", new byte[] { 226, 10, 71, 255, 0, 247, 233, 169, 146, 105, 140, 235, 162, 89, 228, 7, 239, 239, 228, 185, 201, 135, 125, 90, 127, 201, 109, 199, 1, 4, 203, 92, 185, 91, 220, 81, 207, 28, 135, 102, 8, 83, 171, 146, 45, 43, 86, 59, 141, 114, 148, 90, 149, 154, 81, 128, 64, 115, 167, 155, 9, 82, 20, 162 }, new byte[] { 3, 34, 27, 243, 186, 121, 116, 25, 202, 42, 95, 133, 70, 68, 16, 71, 178, 157, 88, 248, 53, 149, 2, 108, 51, 174, 58, 144, 61, 189, 233, 43, 208, 166, 184, 115, 159, 135, 221, 41, 49, 60, 27, 47, 3, 118, 92, 125, 82, 39, 140, 137, 14, 31, 118, 165, 196, 8, 22, 238, 81, 156, 21, 52, 101, 197, 245, 202, 219, 157, 207, 192, 27, 20, 214, 9, 194, 90, 53, 43, 75, 18, 95, 102, 204, 187, 151, 85, 230, 160, 38, 120, 178, 97, 190, 60, 156, 91, 2, 199, 31, 218, 1, 74, 138, 68, 128, 14, 112, 201, 11, 255, 145, 122, 79, 234, 226, 35, 133, 52, 64, 50, 96, 169, 104, 144, 209, 40 }, null, "User8@User8" },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "User9", "User9", "password123", new byte[] { 226, 10, 71, 255, 0, 247, 233, 169, 146, 105, 140, 235, 162, 89, 228, 7, 239, 239, 228, 185, 201, 135, 125, 90, 127, 201, 109, 199, 1, 4, 203, 92, 185, 91, 220, 81, 207, 28, 135, 102, 8, 83, 171, 146, 45, 43, 86, 59, 141, 114, 148, 90, 149, 154, 81, 128, 64, 115, 167, 155, 9, 82, 20, 162 }, new byte[] { 3, 34, 27, 243, 186, 121, 116, 25, 202, 42, 95, 133, 70, 68, 16, 71, 178, 157, 88, 248, 53, 149, 2, 108, 51, 174, 58, 144, 61, 189, 233, 43, 208, 166, 184, 115, 159, 135, 221, 41, 49, 60, 27, 47, 3, 118, 92, 125, 82, 39, 140, 137, 14, 31, 118, 165, 196, 8, 22, 238, 81, 156, 21, 52, 101, 197, 245, 202, 219, 157, 207, 192, 27, 20, 214, 9, 194, 90, 53, 43, 75, 18, 95, 102, 204, 187, 151, 85, 230, 160, 38, 120, 178, 97, 190, 60, 156, 91, 2, 199, 31, 218, 1, 74, 138, 68, 128, 14, 112, 201, 11, 255, 145, 122, 79, 234, 226, 35, 133, 52, 64, 50, 96, 169, 104, 144, 209, 40 }, null, "User9@User9" },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "User10", "User10", "password123", new byte[] { 226, 10, 71, 255, 0, 247, 233, 169, 146, 105, 140, 235, 162, 89, 228, 7, 239, 239, 228, 185, 201, 135, 125, 90, 127, 201, 109, 199, 1, 4, 203, 92, 185, 91, 220, 81, 207, 28, 135, 102, 8, 83, 171, 146, 45, 43, 86, 59, 141, 114, 148, 90, 149, 154, 81, 128, 64, 115, 167, 155, 9, 82, 20, 162 }, new byte[] { 3, 34, 27, 243, 186, 121, 116, 25, 202, 42, 95, 133, 70, 68, 16, 71, 178, 157, 88, 248, 53, 149, 2, 108, 51, 174, 58, 144, 61, 189, 233, 43, 208, 166, 184, 115, 159, 135, 221, 41, 49, 60, 27, 47, 3, 118, 92, 125, 82, 39, 140, 137, 14, 31, 118, 165, 196, 8, 22, 238, 81, 156, 21, 52, 101, 197, 245, 202, 219, 157, 207, 192, 27, 20, 214, 9, 194, 90, 53, 43, 75, 18, 95, 102, 204, 187, 151, 85, 230, 160, 38, 120, 178, 97, 190, 60, 156, 91, 2, 199, 31, 218, 1, 74, 138, 68, 128, 14, 112, 201, 11, 255, 145, 122, 79, 234, 226, 35, 133, 52, 64, 50, 96, 169, 104, 144, 209, 40 }, null, "User10@User10" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropSequence(
                name: "Code",
                schema: "shared");
        }
    }
}
