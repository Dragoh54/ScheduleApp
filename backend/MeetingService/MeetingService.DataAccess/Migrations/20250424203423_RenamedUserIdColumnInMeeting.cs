using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenamedUserIdColumnInMeeting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Meetings",
                newName: "OrganizationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Meetings_UserId",
                table: "Meetings",
                newName: "IX_Meetings_OrganizationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrganizationUserId",
                table: "Meetings",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Meetings_OrganizationUserId",
                table: "Meetings",
                newName: "IX_Meetings_UserId");
        }
    }
}
