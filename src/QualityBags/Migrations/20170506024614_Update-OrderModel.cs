using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QualityBags.Migrations
{
    public partial class UpdateOrderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_UserId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Order",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Contact",
                table: "Order",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Order",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Order",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Receiver",
                table: "Order",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Order",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCost",
                table: "Order",
                nullable: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "SubTotal",
                table: "Order",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerID",
                table: "Order",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerID",
                table: "Order",
                column: "CustomerID");

            migrationBuilder.AlterColumn<string>(
                name: "CartID",
                table: "CartItem",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Bag",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_CustomerID",
                table: "Order",
                column: "CustomerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_CustomerID",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_CustomerID",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Contact",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Receiver",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Order",
                nullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "TotalCost",
                table: "Order",
                nullable: false);

            migrationBuilder.AlterColumn<float>(
                name: "SubTotal",
                table: "Order",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "Order",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.AlterColumn<int>(
                name: "CartID",
                table: "CartItem",
                nullable: false);

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Bag",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
