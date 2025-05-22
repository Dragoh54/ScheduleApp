using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserService.DataAccess.Database.Migrations
{
    /// <inheritdoc />
    public partial class RenamedTokenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7ff18186-3b3d-4d87-8728-66261e65c7ce"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("6ca81f82-533a-40c5-9f47-b6f970ad5f0f"), new Guid("a1202621-c0a9-49a4-b294-eb2236e4ab4a") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("abc1cb8d-96c5-4d2e-ae46-71df55c9953a"), new Guid("a1202621-c0a9-49a4-b294-eb2236e4ab4a") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("0c58d927-a59c-4982-b635-5b0b87c57b25"), new Guid("a7b34380-830f-4cbf-a758-16fe3e92e7e0") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("6ca81f82-533a-40c5-9f47-b6f970ad5f0f"), new Guid("a7b34380-830f-4cbf-a758-16fe3e92e7e0") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("abc1cb8d-96c5-4d2e-ae46-71df55c9953a"), new Guid("a7b34380-830f-4cbf-a758-16fe3e92e7e0") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("6ca81f82-533a-40c5-9f47-b6f970ad5f0f"), new Guid("d01a1ce4-12c6-4acc-9bb1-50f85164acfe") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0c58d927-a59c-4982-b635-5b0b87c57b25"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6ca81f82-533a-40c5-9f47-b6f970ad5f0f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("abc1cb8d-96c5-4d2e-ae46-71df55c9953a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1202621-c0a9-49a4-b294-eb2236e4ab4a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a7b34380-830f-4cbf-a758-16fe3e92e7e0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d01a1ce4-12c6-4acc-9bb1-50f85164acfe"));

            migrationBuilder.RenameColumn(
                name: "RoleName",
                table: "Roles",
                newName: "RolesName");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RolesName" },
                values: new object[,]
                {
                    { new Guid("69eedaee-d2e6-4493-a4ee-8d2d7eb9310d"), 3 },
                    { new Guid("7cd45859-e536-4e85-af8b-95be916f6c6f"), 1 },
                    { new Guid("8d95b8d6-f349-4ddb-9a09-0fc416d38132"), 0 },
                    { new Guid("90fb01b5-4a28-4b05-bf15-61b1b176b314"), 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "FirstName", "IsConfirmed", "IsDeleted", "LastName", "PasswordHash", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("64d71b71-6617-47f4-83bd-db4d97928ee5"), new DateTime(2025, 3, 28, 7, 35, 27, 308, DateTimeKind.Utc).AddTicks(8711), null, "admin@example.com", "Admin", true, false, "User", "$2a$11$fMotE2uso.Vl/dInRwOYE.CVTYpLLW9bRv/JxwOcFuISgyclU/XKS", new DateTime(2025, 3, 28, 7, 35, 27, 308, DateTimeKind.Utc).AddTicks(8824), "admin" },
                    { new Guid("67b8e8ef-1518-4560-9748-0e9dffb939d8"), new DateTime(2025, 3, 28, 7, 35, 27, 308, DateTimeKind.Utc).AddTicks(9126), null, "user2@example.com", "Jane", false, false, "Doe", "$2a$11$fP8SA4QPz5bfUeIS5ZH8LOE8VXKYY1JH6JYfIk5iCCqs6PyV6d8La", new DateTime(2025, 3, 28, 7, 35, 27, 308, DateTimeKind.Utc).AddTicks(9126), "user2" },
                    { new Guid("e947d533-1dfe-465b-bc2c-f1dae8410063"), new DateTime(2025, 3, 28, 7, 35, 27, 308, DateTimeKind.Utc).AddTicks(9115), null, "user1@example.com", "John", true, false, "Doe", "$2a$11$Vmq/dCwgmETEykLr31YlZez.vX2akujpnGBJoXsRFSL9n112KwOLK", new DateTime(2025, 3, 28, 7, 35, 27, 308, DateTimeKind.Utc).AddTicks(9115), "user1" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("69eedaee-d2e6-4493-a4ee-8d2d7eb9310d"), new Guid("64d71b71-6617-47f4-83bd-db4d97928ee5") },
                    { new Guid("7cd45859-e536-4e85-af8b-95be916f6c6f"), new Guid("64d71b71-6617-47f4-83bd-db4d97928ee5") },
                    { new Guid("90fb01b5-4a28-4b05-bf15-61b1b176b314"), new Guid("64d71b71-6617-47f4-83bd-db4d97928ee5") },
                    { new Guid("7cd45859-e536-4e85-af8b-95be916f6c6f"), new Guid("67b8e8ef-1518-4560-9748-0e9dffb939d8") },
                    { new Guid("7cd45859-e536-4e85-af8b-95be916f6c6f"), new Guid("e947d533-1dfe-465b-bc2c-f1dae8410063") },
                    { new Guid("90fb01b5-4a28-4b05-bf15-61b1b176b314"), new Guid("e947d533-1dfe-465b-bc2c-f1dae8410063") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d95b8d6-f349-4ddb-9a09-0fc416d38132"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("69eedaee-d2e6-4493-a4ee-8d2d7eb9310d"), new Guid("64d71b71-6617-47f4-83bd-db4d97928ee5") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7cd45859-e536-4e85-af8b-95be916f6c6f"), new Guid("64d71b71-6617-47f4-83bd-db4d97928ee5") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("90fb01b5-4a28-4b05-bf15-61b1b176b314"), new Guid("64d71b71-6617-47f4-83bd-db4d97928ee5") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7cd45859-e536-4e85-af8b-95be916f6c6f"), new Guid("67b8e8ef-1518-4560-9748-0e9dffb939d8") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7cd45859-e536-4e85-af8b-95be916f6c6f"), new Guid("e947d533-1dfe-465b-bc2c-f1dae8410063") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("90fb01b5-4a28-4b05-bf15-61b1b176b314"), new Guid("e947d533-1dfe-465b-bc2c-f1dae8410063") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("69eedaee-d2e6-4493-a4ee-8d2d7eb9310d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7cd45859-e536-4e85-af8b-95be916f6c6f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("90fb01b5-4a28-4b05-bf15-61b1b176b314"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("64d71b71-6617-47f4-83bd-db4d97928ee5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("67b8e8ef-1518-4560-9748-0e9dffb939d8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e947d533-1dfe-465b-bc2c-f1dae8410063"));

            migrationBuilder.RenameColumn(
                name: "RolesName",
                table: "Roles",
                newName: "RoleName");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { new Guid("0c58d927-a59c-4982-b635-5b0b87c57b25"), 3 },
                    { new Guid("6ca81f82-533a-40c5-9f47-b6f970ad5f0f"), 1 },
                    { new Guid("7ff18186-3b3d-4d87-8728-66261e65c7ce"), 0 },
                    { new Guid("abc1cb8d-96c5-4d2e-ae46-71df55c9953a"), 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "FirstName", "IsConfirmed", "IsDeleted", "LastName", "PasswordHash", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("a1202621-c0a9-49a4-b294-eb2236e4ab4a"), new DateTime(2025, 3, 27, 9, 7, 22, 985, DateTimeKind.Utc).AddTicks(204), null, "user1@example.com", "John", true, false, "Doe", "$2a$11$Vmq/dCwgmETEykLr31YlZez.vX2akujpnGBJoXsRFSL9n112KwOLK", new DateTime(2025, 3, 27, 9, 7, 22, 985, DateTimeKind.Utc).AddTicks(204), "user1" },
                    { new Guid("a7b34380-830f-4cbf-a758-16fe3e92e7e0"), new DateTime(2025, 3, 27, 9, 7, 22, 984, DateTimeKind.Utc).AddTicks(9771), null, "admin@example.com", "Admin", true, false, "User", "$2a$11$fMotE2uso.Vl/dInRwOYE.CVTYpLLW9bRv/JxwOcFuISgyclU/XKS", new DateTime(2025, 3, 27, 9, 7, 22, 984, DateTimeKind.Utc).AddTicks(9888), "admin" },
                    { new Guid("d01a1ce4-12c6-4acc-9bb1-50f85164acfe"), new DateTime(2025, 3, 27, 9, 7, 22, 985, DateTimeKind.Utc).AddTicks(206), null, "user2@example.com", "Jane", false, false, "Doe", "$2a$11$fP8SA4QPz5bfUeIS5ZH8LOE8VXKYY1JH6JYfIk5iCCqs6PyV6d8La", new DateTime(2025, 3, 27, 9, 7, 22, 985, DateTimeKind.Utc).AddTicks(206), "user2" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("6ca81f82-533a-40c5-9f47-b6f970ad5f0f"), new Guid("a1202621-c0a9-49a4-b294-eb2236e4ab4a") },
                    { new Guid("abc1cb8d-96c5-4d2e-ae46-71df55c9953a"), new Guid("a1202621-c0a9-49a4-b294-eb2236e4ab4a") },
                    { new Guid("0c58d927-a59c-4982-b635-5b0b87c57b25"), new Guid("a7b34380-830f-4cbf-a758-16fe3e92e7e0") },
                    { new Guid("6ca81f82-533a-40c5-9f47-b6f970ad5f0f"), new Guid("a7b34380-830f-4cbf-a758-16fe3e92e7e0") },
                    { new Guid("abc1cb8d-96c5-4d2e-ae46-71df55c9953a"), new Guid("a7b34380-830f-4cbf-a758-16fe3e92e7e0") },
                    { new Guid("6ca81f82-533a-40c5-9f47-b6f970ad5f0f"), new Guid("d01a1ce4-12c6-4acc-9bb1-50f85164acfe") }
                });
        }
    }
}
