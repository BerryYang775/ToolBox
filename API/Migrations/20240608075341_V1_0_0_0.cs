using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class V1_0_0_0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoCategory",
                columns: table => new
                {
                    TodoCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DateTime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoCategory", x => x.TodoCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Todo",
                columns: table => new
                {
                    TodoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Caption = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DateTime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FinishDate = table.Column<DateTime>(type: "DateTime", nullable: true),
                    FinishBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CategoryTodoCategoryID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todo", x => x.TodoID);
                    table.ForeignKey(
                        name: "FK_Todo_TodoCategory_CategoryTodoCategoryID",
                        column: x => x.CategoryTodoCategoryID,
                        principalTable: "TodoCategory",
                        principalColumn: "TodoCategoryID");
                });

            migrationBuilder.InsertData(
                table: "TodoCategory",
                columns: new[] { "TodoCategoryID", "Active", "CreatedBy", "Title", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, true, "System", "Target", null, null },
                    { 2, true, "System", "Daily", null, null },
                    { 3, true, "System", "Study", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Todo_CategoryTodoCategoryID",
                table: "Todo",
                column: "CategoryTodoCategoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todo");

            migrationBuilder.DropTable(
                name: "TodoCategory");
        }
    }
}
