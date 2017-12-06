using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KeezzContractors.API.Migrations
{
    public partial class KeezzContractorsDBInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contractors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    Inactive = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contractors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractorInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContractorId = table.Column<int>(nullable: false),
                    ContractorInvDate = table.Column<DateTime>(nullable: false),
                    ContractorInvNote = table.Column<string>(maxLength: 200, nullable: true),
                    ContractorInvRef = table.Column<string>(maxLength: 20, nullable: false),
                    DaysBilled = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorInvoices_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContractorInvoiceId = table.Column<int>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    ExpenseAmount = table.Column<double>(nullable: false),
                    ExpenseDate = table.Column<DateTime>(nullable: false),
                    ExpenseHeaderId = table.Column<int>(nullable: false),
                    ExpenseNote = table.Column<string>(nullable: true),
                    ForeignAmount = table.Column<double>(nullable: false),
                    GST = table.Column<bool>(nullable: false),
                    KeezzInvId = table.Column<int>(nullable: false),
                    OnBill = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_ContractorInvoices_ContractorInvoiceId",
                        column: x => x.ContractorInvoiceId,
                        principalTable: "ContractorInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractorInvoices_ContractorId",
                table: "ContractorInvoices",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ContractorInvoiceId",
                table: "Expenses",
                column: "ContractorInvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "ContractorInvoices");

            migrationBuilder.DropTable(
                name: "Contractors");
        }
    }
}
