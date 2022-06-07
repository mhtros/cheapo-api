using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cheapo.Api.Data.Migrations
{
    public partial class TransactionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "06a5fda9-f335-4c45-b9be-e3a1490aeb6d");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "09b3e5e5-9109-464b-99ab-1d831dd94da7");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "0d0efd48-8469-478d-8b16-735722f86248");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "14b887d2-bfba-4715-9216-d05bfff839ab");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "17f1457a-0e65-43ce-b079-3b91ef9ec449");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "20330688-f1b8-42f2-a1ca-8e2a136ca659");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "27868c8b-c7d3-4f55-93dd-35649b1635ea");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "2a2a333e-16ab-4c57-8c02-3170c639ab00");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "31758331-212f-4b02-b25f-cfb215293d5c");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "4b36d905-0cd9-439b-ac6d-a3006779d023");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "62f06d0d-8009-4952-87f0-6005b069f5d6");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "67cd6d07-8d61-4092-8b10-4dc8c2190bc9");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "81250494-4260-4772-b299-f9721131edf2");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "81d136fc-abfe-4aea-98fd-2ec1cc37005c");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "a16615a5-391f-4812-94df-d31319ba33f2");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "b127229c-85bf-484c-ad0b-0d26c87815e2");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "b5992054-108c-4910-a5f1-d67c822e4073");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "ba5ba1fc-d1ff-42d3-97fe-c7edfeee1a0e");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "bb43c768-2d60-4008-a9f4-21137377033a");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "ce8330f8-7efa-465a-96a9-cad9b0b57bb7");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "d0e13c6a-757b-4a45-b8f1-c64b4ae27385");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "f4c03752-8b92-4b96-bc28-96339be8084b");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "f9f7f1de-1ad6-4bce-8a53-b27a1ef403f6");

            migrationBuilder.CreateTable(
                name: "ApplicationTransactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Comments = table.Column<string>(type: "text", nullable: true),
                    IsExpense = table.Column<bool>(type: "boolean", nullable: false),
                    CategoryId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationTransactions_ApplicationTransactionCategories_Ca~",
                        column: x => x.CategoryId,
                        principalTable: "ApplicationTransactionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationTransactions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ApplicationTransactionCategories",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[,]
                {
                    { "0e7fec9e-7d96-490e-9b6b-b94c2094ab5b", "Entertainment", null },
                    { "13369ef9-4ffe-462f-b351-506da3aa8afe", "Uncategorized", null },
                    { "1952ab46-39f2-4a56-938b-8ec2b56ef2fd", "Pets", null },
                    { "1b4cc567-a573-4ead-b88e-13ebc2475f42", "Retirement", null },
                    { "2a5973a3-2191-4bc6-8d8d-3772a502e664", "Insurance", null },
                    { "3aa387a9-1789-4666-ba81-dbed6429da0e", "Education", null },
                    { "3dfab70a-edb1-416f-a613-549b9cda6614", "Debt", null },
                    { "7153294b-fa68-4ded-8cf1-2b63c9233665", "Food", null },
                    { "71670fa9-baeb-4d26-94de-1e415f27a69c", "Transportation", null },
                    { "72f31082-0443-414b-a098-395c0696804c", "Personal", null },
                    { "77fe6763-71f5-46ab-bf28-f7deb15b17f2", "Travel", null },
                    { "8eff62a4-bfe7-4d3f-b17c-d71811a70090", "Other", null },
                    { "9a5543e6-3b81-4887-a2f4-835d22cbcc37", "Savings", null },
                    { "a4ef6cc5-03f1-4c5d-8e88-675f13e1c750", "Technology", null },
                    { "aea685f2-9bde-4feb-9af3-878d76e72f25", "Utilities", null },
                    { "af6110a6-ff72-4e0f-aa35-67f6a8915962", "Gift/Donations", null },
                    { "b387c55a-a814-4259-9a50-fc34a156e122", "Household Supplies", null },
                    { "b45dff03-d42a-4432-bd92-4e47f4d10088", "Clothing", null },
                    { "c1ba7f07-c589-4644-b531-5680dca8abfb", "Kids", null },
                    { "c90b963f-fc2f-4377-9cae-b3ad9146a954", "Bank", null },
                    { "df6ff33f-72ff-4902-8c23-b936065e95b0", "Medical/Health Care", null },
                    { "e15476c5-aa63-4c96-8fac-15e4817a5b0d", "Housing", null },
                    { "fb843781-93ba-4203-991a-6ed2dca794f5", "Payroll", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationTransactions_CategoryId",
                table: "ApplicationTransactions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationTransactions_UserId",
                table: "ApplicationTransactions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationTransactions");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "0e7fec9e-7d96-490e-9b6b-b94c2094ab5b");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "13369ef9-4ffe-462f-b351-506da3aa8afe");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "1952ab46-39f2-4a56-938b-8ec2b56ef2fd");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "1b4cc567-a573-4ead-b88e-13ebc2475f42");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "2a5973a3-2191-4bc6-8d8d-3772a502e664");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "3aa387a9-1789-4666-ba81-dbed6429da0e");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "3dfab70a-edb1-416f-a613-549b9cda6614");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "7153294b-fa68-4ded-8cf1-2b63c9233665");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "71670fa9-baeb-4d26-94de-1e415f27a69c");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "72f31082-0443-414b-a098-395c0696804c");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "77fe6763-71f5-46ab-bf28-f7deb15b17f2");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "8eff62a4-bfe7-4d3f-b17c-d71811a70090");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "9a5543e6-3b81-4887-a2f4-835d22cbcc37");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "a4ef6cc5-03f1-4c5d-8e88-675f13e1c750");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "aea685f2-9bde-4feb-9af3-878d76e72f25");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "af6110a6-ff72-4e0f-aa35-67f6a8915962");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "b387c55a-a814-4259-9a50-fc34a156e122");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "b45dff03-d42a-4432-bd92-4e47f4d10088");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "c1ba7f07-c589-4644-b531-5680dca8abfb");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "c90b963f-fc2f-4377-9cae-b3ad9146a954");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "df6ff33f-72ff-4902-8c23-b936065e95b0");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "e15476c5-aa63-4c96-8fac-15e4817a5b0d");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "fb843781-93ba-4203-991a-6ed2dca794f5");

            migrationBuilder.InsertData(
                table: "ApplicationTransactionCategories",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[,]
                {
                    { "06a5fda9-f335-4c45-b9be-e3a1490aeb6d", "Debt", null },
                    { "09b3e5e5-9109-464b-99ab-1d831dd94da7", "Clothing", null },
                    { "0d0efd48-8469-478d-8b16-735722f86248", "Retirement", null },
                    { "14b887d2-bfba-4715-9216-d05bfff839ab", "Entertainment", null },
                    { "17f1457a-0e65-43ce-b079-3b91ef9ec449", "Food", null },
                    { "20330688-f1b8-42f2-a1ca-8e2a136ca659", "Other", null },
                    { "27868c8b-c7d3-4f55-93dd-35649b1635ea", "Kids", null },
                    { "2a2a333e-16ab-4c57-8c02-3170c639ab00", "Payroll", null },
                    { "31758331-212f-4b02-b25f-cfb215293d5c", "Pets", null },
                    { "4b36d905-0cd9-439b-ac6d-a3006779d023", "Household Supplies", null },
                    { "62f06d0d-8009-4952-87f0-6005b069f5d6", "Personal", null },
                    { "67cd6d07-8d61-4092-8b10-4dc8c2190bc9", "Education", null },
                    { "81250494-4260-4772-b299-f9721131edf2", "Technology", null },
                    { "81d136fc-abfe-4aea-98fd-2ec1cc37005c", "Transportation", null },
                    { "a16615a5-391f-4812-94df-d31319ba33f2", "Travel", null },
                    { "b127229c-85bf-484c-ad0b-0d26c87815e2", "Medical/Health Care", null },
                    { "b5992054-108c-4910-a5f1-d67c822e4073", "Uncategorized", null },
                    { "ba5ba1fc-d1ff-42d3-97fe-c7edfeee1a0e", "Insurance", null },
                    { "bb43c768-2d60-4008-a9f4-21137377033a", "Bank", null },
                    { "ce8330f8-7efa-465a-96a9-cad9b0b57bb7", "Savings", null },
                    { "d0e13c6a-757b-4a45-b8f1-c64b4ae27385", "Housing", null },
                    { "f4c03752-8b92-4b96-bc28-96339be8084b", "Utilities", null },
                    { "f9f7f1de-1ad6-4bce-8a53-b27a1ef403f6", "Gift/Donations", null }
                });
        }
    }
}
