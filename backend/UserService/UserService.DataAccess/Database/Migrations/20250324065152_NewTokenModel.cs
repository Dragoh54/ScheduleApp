using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserService.DataAccess.Database.Migrations
{
    /// <inheritdoc />
    public partial class NewTokenModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bb3d7b10-66d9-42aa-b1df-24445af2c490"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("f06cfd35-4691-4e37-932d-4c351515a973"), new Guid("003952a7-6c42-4bd8-8cd2-a849d17a1097") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("58163706-195e-408e-95bd-c6f4168228a9"), new Guid("68c04a83-164e-4ee9-a8e2-d1cc5796ee1f") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("f06cfd35-4691-4e37-932d-4c351515a973"), new Guid("68c04a83-164e-4ee9-a8e2-d1cc5796ee1f") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("58163706-195e-408e-95bd-c6f4168228a9"), new Guid("83e56a1b-3995-49b5-8553-ba27f7571bcd") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("6b383261-6789-4dc8-9523-658018324d36"), new Guid("83e56a1b-3995-49b5-8553-ba27f7571bcd") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("f06cfd35-4691-4e37-932d-4c351515a973"), new Guid("83e56a1b-3995-49b5-8553-ba27f7571bcd") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("58163706-195e-408e-95bd-c6f4168228a9"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6b383261-6789-4dc8-9523-658018324d36"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f06cfd35-4691-4e37-932d-4c351515a973"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("003952a7-6c42-4bd8-8cd2-a849d17a1097"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("68c04a83-164e-4ee9-a8e2-d1cc5796ee1f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("83e56a1b-3995-49b5-8553-ba27f7571bcd"));

            migrationBuilder.AddColumn<int>(
                name: "Token",
                table: "RefreshTokens",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "Token",
                table: "RefreshTokens");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { new Guid("58163706-195e-408e-95bd-c6f4168228a9"), 2 },
                    { new Guid("6b383261-6789-4dc8-9523-658018324d36"), 3 },
                    { new Guid("bb3d7b10-66d9-42aa-b1df-24445af2c490"), 0 },
                    { new Guid("f06cfd35-4691-4e37-932d-4c351515a973"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "FirstName", "LastName", "PasswordHash", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("003952a7-6c42-4bd8-8cd2-a849d17a1097"), new DateTime(2025, 3, 21, 12, 47, 24, 987, DateTimeKind.Utc).AddTicks(8894), null, "user2@example.com", "Jane", "Doe", "$2a$11$fP8SA4QPz5bfUeIS5ZH8LOE8VXKYY1JH6JYfIk5iCCqs6PyV6d8La", new DateTime(2025, 3, 21, 12, 47, 24, 987, DateTimeKind.Utc).AddTicks(8894), "user2" },
                    { new Guid("68c04a83-164e-4ee9-a8e2-d1cc5796ee1f"), new DateTime(2025, 3, 21, 12, 47, 24, 987, DateTimeKind.Utc).AddTicks(8892), null, "user1@example.com", "John", "Doe", "$2a$11$Vmq/dCwgmETEykLr31YlZez.vX2akujpnGBJoXsRFSL9n112KwOLK", new DateTime(2025, 3, 21, 12, 47, 24, 987, DateTimeKind.Utc).AddTicks(8892), "user1" },
                    { new Guid("83e56a1b-3995-49b5-8553-ba27f7571bcd"), new DateTime(2025, 3, 21, 12, 47, 24, 987, DateTimeKind.Utc).AddTicks(8464), null, "admin@example.com", "Admin", "User", "$2a$11$fMotE2uso.Vl/dInRwOYE.CVTYpLLW9bRv/JxwOcFuISgyclU/XKS", new DateTime(2025, 3, 21, 12, 47, 24, 987, DateTimeKind.Utc).AddTicks(8616), "admin" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("f06cfd35-4691-4e37-932d-4c351515a973"), new Guid("003952a7-6c42-4bd8-8cd2-a849d17a1097") },
                    { new Guid("58163706-195e-408e-95bd-c6f4168228a9"), new Guid("68c04a83-164e-4ee9-a8e2-d1cc5796ee1f") },
                    { new Guid("f06cfd35-4691-4e37-932d-4c351515a973"), new Guid("68c04a83-164e-4ee9-a8e2-d1cc5796ee1f") },
                    { new Guid("58163706-195e-408e-95bd-c6f4168228a9"), new Guid("83e56a1b-3995-49b5-8553-ba27f7571bcd") },
                    { new Guid("6b383261-6789-4dc8-9523-658018324d36"), new Guid("83e56a1b-3995-49b5-8553-ba27f7571bcd") },
                    { new Guid("f06cfd35-4691-4e37-932d-4c351515a973"), new Guid("83e56a1b-3995-49b5-8553-ba27f7571bcd") }
                });
        }
    }
}
