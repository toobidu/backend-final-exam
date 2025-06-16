using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToTienDung0300567.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mechanic",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    maTho = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    tenTho = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    cCCD = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    ngayNhanViec = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mechanic", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bienSoXe = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    loaiXe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RepairRecord",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mechanicId = table.Column<int>(type: "int", nullable: false),
                    vehicleId = table.Column<int>(type: "int", nullable: false),
                    soLanSua = table.Column<int>(type: "int", nullable: false),
                    MechanicId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairRecord", x => x.id);
                    table.ForeignKey(
                        name: "FK_RepairRecord_Mechanic_mechanicId",
                        column: x => x.mechanicId,
                        principalTable: "Mechanic",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepairRecord_Vehicle_vehicleId",
                        column: x => x.vehicleId,
                        principalTable: "Vehicle",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mechanic_cCCD",
                table: "Mechanic",
                column: "cCCD",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mechanic_maTho",
                table: "Mechanic",
                column: "maTho",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mechanic_tenTho",
                table: "Mechanic",
                column: "tenTho",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RepairRecord_mechanicId",
                table: "RepairRecord",
                column: "mechanicId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairRecord_vehicleId",
                table: "RepairRecord",
                column: "vehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_bienSoXe",
                table: "Vehicle",
                column: "bienSoXe",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepairRecord");

            migrationBuilder.DropTable(
                name: "Mechanic");

            migrationBuilder.DropTable(
                name: "Vehicle");
        }
    }
}
