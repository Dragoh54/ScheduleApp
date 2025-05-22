using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserService.DataAccess.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3d1cb5ac-26bc-469b-871f-2c79318c5956"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c13a8dd9-ec90-4d82-9ef6-390b709e5bbb"), new Guid("5f43e9a2-17c5-48f7-b5fa-e5885f1b322b") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("8ba2ba06-daba-4688-91d9-476db70a3c00"), new Guid("80176a12-3982-4a68-b225-4645ba599da4") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c13a8dd9-ec90-4d82-9ef6-390b709e5bbb"), new Guid("80176a12-3982-4a68-b225-4645ba599da4") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("3e3bc195-188b-4ac2-b3c4-763c7d2f8b20"), new Guid("ae69614e-7a28-4e4e-9e15-b8587869df59") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("8ba2ba06-daba-4688-91d9-476db70a3c00"), new Guid("ae69614e-7a28-4e4e-9e15-b8587869df59") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c13a8dd9-ec90-4d82-9ef6-390b709e5bbb"), new Guid("ae69614e-7a28-4e4e-9e15-b8587869df59") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3e3bc195-188b-4ac2-b3c4-763c7d2f8b20"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8ba2ba06-daba-4688-91d9-476db70a3c00"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c13a8dd9-ec90-4d82-9ef6-390b709e5bbb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5f43e9a2-17c5-48f7-b5fa-e5885f1b322b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("80176a12-3982-4a68-b225-4645ba599da4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ae69614e-7a28-4e4e-9e15-b8587869df59"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
