using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Time_tracking_system.DAL.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Patronymic",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Post",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAttributes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeData = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypesRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypesVocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesVocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attributes_TypesRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeRequest_id = table.Column<int>(type: "int", nullable: false),
                    Attr_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes_TypesRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attributes_TypesRequests_Attributes_Attr_id",
                        column: x => x.Attr_id,
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attributes_TypesRequests_TypesRequests_TypeRequest_id",
                        column: x => x.TypeRequest_id,
                        principalTable: "TypesRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeRequest_id = table.Column<int>(type: "int", nullable: false),
                    Applicant_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRequested = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CountDayOnRequest = table.Column<int>(type: "int", nullable: true),
                    Cordinating_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StatusRequest_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_AspNetUsers_Applicant_id",
                        column: x => x.Applicant_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_AspNetUsers_Cordinating_id",
                        column: x => x.Cordinating_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_StatusRequests_StatusRequest_id",
                        column: x => x.StatusRequest_id,
                        principalTable: "StatusRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_TypesRequests_TypeRequest_id",
                        column: x => x.TypeRequest_id,
                        principalTable: "TypesRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ValuesRequestsAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Request_id = table.Column<int>(type: "int", nullable: true),
                    Attr_TypeRequest_id = table.Column<int>(type: "int", nullable: true),
                    ValueEmloyee_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ValueTypeVocation_id = table.Column<int>(type: "int", nullable: true),
                    ValueText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValueBool = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValuesRequestsAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValuesRequestsAttributes_AspNetUsers_ValueEmloyee_id",
                        column: x => x.ValueEmloyee_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ValuesRequestsAttributes_Attributes_TypesRequests_Attr_TypeRequest_id",
                        column: x => x.Attr_TypeRequest_id,
                        principalTable: "Attributes_TypesRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ValuesRequestsAttributes_Requests_Request_id",
                        column: x => x.Request_id,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ValuesRequestsAttributes_TypesVocations_ValueTypeVocation_id",
                        column: x => x.ValueTypeVocation_id,
                        principalTable: "TypesVocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_TypesRequests_Attr_id",
                table: "Attributes_TypesRequests",
                column: "Attr_id");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_TypesRequests_TypeRequest_id",
                table: "Attributes_TypesRequests",
                column: "TypeRequest_id");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_Applicant_id",
                table: "Requests",
                column: "Applicant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_Cordinating_id",
                table: "Requests",
                column: "Cordinating_id");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_StatusRequest_id",
                table: "Requests",
                column: "StatusRequest_id");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_TypeRequest_id",
                table: "Requests",
                column: "TypeRequest_id");

            migrationBuilder.CreateIndex(
                name: "IX_ValuesRequestsAttributes_Attr_TypeRequest_id",
                table: "ValuesRequestsAttributes",
                column: "Attr_TypeRequest_id");

            migrationBuilder.CreateIndex(
                name: "IX_ValuesRequestsAttributes_Request_id",
                table: "ValuesRequestsAttributes",
                column: "Request_id");

            migrationBuilder.CreateIndex(
                name: "IX_ValuesRequestsAttributes_ValueEmloyee_id",
                table: "ValuesRequestsAttributes",
                column: "ValueEmloyee_id");

            migrationBuilder.CreateIndex(
                name: "IX_ValuesRequestsAttributes_ValueTypeVocation_id",
                table: "ValuesRequestsAttributes",
                column: "ValueTypeVocation_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ValuesRequestsAttributes");

            migrationBuilder.DropTable(
                name: "Attributes_TypesRequests");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "TypesVocations");

            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropTable(
                name: "StatusRequests");

            migrationBuilder.DropTable(
                name: "TypesRequests");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Patronymic",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Post",
                table: "AspNetUsers");
        }
    }
}
