using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Entities.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Values",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PlannedStartDate = table.Column<DateTime>(nullable: false),
                    FactStartDate = table.Column<DateTime>(nullable: true),
                    NewPlannedStartDate = table.Column<DateTime>(nullable: true),
                    IncDayCost = table.Column<decimal>(nullable: false),
                    DecDayCost = table.Column<decimal>(nullable: false),
                    MinimalDuration = table.Column<int>(nullable: false),
                    NormDuration = table.Column<int>(nullable: false),
                    MinimalDurationCost = table.Column<decimal>(nullable: false),
                    AddedCost = table.Column<decimal>(nullable: false),
                    AddedChildrenCost = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkWorkEntity",
                columns: table => new
                {
                    PrevWorkId = table.Column<Guid>(nullable: false),
                    NextWorkId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkWorkEntity", x => new { x.NextWorkId, x.PrevWorkId });
                    table.ForeignKey(
                        name: "FK_WorkWorkEntity_Works_NextWorkId",
                        column: x => x.NextWorkId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkWorkEntity_Works_PrevWorkId",
                        column: x => x.PrevWorkId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkWorkEntity_PrevWorkId",
                table: "WorkWorkEntity",
                column: "PrevWorkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Values");

            migrationBuilder.DropTable(
                name: "WorkWorkEntity");

            migrationBuilder.DropTable(
                name: "Works");
        }
    }
}
