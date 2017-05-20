using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QualityBags.Migrations
{
    public partial class ChangeDeleteBehaviour_OrderItem_Bag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Bag_BagID",
                table: "OrderItem");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Bag_BagID",
                table: "OrderItem",
                column: "BagID",
                principalTable: "Bag",
                principalColumn: "BagID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Bag_BagID",
                table: "OrderItem");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Bag_BagID",
                table: "OrderItem",
                column: "BagID",
                principalTable: "Bag",
                principalColumn: "BagID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
