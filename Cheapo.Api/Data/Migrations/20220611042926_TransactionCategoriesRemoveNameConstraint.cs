using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cheapo.Api.Data.Migrations
{
    public partial class TransactionCategoriesRemoveNameConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApplicationTransactionCategories_Name",
                table: "ApplicationTransactionCategories");

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

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ApplicationTransactions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "ApplicationTransactionCategories",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[,]
                {
                    { "1279b28d-e628-4895-9e21-33ab5e24c929", "Clothing", null },
                    { "151a9515-2692-461e-b9eb-444c3ac5b0d3", "Savings", null },
                    { "24d60790-56c0-446a-a43e-d757efaea7a2", "Other", null },
                    { "281a8f8c-1b07-4054-a88c-48b75a95ef81", "Medical/Health Care", null },
                    { "2a0e3a16-148c-451c-b1d0-9eff8f5e2f2f", "Entertainment", null },
                    { "2fa661e8-5455-4be9-aeea-8b671d833535", "Insurance", null },
                    { "353ac71c-14f5-4b8f-9a4f-08edaeb1af31", "Gift/Donations", null },
                    { "3cb40f08-1b4e-426d-a731-541cad1b8bef", "Bank", null },
                    { "51509e9a-4e33-4030-bbd3-6eb28ba78bb0", "Payroll", null },
                    { "5284c1c0-9a45-48e0-b2ed-1606d866d2ab", "Technology", null },
                    { "5b36d424-8159-4ff6-bca9-98dfebd7fb75", "Travel", null },
                    { "9904bee8-0bd9-44a8-a524-c37022349263", "Uncategorized", null },
                    { "b69e12a0-961c-4c09-9ea9-1efef0b367c1", "Housing", null },
                    { "c699a18c-cad8-4b5c-93e3-6112c21e8a73", "Utilities", null },
                    { "cad546bd-3656-4514-9c44-682d689eb55e", "Debt", null },
                    { "d23224ea-2405-4ccc-b990-f1aeedc4a334", "Kids", null },
                    { "d301accd-d2c5-485a-8f17-88503a7dc784", "Education", null },
                    { "d3351474-f844-44c6-877d-3701cdeeb916", "Personal", null },
                    { "d68afc8c-7503-420f-b8e7-805b79692261", "Food", null },
                    { "eeaff367-a232-4a07-b257-ac0cd0d932f2", "Retirement", null },
                    { "f40092e6-f134-43e5-a47a-b9cc67eda1cd", "Pets", null },
                    { "f7d94554-cdda-436d-8b19-8d5d3cdb957d", "Transportation", null },
                    { "fc0c3278-530c-4937-9e3e-99d6631a6844", "Household Supplies", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "1279b28d-e628-4895-9e21-33ab5e24c929");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "151a9515-2692-461e-b9eb-444c3ac5b0d3");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "24d60790-56c0-446a-a43e-d757efaea7a2");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "281a8f8c-1b07-4054-a88c-48b75a95ef81");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "2a0e3a16-148c-451c-b1d0-9eff8f5e2f2f");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "2fa661e8-5455-4be9-aeea-8b671d833535");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "353ac71c-14f5-4b8f-9a4f-08edaeb1af31");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "3cb40f08-1b4e-426d-a731-541cad1b8bef");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "51509e9a-4e33-4030-bbd3-6eb28ba78bb0");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "5284c1c0-9a45-48e0-b2ed-1606d866d2ab");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "5b36d424-8159-4ff6-bca9-98dfebd7fb75");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "9904bee8-0bd9-44a8-a524-c37022349263");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "b69e12a0-961c-4c09-9ea9-1efef0b367c1");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "c699a18c-cad8-4b5c-93e3-6112c21e8a73");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "cad546bd-3656-4514-9c44-682d689eb55e");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "d23224ea-2405-4ccc-b990-f1aeedc4a334");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "d301accd-d2c5-485a-8f17-88503a7dc784");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "d3351474-f844-44c6-877d-3701cdeeb916");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "d68afc8c-7503-420f-b8e7-805b79692261");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "eeaff367-a232-4a07-b257-ac0cd0d932f2");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "f40092e6-f134-43e5-a47a-b9cc67eda1cd");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "f7d94554-cdda-436d-8b19-8d5d3cdb957d");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "fc0c3278-530c-4937-9e3e-99d6631a6844");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ApplicationTransactions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

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

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationTransactionCategories_Name",
                table: "ApplicationTransactionCategories",
                column: "Name",
                unique: true);
        }
    }
}
