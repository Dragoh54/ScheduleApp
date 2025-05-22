using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserService.DataAccess.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddTokenStringToTokenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1adc37e9-f47b-4e06-9a1d-ed5f0cae09a3"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("41187b60-757a-4699-91e4-ddc2886a15e1"), new Guid("0354cf34-a554-4d40-8a52-00343777d7a4") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("5599d238-8112-4100-8705-5e1511b95017"), new Guid("0354cf34-a554-4d40-8a52-00343777d7a4") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("8559f703-9d61-42ef-973b-434b6bc5cafe"), new Guid("0354cf34-a554-4d40-8a52-00343777d7a4") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("5599d238-8112-4100-8705-5e1511b95017"), new Guid("c0771ca1-dcdd-4789-ab82-a13cc997a967") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("8559f703-9d61-42ef-973b-434b6bc5cafe"), new Guid("c0771ca1-dcdd-4789-ab82-a13cc997a967") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("8559f703-9d61-42ef-973b-434b6bc5cafe"), new Guid("efd12259-043e-45b6-8595-cc3d8b6fbf5c") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("41187b60-757a-4699-91e4-ddc2886a15e1"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5599d238-8112-4100-8705-5e1511b95017"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8559f703-9d61-42ef-973b-434b6bc5cafe"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0354cf34-a554-4d40-8a52-00343777d7a4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c0771ca1-dcdd-4789-ab82-a13cc997a967"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("efd12259-043e-45b6-8595-cc3d8b6fbf5c"));

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "Tokens",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "TokenType",
                table: "Tokens",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "TokenType",
                table: "Tokens");

            migrationBuilder.AlterColumn<int>(
                name: "Token",
                table: "Tokens",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { new Guid("1adc37e9-f47b-4e06-9a1d-ed5f0cae09a3"), 0 },
                    { new Guid("41187b60-757a-4699-91e4-ddc2886a15e1"), 3 },
                    { new Guid("5599d238-8112-4100-8705-5e1511b95017"), 2 },
                    { new Guid("8559f703-9d61-42ef-973b-434b6bc5cafe"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "FirstName", "LastName", "PasswordHash", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("0354cf34-a554-4d40-8a52-00343777d7a4"), new DateTime(2025, 3, 24, 13, 20, 16, 866, DateTimeKind.Utc).AddTicks(4746), null, "admin@example.com", "Admin", "User", "$2a$11$fMotE2uso.Vl/dInRwOYE.CVTYpLLW9bRv/JxwOcFuISgyclU/XKS", new DateTime(2025, 3, 24, 13, 20, 16, 866, DateTimeKind.Utc).AddTicks(4857), "admin" },
                    { new Guid("c0771ca1-dcdd-4789-ab82-a13cc997a967"), new DateTime(2025, 3, 24, 13, 20, 16, 866, DateTimeKind.Utc).AddTicks(5108), null, "user1@example.com", "John", "Doe", "$2a$11$Vmq/dCwgmETEykLr31YlZez.vX2akujpnGBJoXsRFSL9n112KwOLK", new DateTime(2025, 3, 24, 13, 20, 16, 866, DateTimeKind.Utc).AddTicks(5108), "user1" },
                    { new Guid("efd12259-043e-45b6-8595-cc3d8b6fbf5c"), new DateTime(2025, 3, 24, 13, 20, 16, 866, DateTimeKind.Utc).AddTicks(5110), null, "user2@example.com", "Jane", "Doe", "$2a$11$fP8SA4QPz5bfUeIS5ZH8LOE8VXKYY1JH6JYfIk5iCCqs6PyV6d8La", new DateTime(2025, 3, 24, 13, 20, 16, 866, DateTimeKind.Utc).AddTicks(5110), "user2" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("41187b60-757a-4699-91e4-ddc2886a15e1"), new Guid("0354cf34-a554-4d40-8a52-00343777d7a4") },
                    { new Guid("5599d238-8112-4100-8705-5e1511b95017"), new Guid("0354cf34-a554-4d40-8a52-00343777d7a4") },
                    { new Guid("8559f703-9d61-42ef-973b-434b6bc5cafe"), new Guid("0354cf34-a554-4d40-8a52-00343777d7a4") },
                    { new Guid("5599d238-8112-4100-8705-5e1511b95017"), new Guid("c0771ca1-dcdd-4789-ab82-a13cc997a967") },
                    { new Guid("8559f703-9d61-42ef-973b-434b6bc5cafe"), new Guid("c0771ca1-dcdd-4789-ab82-a13cc997a967") },
                    { new Guid("8559f703-9d61-42ef-973b-434b6bc5cafe"), new Guid("efd12259-043e-45b6-8595-cc3d8b6fbf5c") }
                });
        }
    }
}
