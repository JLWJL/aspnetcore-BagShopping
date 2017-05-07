using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QualityBags.Migrations
{
    public partial class NoRequiredCustIDForOrderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_CustomerID",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerID",
                table: "Order",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_CustomerID",
                table: "Order",
                column: "CustomerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_CustomerID",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerID",
                table: "Order",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_CustomerID",
                table: "Order",
                column: "CustomerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
