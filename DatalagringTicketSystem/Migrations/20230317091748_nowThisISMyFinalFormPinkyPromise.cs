using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatalagringTicketSystem.Migrations
{
    /// <inheritdoc />
    public partial class nowThisISMyFinalFormPinkyPromise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tickets_TicketId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "TicketId",
                table: "Comments",
                newName: "TicketNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_TicketId",
                table: "Comments",
                newName: "IX_Comments_TicketNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tickets_TicketNumber",
                table: "Comments",
                column: "TicketNumber",
                principalTable: "Tickets",
                principalColumn: "TicketNumber",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tickets_TicketNumber",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "TicketNumber",
                table: "Comments",
                newName: "TicketId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_TicketNumber",
                table: "Comments",
                newName: "IX_Comments_TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tickets_TicketId",
                table: "Comments",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "TicketNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
