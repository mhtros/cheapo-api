using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cheapo.Api.Data.Migrations
{
    public partial class TransactionCategoriesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationTransactionCategories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationTransactionCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationTransactionCategories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationTransactionCategories_Name",
                table: "ApplicationTransactionCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationTransactionCategories_UserId",
                table: "ApplicationTransactionCategories",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationTransactionCategories");
        }
    }
}
