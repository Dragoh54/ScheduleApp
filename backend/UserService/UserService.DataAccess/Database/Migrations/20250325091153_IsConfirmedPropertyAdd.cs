using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserService.DataAccess.Database.Migrations
{
    /// <inheritdoc />
    public partial class IsConfirmedPropertyAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c5478671-3600-42df-9f03-5f2350c4f034"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("de1eeff9-94fc-4eb4-9903-ded1fdf0c7db"), new Guid("455bc0d7-2ab0-46b6-b543-b078ee78132f") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("36d3c130-f67d-4701-971e-5135244a8a4e"), new Guid("dc72797e-4041-407b-b784-0567d4248783") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("90d2e819-ddcd-4fd0-962e-fbc66efe5a57"), new Guid("dc72797e-4041-407b-b784-0567d4248783") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("de1eeff9-94fc-4eb4-9903-ded1fdf0c7db"), new Guid("dc72797e-4041-407b-b784-0567d4248783") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("90d2e819-ddcd-4fd0-962e-fbc66efe5a57"), new Guid("e68b001e-54a2-49e7-999c-14e7e5c7492e") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("de1eeff9-94fc-4eb4-9903-ded1fdf0c7db"), new Guid("e68b001e-54a2-49e7-999c-14e7e5c7492e") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("36d3c130-f67d-4701-971e-5135244a8a4e"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("90d2e819-ddcd-4fd0-962e-fbc66efe5a57"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("de1eeff9-94fc-4eb4-9903-ded1fdf0c7db"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("455bc0d7-2ab0-46b6-b543-b078ee78132f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("dc72797e-4041-407b-b784-0567d4248783"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e68b001e-54a2-49e7-999c-14e7e5c7492e"));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { new Guid("b865f0c0-e730-49a5-8c80-9b9b4f5b8d56"), 2 },
                    { new Guid("b9042180-949b-470c-9ebb-4fb6b1b27404"), 3 },
                    { new Guid("b92bca83-5a80-4323-8c93-635e3486998c"), 0 },
                    { new Guid("c7ed4f3b-e0e3-40e1-8dda-0d5c7b201a0c"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "FirstName", "IsConfirmed", "IsDeleted", "LastName", "PasswordHash", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("5d360b8e-d55d-45dc-86a0-994062809dab"), new DateTime(2025, 3, 25, 9, 11, 52, 147, DateTimeKind.Utc).AddTicks(6294), null, "admin@example.com", "Admin", true, false, "User", "$2a$11$fMotE2uso.Vl/dInRwOYE.CVTYpLLW9bRv/JxwOcFuISgyclU/XKS", new DateTime(2025, 3, 25, 9, 11, 52, 147, DateTimeKind.Utc).AddTicks(6400), "admin" },
                    { new Guid("727dd265-0f1a-48f3-bce0-7fe085e92310"), new DateTime(2025, 3, 25, 9, 11, 52, 147, DateTimeKind.Utc).AddTicks(6704), null, "user2@example.com", "Jane", false, false, "Doe", "$2a$11$fP8SA4QPz5bfUeIS5ZH8LOE8VXKYY1JH6JYfIk5iCCqs6PyV6d8La", new DateTime(2025, 3, 25, 9, 11, 52, 147, DateTimeKind.Utc).AddTicks(6704), "user2" },
                    { new Guid("8f841611-a026-48a1-aee8-669d5760a45f"), new DateTime(2025, 3, 25, 9, 11, 52, 147, DateTimeKind.Utc).AddTicks(6701), null, "user1@example.com", "John", true, false, "Doe", "$2a$11$Vmq/dCwgmETEykLr31YlZez.vX2akujpnGBJoXsRFSL9n112KwOLK", new DateTime(2025, 3, 25, 9, 11, 52, 147, DateTimeKind.Utc).AddTicks(6702), "user1" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("b865f0c0-e730-49a5-8c80-9b9b4f5b8d56"), new Guid("5d360b8e-d55d-45dc-86a0-994062809dab") },
                    { new Guid("b9042180-949b-470c-9ebb-4fb6b1b27404"), new Guid("5d360b8e-d55d-45dc-86a0-994062809dab") },
                    { new Guid("c7ed4f3b-e0e3-40e1-8dda-0d5c7b201a0c"), new Guid("5d360b8e-d55d-45dc-86a0-994062809dab") },
                    { new Guid("c7ed4f3b-e0e3-40e1-8dda-0d5c7b201a0c"), new Guid("727dd265-0f1a-48f3-bce0-7fe085e92310") },
                    { new Guid("b865f0c0-e730-49a5-8c80-9b9b4f5b8d56"), new Guid("8f841611-a026-48a1-aee8-669d5760a45f") },
                    { new Guid("c7ed4f3b-e0e3-40e1-8dda-0d5c7b201a0c"), new Guid("8f841611-a026-48a1-aee8-669d5760a45f") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b92bca83-5a80-4323-8c93-635e3486998c"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("b865f0c0-e730-49a5-8c80-9b9b4f5b8d56"), new Guid("5d360b8e-d55d-45dc-86a0-994062809dab") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("b9042180-949b-470c-9ebb-4fb6b1b27404"), new Guid("5d360b8e-d55d-45dc-86a0-994062809dab") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c7ed4f3b-e0e3-40e1-8dda-0d5c7b201a0c"), new Guid("5d360b8e-d55d-45dc-86a0-994062809dab") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c7ed4f3b-e0e3-40e1-8dda-0d5c7b201a0c"), new Guid("727dd265-0f1a-48f3-bce0-7fe085e92310") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("b865f0c0-e730-49a5-8c80-9b9b4f5b8d56"), new Guid("8f841611-a026-48a1-aee8-669d5760a45f") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c7ed4f3b-e0e3-40e1-8dda-0d5c7b201a0c"), new Guid("8f841611-a026-48a1-aee8-669d5760a45f") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b865f0c0-e730-49a5-8c80-9b9b4f5b8d56"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b9042180-949b-470c-9ebb-4fb6b1b27404"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c7ed4f3b-e0e3-40e1-8dda-0d5c7b201a0c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5d360b8e-d55d-45dc-86a0-994062809dab"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("727dd265-0f1a-48f3-bce0-7fe085e92310"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8f841611-a026-48a1-aee8-669d5760a45f"));

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Users");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { new Guid("36d3c130-f67d-4701-971e-5135244a8a4e"), 3 },
                    { new Guid("90d2e819-ddcd-4fd0-962e-fbc66efe5a57"), 2 },
                    { new Guid("c5478671-3600-42df-9f03-5f2350c4f034"), 0 },
                    { new Guid("de1eeff9-94fc-4eb4-9903-ded1fdf0c7db"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "FirstName", "LastName", "PasswordHash", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("455bc0d7-2ab0-46b6-b543-b078ee78132f"), new DateTime(2025, 3, 24, 14, 6, 10, 218, DateTimeKind.Utc).AddTicks(3568), null, "user2@example.com", "Jane", "Doe", "$2a$11$fP8SA4QPz5bfUeIS5ZH8LOE8VXKYY1JH6JYfIk5iCCqs6PyV6d8La", new DateTime(2025, 3, 24, 14, 6, 10, 218, DateTimeKind.Utc).AddTicks(3568), "user2" },
                    { new Guid("dc72797e-4041-407b-b784-0567d4248783"), new DateTime(2025, 3, 24, 14, 6, 10, 218, DateTimeKind.Utc).AddTicks(3208), null, "admin@example.com", "Admin", "User", "$2a$11$fMotE2uso.Vl/dInRwOYE.CVTYpLLW9bRv/JxwOcFuISgyclU/XKS", new DateTime(2025, 3, 24, 14, 6, 10, 218, DateTimeKind.Utc).AddTicks(3316), "admin" },
                    { new Guid("e68b001e-54a2-49e7-999c-14e7e5c7492e"), new DateTime(2025, 3, 24, 14, 6, 10, 218, DateTimeKind.Utc).AddTicks(3565), null, "user1@example.com", "John", "Doe", "$2a$11$Vmq/dCwgmETEykLr31YlZez.vX2akujpnGBJoXsRFSL9n112KwOLK", new DateTime(2025, 3, 24, 14, 6, 10, 218, DateTimeKind.Utc).AddTicks(3565), "user1" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("de1eeff9-94fc-4eb4-9903-ded1fdf0c7db"), new Guid("455bc0d7-2ab0-46b6-b543-b078ee78132f") },
                    { new Guid("36d3c130-f67d-4701-971e-5135244a8a4e"), new Guid("dc72797e-4041-407b-b784-0567d4248783") },
                    { new Guid("90d2e819-ddcd-4fd0-962e-fbc66efe5a57"), new Guid("dc72797e-4041-407b-b784-0567d4248783") },
                    { new Guid("de1eeff9-94fc-4eb4-9903-ded1fdf0c7db"), new Guid("dc72797e-4041-407b-b784-0567d4248783") },
                    { new Guid("90d2e819-ddcd-4fd0-962e-fbc66efe5a57"), new Guid("e68b001e-54a2-49e7-999c-14e7e5c7492e") },
                    { new Guid("de1eeff9-94fc-4eb4-9903-ded1fdf0c7db"), new Guid("e68b001e-54a2-49e7-999c-14e7e5c7492e") }
                });
        }
    }
}
