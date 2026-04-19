using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Contacts.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    CategoryId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: false),
                    CategoryId = table.Column<Guid>(type: "char(36)", nullable: true),
                    SubCategoryId = table.Column<Guid>(type: "char(36)", nullable: true),
                    CustomSubCategory = table.Column<string>(type: "longtext", nullable: true),
                    Phone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Contacts_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("020b977f-e9f0-4310-af8e-1be21fab0a77"), "Prywatny" },
                    { new Guid("462cdacf-1d2e-49d7-af10-aefad2d29459"), "Służbowy" }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { new Guid("b1a7c6d8-3f44-4c9a-9f1a-111111111111"), new Guid("462cdacf-1d2e-49d7-af10-aefad2d29459"), "Szef" },
                    { new Guid("b2a7c6d8-3f44-4c9a-9f1a-222222222222"), new Guid("462cdacf-1d2e-49d7-af10-aefad2d29459"), "Sprzedaż" },
                    { new Guid("b3a7c6d8-3f44-4c9a-9f1a-333333333333"), new Guid("462cdacf-1d2e-49d7-af10-aefad2d29459"), "Kontrahent" },
                    { new Guid("b4a7c6d8-3f44-4c9a-9f1a-444444444444"), new Guid("462cdacf-1d2e-49d7-af10-aefad2d29459"), "Dział IT" },
                    { new Guid("c1a7c6d8-3f44-4c9a-9f1a-555555555555"), new Guid("020b977f-e9f0-4310-af8e-1be21fab0a77"), "Rodzina" },
                    { new Guid("c2a7c6d8-3f44-4c9a-9f1a-666666666666"), new Guid("020b977f-e9f0-4310-af8e-1be21fab0a77"), "Przyjaciele" },
                    { new Guid("c3a7c6d8-3f44-4c9a-9f1a-777777777777"), new Guid("020b977f-e9f0-4310-af8e-1be21fab0a77"), "Znajomi" },
                    { new Guid("c4a7c6d8-3f44-4c9a-9f1a-888888888888"), new Guid("020b977f-e9f0-4310-af8e-1be21fab0a77"), "Sąsiedzi" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CategoryId",
                table: "Contacts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Email",
                table: "Contacts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_SubCategoryId",
                table: "Contacts",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
