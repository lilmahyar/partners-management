using Microsoft.EntityFrameworkCore.Migrations;

namespace PartnersManagement.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Partner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubmittedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExposureId = table.Column<long>(type: "bigint", nullable: true),
                    UDAC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelatedOrder = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductType = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdWordCampaign_CampaignName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdWordCampaign_CampaignAddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdWordCampaign_CampaignPostCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdWordCampaign_CampaignRadius = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdWordCampaign_LeadPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdWordCampaign_SMSPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdWordCampaign_UniqueSellingPoint1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdWordCampaign_UniqueSellingPoint2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdWordCampaign_UniqueSellingPoint3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdWordCampaign_Offer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdWordCampaign_DestinationURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteDetails_TemplateId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteDetails_WebsiteBusinessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteDetails_WebsiteAddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteDetails_WebsiteAddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteDetails_WebsiteCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteDetails_WebsiteState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteDetails_WebsitePostCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteDetails_WebsitePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteDetails_WebsiteEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteDetails_WebsiteMobile = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "dbo",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "dbo");
        }
    }
}
