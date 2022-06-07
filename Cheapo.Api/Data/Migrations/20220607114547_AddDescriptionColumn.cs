using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cheapo.Api.Data.Migrations
{
    public partial class AddDescriptionColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ApplicationTransactions",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "ApplicationTransactionCategories",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[,]
                {
                    { "07f102ef-db45-4e2e-8248-8aea86934abf", "Other", null },
                    { "0bf6124f-2bcf-42b5-bba0-003787318c19", "Transportation", null },
                    { "169eebe8-0bcb-42dc-9f87-0835e1bdc82a", "Debt", null },
                    { "27180a35-2489-41f6-a340-a11362e3b395", "Clothing", null },
                    { "4a3a877a-7f95-4a50-b01e-a8ccbd9f255c", "Gift/Donations", null },
                    { "5f424658-4a50-4216-a281-88ad1e56a695", "Kids", null },
                    { "603b67dc-1e4f-4990-b258-5fdde79afb81", "Payroll", null },
                    { "67bec586-c941-4cce-8feb-de537ba5372d", "Entertainment", null },
                    { "68f52ed6-c730-4eb3-99e8-44da57c18f80", "Pets", null },
                    { "6caf4e2d-e1e6-46a7-9401-19aedb4bbee5", "Medical/Health Care", null },
                    { "7e926010-b1da-4e97-91b0-0b20a34b6845", "Education", null },
                    { "824be1a6-9b38-4ade-a6df-f299ec115e50", "Savings", null },
                    { "87521f67-e376-41e9-8ee9-2102509b0e9d", "Technology", null },
                    { "9429076d-f4c5-4d09-92dd-8792d19b5f9e", "Bank", null },
                    { "9a6d40d4-7a12-4862-adc5-14d31477dd42", "Retirement", null },
                    { "9f665e79-21d4-4509-8790-7ee978c490e3", "Food", null },
                    { "a2afb8ba-8719-47d2-b1fb-221fbf210bbe", "Insurance", null },
                    { "a5fdfc23-afdf-4acb-9f85-4e3789224e67", "Uncategorized", null },
                    { "c048ce05-24d4-4212-a78d-3cf5a20d86ae", "Personal", null },
                    { "c303ed94-df56-45ff-b5b8-d42b97b76c93", "Household Supplies", null },
                    { "cd5e72dd-3e3c-496e-a1f4-cb2a73326314", "Housing", null },
                    { "da9c81d8-7703-49c2-b924-b81e0fc9ff4e", "Utilities", null },
                    { "f9f57b44-e6a1-4b17-86e4-a863267d72d4", "Travel", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "07f102ef-db45-4e2e-8248-8aea86934abf");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "0bf6124f-2bcf-42b5-bba0-003787318c19");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "169eebe8-0bcb-42dc-9f87-0835e1bdc82a");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "27180a35-2489-41f6-a340-a11362e3b395");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "4a3a877a-7f95-4a50-b01e-a8ccbd9f255c");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "5f424658-4a50-4216-a281-88ad1e56a695");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "603b67dc-1e4f-4990-b258-5fdde79afb81");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "67bec586-c941-4cce-8feb-de537ba5372d");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "68f52ed6-c730-4eb3-99e8-44da57c18f80");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "6caf4e2d-e1e6-46a7-9401-19aedb4bbee5");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "7e926010-b1da-4e97-91b0-0b20a34b6845");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "824be1a6-9b38-4ade-a6df-f299ec115e50");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "87521f67-e376-41e9-8ee9-2102509b0e9d");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "9429076d-f4c5-4d09-92dd-8792d19b5f9e");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "9a6d40d4-7a12-4862-adc5-14d31477dd42");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "9f665e79-21d4-4509-8790-7ee978c490e3");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "a2afb8ba-8719-47d2-b1fb-221fbf210bbe");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "a5fdfc23-afdf-4acb-9f85-4e3789224e67");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "c048ce05-24d4-4212-a78d-3cf5a20d86ae");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "c303ed94-df56-45ff-b5b8-d42b97b76c93");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "cd5e72dd-3e3c-496e-a1f4-cb2a73326314");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "da9c81d8-7703-49c2-b924-b81e0fc9ff4e");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "f9f57b44-e6a1-4b17-86e4-a863267d72d4");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ApplicationTransactions");

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
        }
    }
}
