using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestDevCore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ExterRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovementDetails_Movements_MovementId1",
                table: "MovementDetails");

            migrationBuilder.DropIndex(
                name: "IX_MovementDetails_MovementId1",
                table: "MovementDetails");

            migrationBuilder.DropColumn(
                name: "MovementId1",
                table: "MovementDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MovementId1",
                table: "MovementDetails",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovementDetails_MovementId1",
                table: "MovementDetails",
                column: "MovementId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MovementDetails_Movements_MovementId1",
                table: "MovementDetails",
                column: "MovementId1",
                principalTable: "Movements",
                principalColumn: "Id");
        }
    }
}
