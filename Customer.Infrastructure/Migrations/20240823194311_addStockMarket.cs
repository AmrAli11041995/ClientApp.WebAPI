using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Customer.Infrastructure.Migrations
{
    public partial class addStockMarket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockMarkets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClosePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OpenPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HighestPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LowestPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TimeStamp = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NumberOfTransactions = table.Column<int>(type: "int", nullable: true),
                    TradingVolume = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VolumeWeighted = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMarkets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockMarkets");
        }
    }
}
