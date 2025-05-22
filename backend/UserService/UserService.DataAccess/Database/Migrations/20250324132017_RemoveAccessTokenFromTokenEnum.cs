using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserService.DataAccess.Database.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAccessTokenFromTokenEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("df15b981-6275-49c1-b94c-0de7eba98bac"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("1c1b9d23-49e4-4ab1-b970-ce17d67b3035"), new Guid("20bf313c-eb72-42e7-8346-6e1a7667c6fc") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("9eee284a-57b4-496c-8ca6-660c6d848d6a"), new Guid("20bf313c-eb72-42e7-8346-6e1a7667c6fc") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("f63116d0-a0f4-4cdb-aae4-1d635f655f12"), new Guid("20bf313c-eb72-42e7-8346-6e1a7667c6fc") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("f63116d0-a0f4-4cdb-aae4-1d635f655f12"), new Guid("937efb8c-804a-429a-871b-33bfc4efec3e") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("9eee284a-57b4-496c-8ca6-660c6d848d6a"), new Guid("b2f3c585-7a76-46e1-95e4-5862fb21b50f") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("f63116d0-a0f4-4cdb-aae4-1d635f655f12"), new Guid("b2f3c585-7a76-46e1-95e4-5862fb21b50f") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1c1b9d23-49e4-4ab1-b970-ce17d67b3035"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9eee284a-57b4-496c-8ca6-660c6d848d6a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f63116d0-a0f4-4cdb-aae4-1d635f655f12"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("20bf313c-eb72-42e7-8346-6e1a7667c6fc"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("937efb8c-804a-429a-871b-33bfc4efec3e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2f3c585-7a76-46e1-95e4-5862fb21b50f"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { new Guid("1c1b9d23-49e4-4ab1-b970-ce17d67b3035"), 3 },
                    { new Guid("9eee284a-57b4-496c-8ca6-660c6d848d6a"), 2 },
                    { new Guid("df15b981-6275-49c1-b94c-0de7eba98bac"), 0 },
                    { new Guid("f63116d0-a0f4-4cdb-aae4-1d635f655f12"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "FirstName", "LastName", "PasswordHash", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("20bf313c-eb72-42e7-8346-6e1a7667c6fc"), new DateTime(2025, 3, 24, 6, 54, 42, 427, DateTimeKind.Utc).AddTicks(9087), null, "admin@example.com", "Admin", "User", "$2a$11$fMotE2uso.Vl/dInRwOYE.CVTYpLLW9bRv/JxwOcFuISgyclU/XKS", new DateTime(2025, 3, 24, 6, 54, 42, 427, DateTimeKind.Utc).AddTicks(9206), "admin" },
                    { new Guid("937efb8c-804a-429a-871b-33bfc4efec3e"), new DateTime(2025, 3, 24, 6, 54, 42, 427, DateTimeKind.Utc).AddTicks(9432), null, "user2@example.com", "Jane", "Doe", "$2a$11$fP8SA4QPz5bfUeIS5ZH8LOE8VXKYY1JH6JYfIk5iCCqs6PyV6d8La", new DateTime(2025, 3, 24, 6, 54, 42, 427, DateTimeKind.Utc).AddTicks(9433), "user2" },
                    { new Guid("b2f3c585-7a76-46e1-95e4-5862fb21b50f"), new DateTime(2025, 3, 24, 6, 54, 42, 427, DateTimeKind.Utc).AddTicks(9430), null, "user1@example.com", "John", "Doe", "$2a$11$Vmq/dCwgmETEykLr31YlZez.vX2akujpnGBJoXsRFSL9n112KwOLK", new DateTime(2025, 3, 24, 6, 54, 42, 427, DateTimeKind.Utc).AddTicks(9430), "user1" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("1c1b9d23-49e4-4ab1-b970-ce17d67b3035"), new Guid("20bf313c-eb72-42e7-8346-6e1a7667c6fc") },
                    { new Guid("9eee284a-57b4-496c-8ca6-660c6d848d6a"), new Guid("20bf313c-eb72-42e7-8346-6e1a7667c6fc") },
                    { new Guid("f63116d0-a0f4-4cdb-aae4-1d635f655f12"), new Guid("20bf313c-eb72-42e7-8346-6e1a7667c6fc") },
                    { new Guid("f63116d0-a0f4-4cdb-aae4-1d635f655f12"), new Guid("937efb8c-804a-429a-871b-33bfc4efec3e") },
                    { new Guid("9eee284a-57b4-496c-8ca6-660c6d848d6a"), new Guid("b2f3c585-7a76-46e1-95e4-5862fb21b50f") },
                    { new Guid("f63116d0-a0f4-4cdb-aae4-1d635f655f12"), new Guid("b2f3c585-7a76-46e1-95e4-5862fb21b50f") }
                });
        }
    }
}
