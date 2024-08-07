using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EgyTransDb.Migrations
{
    /// <inheritdoc />
    public partial class initail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarDatas",
                columns: table => new
                {
                    CarID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarPrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarPrincebyDay = table.Column<int>(type: "int", nullable: false),
                    CarPrincebyHour = table.Column<int>(type: "int", nullable: false),
                    CarDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarDatas", x => x.CarID);
                });

            migrationBuilder.CreateTable(
                name: "ClientDatas",
                columns: table => new
                {
                    ClientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonsCount = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientDatas", x => x.ClientID);
                });

            migrationBuilder.CreateTable(
                name: "ClientTypes",
                columns: table => new
                {
                    TypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTypes", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "DriverDatas",
                columns: table => new
                {
                    DriverDataID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverPhone = table.Column<int>(type: "int", nullable: false),
                    DriverPrice = table.Column<int>(type: "int", nullable: false),
                    DriverTeps = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverDatas", x => x.DriverDataID);
                });

            migrationBuilder.CreateTable(
                name: "SupplierDatas",
                columns: table => new
                {
                    SupplieID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierPhone = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierDatas", x => x.SupplieID);
                });

            migrationBuilder.CreateTable(
                name: "TourGuides",
                columns: table => new
                {
                    GuideID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuideName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Telphne = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourGuides", x => x.GuideID);
                });

            migrationBuilder.CreateTable(
                name: "TravelDatas",
                columns: table => new
                {
                    TravelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<int>(type: "int", nullable: false),
                    TypeID = table.Column<int>(type: "int", nullable: false),
                    TransferDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NetCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Profit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelDatas", x => x.TravelID);
                    table.ForeignKey(
                        name: "FK_TravelDatas_ClientDatas_ClientID",
                        column: x => x.ClientID,
                        principalTable: "ClientDatas",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TravelDatas_ClientTypes_TypeID",
                        column: x => x.TypeID,
                        principalTable: "ClientTypes",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuideID",
                columns: table => new
                {
                    TourGuidesGuideID = table.Column<int>(type: "int", nullable: false),
                    TravelDataTravelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuideID", x => new { x.TourGuidesGuideID, x.TravelDataTravelID });
                    table.ForeignKey(
                        name: "FK_GuideID_TourGuides_TourGuidesGuideID",
                        column: x => x.TourGuidesGuideID,
                        principalTable: "TourGuides",
                        principalColumn: "GuideID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuideID_TravelDatas_TravelDataTravelID",
                        column: x => x.TravelDataTravelID,
                        principalTable: "TravelDatas",
                        principalColumn: "TravelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TravelCar",
                columns: table => new
                {
                    CarsCarID = table.Column<int>(type: "int", nullable: false),
                    TravelDataTravelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelCar", x => new { x.CarsCarID, x.TravelDataTravelID });
                    table.ForeignKey(
                        name: "FK_TravelCar_CarDatas_CarsCarID",
                        column: x => x.CarsCarID,
                        principalTable: "CarDatas",
                        principalColumn: "CarID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TravelCar_TravelDatas_TravelDataTravelID",
                        column: x => x.TravelDataTravelID,
                        principalTable: "TravelDatas",
                        principalColumn: "TravelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TravelDriver",
                columns: table => new
                {
                    DriversDriverDataID = table.Column<int>(type: "int", nullable: false),
                    TravelDataTravelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelDriver", x => new { x.DriversDriverDataID, x.TravelDataTravelID });
                    table.ForeignKey(
                        name: "FK_TravelDriver_DriverDatas_DriversDriverDataID",
                        column: x => x.DriversDriverDataID,
                        principalTable: "DriverDatas",
                        principalColumn: "DriverDataID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TravelDriver_TravelDatas_TravelDataTravelID",
                        column: x => x.TravelDataTravelID,
                        principalTable: "TravelDatas",
                        principalColumn: "TravelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TravelInfos",
                columns: table => new
                {
                    TravelInfoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransferFrom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransferTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AtDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TravelDataID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelInfos", x => x.TravelInfoID);
                    table.ForeignKey(
                        name: "FK_TravelInfos_TravelDatas_TravelDataID",
                        column: x => x.TravelDataID,
                        principalTable: "TravelDatas",
                        principalColumn: "TravelID");
                });

            migrationBuilder.CreateTable(
                name: "TravelSupplier",
                columns: table => new
                {
                    SuppliersSupplieID = table.Column<int>(type: "int", nullable: false),
                    TravelDataTravelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelSupplier", x => new { x.SuppliersSupplieID, x.TravelDataTravelID });
                    table.ForeignKey(
                        name: "FK_TravelSupplier_SupplierDatas_SuppliersSupplieID",
                        column: x => x.SuppliersSupplieID,
                        principalTable: "SupplierDatas",
                        principalColumn: "SupplieID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TravelSupplier_TravelDatas_TravelDataTravelID",
                        column: x => x.TravelDataTravelID,
                        principalTable: "TravelDatas",
                        principalColumn: "TravelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuideID_TravelDataTravelID",
                table: "GuideID",
                column: "TravelDataTravelID");

            migrationBuilder.CreateIndex(
                name: "IX_TravelCar_TravelDataTravelID",
                table: "TravelCar",
                column: "TravelDataTravelID");

            migrationBuilder.CreateIndex(
                name: "IX_TravelDatas_ClientID",
                table: "TravelDatas",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_TravelDatas_TypeID",
                table: "TravelDatas",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TravelDriver_TravelDataTravelID",
                table: "TravelDriver",
                column: "TravelDataTravelID");

            migrationBuilder.CreateIndex(
                name: "IX_TravelInfos_TravelDataID",
                table: "TravelInfos",
                column: "TravelDataID");

            migrationBuilder.CreateIndex(
                name: "IX_TravelSupplier_TravelDataTravelID",
                table: "TravelSupplier",
                column: "TravelDataTravelID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuideID");

            migrationBuilder.DropTable(
                name: "TravelCar");

            migrationBuilder.DropTable(
                name: "TravelDriver");

            migrationBuilder.DropTable(
                name: "TravelInfos");

            migrationBuilder.DropTable(
                name: "TravelSupplier");

            migrationBuilder.DropTable(
                name: "TourGuides");

            migrationBuilder.DropTable(
                name: "CarDatas");

            migrationBuilder.DropTable(
                name: "DriverDatas");

            migrationBuilder.DropTable(
                name: "SupplierDatas");

            migrationBuilder.DropTable(
                name: "TravelDatas");

            migrationBuilder.DropTable(
                name: "ClientDatas");

            migrationBuilder.DropTable(
                name: "ClientTypes");
        }
    }
}
