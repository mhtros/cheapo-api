using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cheapo.Api.Data.Migrations
{
    public partial class TransactionAddTransactionDateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateOnly>(
                name: "TransactionDate",
                table: "ApplicationTransactions",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.InsertData(
                table: "ApplicationTransactionCategories",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[,]
                {
                    { "000fdde1-1a6e-4179-add7-a8009419bc24", "Savings", null },
                    { "01b02939-6d26-478d-8f6a-b75bde269e3c", "Travel", null },
                    { "12cce3da-c984-4fe0-af52-0e2844f444b1", "Debt", null },
                    { "29f8bddc-c8b8-4fc1-bc25-1c4a70669a3f", "Household Supplies", null },
                    { "30981d26-1607-45b0-b76d-946c88faf600", "Payroll", null },
                    { "3a45d2ab-5cc0-4092-8ba1-2864b3ebaf6a", "Food", null },
                    { "5b9975e3-345b-4729-82b6-fdc2b84cc028", "Uncategorized", null },
                    { "67d37e84-6818-4b0e-8872-078f0d535295", "Other", null },
                    { "72b85f9b-c426-4deb-b0a6-2a5e2c688f22", "Entertainment", null },
                    { "738b9999-6c63-4766-8565-14ffb7f6f30e", "Education", null },
                    { "76ba3529-d7cf-4fde-a7e3-756ba3cc7f3c", "Medical/Health Care", null },
                    { "7867c6e5-ce52-4368-97a6-e4b6c778a4a0", "Pets", null },
                    { "790d6e9e-a376-45ff-85ae-2452ad1c3ada", "Transportation", null },
                    { "8816e5fa-2806-4701-835c-722c2e3f5294", "Technology", null },
                    { "8d9d2e1d-440d-4434-addc-980d6fca7fe1", "Housing", null },
                    { "8ff9c99d-0b69-417f-8aa9-d04d96141515", "Insurance", null },
                    { "93738cce-16b9-4821-bb76-92a04d611945", "Clothing", null },
                    { "9b8252f1-62f4-4c8a-8600-7e5fad11cec3", "Retirement", null },
                    { "a29dde8a-7bf7-4017-a96e-1efbcb481038", "Kids", null },
                    { "bbae6634-6cde-41ca-92ab-7644bbd1f63d", "Bank", null },
                    { "c849dde7-21e3-4a2e-b9c6-3806e6a501cb", "Gift/Donations", null },
                    { "eff99fcd-08a8-45f7-af47-2cb19a61f2cf", "Personal", null },
                    { "feeb31c7-9eca-4a27-b7fa-7702202fbe81", "Utilities", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "000fdde1-1a6e-4179-add7-a8009419bc24");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "01b02939-6d26-478d-8f6a-b75bde269e3c");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "12cce3da-c984-4fe0-af52-0e2844f444b1");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "29f8bddc-c8b8-4fc1-bc25-1c4a70669a3f");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "30981d26-1607-45b0-b76d-946c88faf600");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "3a45d2ab-5cc0-4092-8ba1-2864b3ebaf6a");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "5b9975e3-345b-4729-82b6-fdc2b84cc028");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "67d37e84-6818-4b0e-8872-078f0d535295");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "72b85f9b-c426-4deb-b0a6-2a5e2c688f22");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "738b9999-6c63-4766-8565-14ffb7f6f30e");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "76ba3529-d7cf-4fde-a7e3-756ba3cc7f3c");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "7867c6e5-ce52-4368-97a6-e4b6c778a4a0");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "790d6e9e-a376-45ff-85ae-2452ad1c3ada");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "8816e5fa-2806-4701-835c-722c2e3f5294");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "8d9d2e1d-440d-4434-addc-980d6fca7fe1");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "8ff9c99d-0b69-417f-8aa9-d04d96141515");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "93738cce-16b9-4821-bb76-92a04d611945");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "9b8252f1-62f4-4c8a-8600-7e5fad11cec3");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "a29dde8a-7bf7-4017-a96e-1efbcb481038");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "bbae6634-6cde-41ca-92ab-7644bbd1f63d");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "c849dde7-21e3-4a2e-b9c6-3806e6a501cb");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "eff99fcd-08a8-45f7-af47-2cb19a61f2cf");

            migrationBuilder.DeleteData(
                table: "ApplicationTransactionCategories",
                keyColumn: "Id",
                keyValue: "feeb31c7-9eca-4a27-b7fa-7702202fbe81");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "ApplicationTransactions");

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
    }
}
