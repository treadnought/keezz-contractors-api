using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KeezzContractors.API.Migrations
{
    public partial class KeezzContractorsDBAddContractorCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContractorCompany",
                table: "Contractors",
                maxLength: 80,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractorCompany",
                table: "Contractors");
        }
    }
}
