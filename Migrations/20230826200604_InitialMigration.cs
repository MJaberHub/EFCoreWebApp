using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreWebApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_CUSTOMERS",
                columns: table => new
                {
                    CUST_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIRST_NAME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    LAST_NAME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CREATED_BY = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    DATE_CREATED = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    DATE_MODIFED = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__T_CUSTOM__93ABC0033BEC0F36", x => x.CUST_ID);
                });

            migrationBuilder.CreateTable(
                name: "T_ACCOUNTS",
                columns: table => new
                {
                    ACCOUNT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CUST_ID = table.Column<int>(type: "int", nullable: true),
                    CREATED_BY = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    DATE_CREATED = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    DATE_MODIFED = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ACCOUNT_NAME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__T_ACCOUN__05B22F601D9B2048", x => x.ACCOUNT_ID);
                    table.ForeignKey(
                        name: "FK__T_ACCOUNT__CUST___3B75D760",
                        column: x => x.CUST_ID,
                        principalTable: "T_CUSTOMERS",
                        principalColumn: "CUST_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_ACCOUNTS_CUST_ID",
                table: "T_ACCOUNTS",
                column: "CUST_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_ACCOUNTS");

            migrationBuilder.DropTable(
                name: "T_CUSTOMERS");
        }
    }
}
