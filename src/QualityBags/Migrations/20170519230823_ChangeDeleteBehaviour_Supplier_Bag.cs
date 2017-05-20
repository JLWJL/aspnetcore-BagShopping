using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QualityBags.Migrations
{
    public partial class ChangeDeleteBehaviour_Supplier_Bag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bag_Supplier_SupplierID",
                table: "Bag");

            migrationBuilder.AddForeignKey(
                name: "FK_Bag_Supplier_SupplierID",
                table: "Bag",
                column: "SupplierID",
                principalTable: "Supplier",
                principalColumn: "SupplierID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
    name: "FK_Bag_Supplier_SupplierID",
    table: "Bag");

            migrationBuilder.AddForeignKey(
                name: "FK_Bag_Supplier_SupplierID",
                table: "Bag",
                column: "SupplierID",
                principalTable: "Supplier",
                principalColumn: "SupplierID",
                onDelete: ReferentialAction.Restrict);
        }
    
    }
}
