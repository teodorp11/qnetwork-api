using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace qnetwork_api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IndustrialDevices",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IndustrialDeviceType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IndustrialSensorType = table.Column<int>(type: "int", nullable: true),
                    IndustrialActuatorType = table.Column<int>(type: "int", nullable: true),
                    IndustrialControllerType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndustrialDevices", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Networks",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Networks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IndustrialDeviceNetworkMappings",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IndustrialDeviceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NetworkID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndustrialDeviceNetworkMappings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IndustrialDeviceNetworkMappings_IndustrialDevices_IndustrialDeviceID",
                        column: x => x.IndustrialDeviceID,
                        principalTable: "IndustrialDevices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndustrialDeviceNetworkMappings_Networks_NetworkID",
                        column: x => x.NetworkID,
                        principalTable: "Networks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndustrialDeviceNetworkMappings_IndustrialDeviceID_NetworkID",
                table: "IndustrialDeviceNetworkMappings",
                columns: new[] { "IndustrialDeviceID", "NetworkID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IndustrialDeviceNetworkMappings_NetworkID",
                table: "IndustrialDeviceNetworkMappings",
                column: "NetworkID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndustrialDeviceNetworkMappings");

            migrationBuilder.DropTable(
                name: "IndustrialDevices");

            migrationBuilder.DropTable(
                name: "Networks");
        }
    }
}
