using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkEntityWorkEntity_Works_ChildWorksId",
                table: "WorkEntityWorkEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkEntityWorkEntity_Works_ParentWorksId",
                table: "WorkEntityWorkEntity");

            migrationBuilder.RenameColumn(
                name: "ParentWorksId",
                table: "WorkEntityWorkEntity",
                newName: "PrevWorksId");

            migrationBuilder.RenameColumn(
                name: "ChildWorksId",
                table: "WorkEntityWorkEntity",
                newName: "NextWorksId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkEntityWorkEntity_ParentWorksId",
                table: "WorkEntityWorkEntity",
                newName: "IX_WorkEntityWorkEntity_PrevWorksId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkEntityWorkEntity_Works_NextWorksId",
                table: "WorkEntityWorkEntity",
                column: "NextWorksId",
                principalTable: "Works",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkEntityWorkEntity_Works_PrevWorksId",
                table: "WorkEntityWorkEntity",
                column: "PrevWorksId",
                principalTable: "Works",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkEntityWorkEntity_Works_NextWorksId",
                table: "WorkEntityWorkEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkEntityWorkEntity_Works_PrevWorksId",
                table: "WorkEntityWorkEntity");

            migrationBuilder.RenameColumn(
                name: "PrevWorksId",
                table: "WorkEntityWorkEntity",
                newName: "ParentWorksId");

            migrationBuilder.RenameColumn(
                name: "NextWorksId",
                table: "WorkEntityWorkEntity",
                newName: "ChildWorksId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkEntityWorkEntity_PrevWorksId",
                table: "WorkEntityWorkEntity",
                newName: "IX_WorkEntityWorkEntity_ParentWorksId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkEntityWorkEntity_Works_ChildWorksId",
                table: "WorkEntityWorkEntity",
                column: "ChildWorksId",
                principalTable: "Works",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkEntityWorkEntity_Works_ParentWorksId",
                table: "WorkEntityWorkEntity",
                column: "ParentWorksId",
                principalTable: "Works",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
