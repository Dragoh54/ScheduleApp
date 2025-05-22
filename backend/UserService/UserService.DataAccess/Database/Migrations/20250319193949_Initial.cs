using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserService.DataAccess.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleName = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    LastName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { new Guid("3d1cb5ac-26bc-469b-871f-2c79318c5956"), 0 },
                    { new Guid("3e3bc195-188b-4ac2-b3c4-763c7d2f8b20"), 3 },
                    { new Guid("8ba2ba06-daba-4688-91d9-476db70a3c00"), 2 },
                    { new Guid("c13a8dd9-ec90-4d82-9ef6-390b709e5bbb"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "FirstName", "LastName", "PasswordHash", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("5f43e9a2-17c5-48f7-b5fa-e5885f1b322b"), new DateTime(2025, 3, 19, 19, 39, 48, 781, DateTimeKind.Utc).AddTicks(5275), null, "user2@example.com", "Jane", "Doe", "$2a$11$fP8SA4QPz5bfUeIS5ZH8LOE8VXKYY1JH6JYfIk5iCCqs6PyV6d8La", new DateTime(2025, 3, 19, 19, 39, 48, 781, DateTimeKind.Utc).AddTicks(5280), "user2" },
                    { new Guid("80176a12-3982-4a68-b225-4645ba599da4"), new DateTime(2025, 3, 19, 19, 39, 48, 694, DateTimeKind.Utc).AddTicks(2502), null, "user1@example.com", "John", "Doe", "$2a$11$Vmq/dCwgmETEykLr31YlZez.vX2akujpnGBJoXsRFSL9n112KwOLK", new DateTime(2025, 3, 19, 19, 39, 48, 694, DateTimeKind.Utc).AddTicks(2507), "user1" },
                    { new Guid("ae69614e-7a28-4e4e-9e15-b8587869df59"), new DateTime(2025, 3, 19, 19, 39, 48, 606, DateTimeKind.Utc).AddTicks(9516), null, "admin@example.com", "Admin", "User", "$2a$11$fMotE2uso.Vl/dInRwOYE.CVTYpLLW9bRv/JxwOcFuISgyclU/XKS", new DateTime(2025, 3, 19, 19, 39, 48, 606, DateTimeKind.Utc).AddTicks(9680), "admin" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("c13a8dd9-ec90-4d82-9ef6-390b709e5bbb"), new Guid("5f43e9a2-17c5-48f7-b5fa-e5885f1b322b") },
                    { new Guid("8ba2ba06-daba-4688-91d9-476db70a3c00"), new Guid("80176a12-3982-4a68-b225-4645ba599da4") },
                    { new Guid("c13a8dd9-ec90-4d82-9ef6-390b709e5bbb"), new Guid("80176a12-3982-4a68-b225-4645ba599da4") },
                    { new Guid("3e3bc195-188b-4ac2-b3c4-763c7d2f8b20"), new Guid("ae69614e-7a28-4e4e-9e15-b8587869df59") },
                    { new Guid("8ba2ba06-daba-4688-91d9-476db70a3c00"), new Guid("ae69614e-7a28-4e4e-9e15-b8587869df59") },
                    { new Guid("c13a8dd9-ec90-4d82-9ef6-390b709e5bbb"), new Guid("ae69614e-7a28-4e4e-9e15-b8587869df59") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
