using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserService.DataAccess.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTokenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bd90067f-362e-4d85-a869-4242e8f6a70d"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("00b87896-bb05-4bf2-bd15-9558c0173f9a"), new Guid("10925bb9-81d6-4a50-927e-8f28a2d4a6f0") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d0a4ffe3-1343-4b5b-895d-4a0121b2190a"), new Guid("10925bb9-81d6-4a50-927e-8f28a2d4a6f0") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("e1c125ab-67ed-4267-9664-0688bc6e64ff"), new Guid("10925bb9-81d6-4a50-927e-8f28a2d4a6f0") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("00b87896-bb05-4bf2-bd15-9558c0173f9a"), new Guid("279afb52-89e7-48a5-bae5-3c60e40b91fc") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d0a4ffe3-1343-4b5b-895d-4a0121b2190a"), new Guid("279afb52-89e7-48a5-bae5-3c60e40b91fc") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("00b87896-bb05-4bf2-bd15-9558c0173f9a"), new Guid("fec513fd-cb5b-4352-8434-27fe702825c5") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00b87896-bb05-4bf2-bd15-9558c0173f9a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d0a4ffe3-1343-4b5b-895d-4a0121b2190a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e1c125ab-67ed-4267-9664-0688bc6e64ff"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("10925bb9-81d6-4a50-927e-8f28a2d4a6f0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("279afb52-89e7-48a5-bae5-3c60e40b91fc"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fec513fd-cb5b-4352-8434-27fe702825c5"));

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "Tokens");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tokens",
                table: "Tokens",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tokens",
                table: "Tokens");

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

            migrationBuilder.RenameTable(
                name: "Tokens",
                newName: "RefreshTokens");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { new Guid("00b87896-bb05-4bf2-bd15-9558c0173f9a"), 1 },
                    { new Guid("bd90067f-362e-4d85-a869-4242e8f6a70d"), 0 },
                    { new Guid("d0a4ffe3-1343-4b5b-895d-4a0121b2190a"), 2 },
                    { new Guid("e1c125ab-67ed-4267-9664-0688bc6e64ff"), 3 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "FirstName", "LastName", "PasswordHash", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("10925bb9-81d6-4a50-927e-8f28a2d4a6f0"), new DateTime(2025, 3, 24, 6, 51, 52, 8, DateTimeKind.Utc).AddTicks(1781), null, "admin@example.com", "Admin", "User", "$2a$11$fMotE2uso.Vl/dInRwOYE.CVTYpLLW9bRv/JxwOcFuISgyclU/XKS", new DateTime(2025, 3, 24, 6, 51, 52, 8, DateTimeKind.Utc).AddTicks(1889), "admin" },
                    { new Guid("279afb52-89e7-48a5-bae5-3c60e40b91fc"), new DateTime(2025, 3, 24, 6, 51, 52, 8, DateTimeKind.Utc).AddTicks(2154), null, "user1@example.com", "John", "Doe", "$2a$11$Vmq/dCwgmETEykLr31YlZez.vX2akujpnGBJoXsRFSL9n112KwOLK", new DateTime(2025, 3, 24, 6, 51, 52, 8, DateTimeKind.Utc).AddTicks(2154), "user1" },
                    { new Guid("fec513fd-cb5b-4352-8434-27fe702825c5"), new DateTime(2025, 3, 24, 6, 51, 52, 8, DateTimeKind.Utc).AddTicks(2156), null, "user2@example.com", "Jane", "Doe", "$2a$11$fP8SA4QPz5bfUeIS5ZH8LOE8VXKYY1JH6JYfIk5iCCqs6PyV6d8La", new DateTime(2025, 3, 24, 6, 51, 52, 8, DateTimeKind.Utc).AddTicks(2156), "user2" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("00b87896-bb05-4bf2-bd15-9558c0173f9a"), new Guid("10925bb9-81d6-4a50-927e-8f28a2d4a6f0") },
                    { new Guid("d0a4ffe3-1343-4b5b-895d-4a0121b2190a"), new Guid("10925bb9-81d6-4a50-927e-8f28a2d4a6f0") },
                    { new Guid("e1c125ab-67ed-4267-9664-0688bc6e64ff"), new Guid("10925bb9-81d6-4a50-927e-8f28a2d4a6f0") },
                    { new Guid("00b87896-bb05-4bf2-bd15-9558c0173f9a"), new Guid("279afb52-89e7-48a5-bae5-3c60e40b91fc") },
                    { new Guid("d0a4ffe3-1343-4b5b-895d-4a0121b2190a"), new Guid("279afb52-89e7-48a5-bae5-3c60e40b91fc") },
                    { new Guid("00b87896-bb05-4bf2-bd15-9558c0173f9a"), new Guid("fec513fd-cb5b-4352-8434-27fe702825c5") }
                });
        }
    }
}
