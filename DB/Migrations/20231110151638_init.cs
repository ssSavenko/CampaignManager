using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Template",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<int>(type: "int", nullable: false),
                    GenderDataId = table.Column<int>(type: "int", nullable: false),
                    CityDataId = table.Column<int>(type: "int", nullable: false),
                    Deposit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NewCustomer = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Cities_CityDataId",
                        column: x => x.CityDataId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customers_Gender_GenderDataId",
                        column: x => x.GenderDataId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaignTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    TemplateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campaigns_Template_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CampaignConditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Condition = table.Column<int>(type: "int", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CampaignId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignConditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampaignConditions_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "London" },
                    { 2, "New York" },
                    { 3, "Paris" },
                    { 4, "Tel-Aviv" }
                });

            migrationBuilder.InsertData(
                table: "Gender",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Male" },
                    { 2, "Female" }
                });

            migrationBuilder.InsertData(
                table: "Template",
                columns: new[] { "Id", "Name", "Url" },
                values: new object[,]
                {
                    { 1, "TemplateA", "Resources/Templates/Resources/Templates/TemplateA.html" },
                    { 2, "TemplateB", "Resources/Templates/Resources/Templates/TemplateB.html" },
                    { 3, "TemplateC", "Resources/Templates/Resources/Templates/TemplateC.html" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Age", "CityDataId", "Deposit", "GenderDataId", "NewCustomer" },
                values: new object[,]
                {
                    { 1, 53, 1, 104m, 1, false },
                    { 2, 44, 2, 209m, 2, true },
                    { 3, 36, 2, 208m, 1, true },
                    { 4, 87, 1, 134m, 2, false },
                    { 5, 54, 3, 123m, 1, true },
                    { 6, 45, 2, 210m, 2, true },
                    { 7, 49, 4, 174m, 2, false },
                    { 8, 35, 3, 52m, 1, true },
                    { 9, 61, 4, 151m, 1, false },
                    { 10, 78, 3, 57m, 1, false },
                    { 11, 41, 2, 131m, 2, false },
                    { 12, 32, 4, 154m, 2, true },
                    { 13, 62, 3, 135m, 2, false },
                    { 14, 67, 4, 153m, 1, true },
                    { 15, 68, 1, 241m, 2, true },
                    { 16, 41, 1, 134m, 1, false },
                    { 17, 46, 1, 212m, 2, false },
                    { 18, 77, 4, 97m, 2, true },
                    { 19, 51, 1, 141m, 1, true },
                    { 20, 80, 3, 189m, 1, false },
                    { 21, 31, 4, 134m, 2, true },
                    { 22, 80, 4, 81m, 2, false },
                    { 23, 36, 1, 237m, 2, true },
                    { 24, 65, 4, 119m, 2, false },
                    { 25, 72, 4, 139m, 1, false },
                    { 26, 64, 4, 128m, 1, true },
                    { 27, 29, 1, 76m, 1, true },
                    { 28, 25, 1, 203m, 1, true },
                    { 29, 77, 2, 54m, 1, true },
                    { 30, 79, 3, 165m, 2, true },
                    { 31, 26, 3, 143m, 2, true },
                    { 32, 74, 1, 61m, 2, false },
                    { 33, 74, 2, 103m, 1, false },
                    { 34, 46, 2, 121m, 2, true },
                    { 35, 47, 2, 214m, 2, false },
                    { 36, 78, 2, 111m, 2, false },
                    { 37, 46, 2, 223m, 2, true },
                    { 38, 26, 2, 78m, 2, true },
                    { 39, 49, 4, 60m, 2, false },
                    { 40, 74, 2, 53m, 2, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampaignConditions_CampaignId",
                table: "CampaignConditions",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_Priority",
                table: "Campaigns",
                column: "Priority",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_TemplateId",
                table: "Campaigns",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CityDataId",
                table: "Customers",
                column: "CityDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_GenderDataId",
                table: "Customers",
                column: "GenderDataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampaignConditions");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "Template");
        }
    }
}
