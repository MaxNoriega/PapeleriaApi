using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPaplria.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alumnos",
                columns: table => new
                {
                    NumCtrl = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumnos", x => x.NumCtrl);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    IdPro = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Descrip = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.IdPro);
                });

            migrationBuilder.CreateTable(
                name: "Puntos",
                columns: table => new
                {
                    NumCtrl = table.Column<int>(type: "int", nullable: false),
                    PuntosDisponibles = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puntos", x => x.NumCtrl);
                    table.ForeignKey(
                        name: "FK_Puntos_Alumnos_NumCtrl",
                        column: x => x.NumCtrl,
                        principalTable: "Alumnos",
                        principalColumn: "NumCtrl");
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    IdVenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PtsGenerados = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClienteNumCtrl = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.IdVenta);
                    table.ForeignKey(
                        name: "FK_Ventas_Alumnos_ClienteNumCtrl",
                        column: x => x.ClienteNumCtrl,
                        principalTable: "Alumnos",
                        principalColumn: "NumCtrl",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ventas_Alumnos_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Alumnos",
                        principalColumn: "NumCtrl");
                });

            migrationBuilder.CreateTable(
                name: "DetallesVenta",
                columns: table => new
                {
                    IdDetalle = table.Column<int>(type: "int", nullable: false),
                    IdVenta = table.Column<int>(type: "int", nullable: false),
                    IdPro = table.Column<int>(type: "int", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CantidadProd = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesVenta", x => x.IdDetalle);
                    table.ForeignKey(
                        name: "FK_DetallesVenta_Productos_IdPro",
                        column: x => x.IdPro,
                        principalTable: "Productos",
                        principalColumn: "IdPro");
                    table.ForeignKey(
                        name: "FK_DetallesVenta_Ventas_IdVenta",
                        column: x => x.IdVenta,
                        principalTable: "Ventas",
                        principalColumn: "IdVenta");
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    IdPago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVenta = table.Column<int>(type: "int", nullable: false),
                    TipoPago = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CuentaOrigen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CuentaDestino = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoTrans = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.IdPago);
                    table.ForeignKey(
                        name: "FK_Pagos_Ventas_IdVenta",
                        column: x => x.IdVenta,
                        principalTable: "Ventas",
                        principalColumn: "IdVenta");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesVenta_IdPro",
                table: "DetallesVenta",
                column: "IdPro");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesVenta_IdVenta",
                table: "DetallesVenta",
                column: "IdVenta");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_IdVenta",
                table: "Pagos",
                column: "IdVenta");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_ClienteNumCtrl",
                table: "Ventas",
                column: "ClienteNumCtrl");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_IdCliente",
                table: "Ventas",
                column: "IdCliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesVenta");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "Puntos");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Alumnos");
        }
    }
}
