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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JobName = table.Column<string>(type: "text", nullable: true),
                    PlannedStartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    FactStartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NewPlannedStartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IncDayCost = table.Column<decimal>(type: "numeric", nullable: false),
                    DecDayCost = table.Column<decimal>(type: "numeric", nullable: false),
                    MinimalDuration = table.Column<int>(type: "integer", nullable: false),
                    NormDuration = table.Column<int>(type: "integer", nullable: false),
                    MinimalDurationCost = table.Column<decimal>(type: "numeric", nullable: false),
                    AddedCost = table.Column<decimal>(type: "numeric", nullable: false),
                    AddedChildrenCost = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkEntityWorkEntity",
                columns: table => new
                {
                    NextWorksId = table.Column<Guid>(type: "uuid", nullable: false),
                    PrevWorksId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkEntityWorkEntity", x => new { x.NextWorksId, x.PrevWorksId });
                    table.ForeignKey(
                        name: "FK_WorkEntityWorkEntity_Works_NextWorksId",
                        column: x => x.NextWorksId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkEntityWorkEntity_Works_PrevWorksId",
                        column: x => x.PrevWorksId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkEntityWorkEntity_PrevWorksId",
                table: "WorkEntityWorkEntity",
                column: "PrevWorksId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Values");

            migrationBuilder.DropTable(
                name: "WorkEntityWorkEntity");

            migrationBuilder.DropTable(
                name: "Works");
        }
    }
}
