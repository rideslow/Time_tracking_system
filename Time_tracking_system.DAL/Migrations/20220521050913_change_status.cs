using Microsoft.EntityFrameworkCore.Migrations;

namespace Time_tracking_system.DAL.Migrations
{
    public partial class change_status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributesVM");

            migrationBuilder.DropTable(
                name: "TypeRequestVM");

            migrationBuilder.DropTable(
                name: "TypeVocationVM");
        }
    }
}
