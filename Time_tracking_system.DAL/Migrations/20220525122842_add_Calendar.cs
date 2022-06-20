using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Time_tracking_system.DAL.Migrations
{
    public partial class add_Calendar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributesVM");

            migrationBuilder.DropTable(
                name: "TypeRequestVM");

            migrationBuilder.DropTable(
                name: "TypeVocationVM");

            migrationBuilder.CreateTable(
                name: "Years",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalendarInputStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Years", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HolidaysCalendar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HolidayDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Years_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidaysCalendar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HolidaysCalendar_Years_Years_id",
                        column: x => x.Years_id,
                        principalTable: "Years",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HolidaysCalendar_Years_id",
                table: "HolidaysCalendar",
                column: "Years_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HolidaysCalendar");

            migrationBuilder.DropTable(
                name: "Years");

            migrationBuilder.CreateTable(
                name: "AttributesVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAttributes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeData = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributesVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeRequestVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeRequestVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeVocationVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeVocationVM", x => x.Id);
                });
        }
    }
}
