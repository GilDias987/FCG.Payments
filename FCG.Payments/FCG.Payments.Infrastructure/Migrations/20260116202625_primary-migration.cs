using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCG.Payments.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class primarymigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_PAGAMENTO",
                columns: table => new
                {
                    ISN_PAGAMENTO = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ISN_USUARIO = table.Column<int>(type: "INT", nullable: false),
                    ISN_GAME = table.Column<int>(type: "INT", nullable: false),
                    COD_PAGAMENTO = table.Column<string>(type: "VARCHAR(1)", nullable: false),
                    COD_STATUS = table.Column<string>(type: "VARCHAR(1)", nullable: false),
                    DTH_CRIACAO = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DTH_ATUALIZACAO = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PAGAMENTO", x => x.ISN_PAGAMENTO);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_PAGAMENTO");
        }
    }
}
