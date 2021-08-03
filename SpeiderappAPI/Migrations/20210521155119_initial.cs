using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SpeiderappAPI.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requirement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    PublishTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    Resources = table.Column<string[]>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requirement_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequirementPrerequisite",
                columns: table => new
                {
                    RequirerId = table.Column<long>(type: "bigint", nullable: false),
                    RequireeId = table.Column<long>(type: "bigint", nullable: false),
                    IsAdvisory = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequirementPrerequisite", x => new { x.RequirerId, x.RequireeId });
                    table.ForeignKey(
                        name: "FK_RequirementPrerequisite_Requirement_RequireeId",
                        column: x => x.RequireeId,
                        principalTable: "Requirement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequirementPrerequisite_Requirement_RequirerId",
                        column: x => x.RequirerId,
                        principalTable: "Requirement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaggedWiths",
                columns: table => new
                {
                    BadgeId = table.Column<long>(type: "bigint", nullable: false),
                    TagId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaggedWiths", x => new { x.BadgeId, x.TagId });
                    table.ForeignKey(
                        name: "FK_TaggedWiths_Requirement_BadgeId",
                        column: x => x.BadgeId,
                        principalTable: "Requirement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaggedWiths_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { -1L, "ocrispe0@jugem.jp", "Ozzie", "Crispe" },
                    { -2L, "adandye@hexun.com", "Aggi", "Dandy" },
                    { -3L, "gspottiswood2@psu.com", "Gerry", "Spottiswood" }
                });

            migrationBuilder.InsertData(
                table: "Requirement",
                columns: new[] { "Id", "AuthorId", "Description", "Discriminator", "Image", "PublishTime", "Resources", "Title" },
                values: new object[] { -1L, -1L, "This is a cool badge for chucking wood.", "Badge", "3aas!2d=", new DateTime(2021, 5, 21, 17, 51, 18, 865, DateTimeKind.Local).AddTicks(8195), new string[0], "Woodchuck" });

            migrationBuilder.InsertData(
                table: "Requirement",
                columns: new[] { "Id", "AuthorId", "Description", "Discriminator", "PublishTime" },
                values: new object[] { -2L, -1L, "Actually chop wood", "Requirement", new DateTime(2021, 5, 21, 17, 51, 18, 866, DateTimeKind.Local).AddTicks(134) });

            migrationBuilder.InsertData(
                table: "RequirementPrerequisite",
                columns: new[] { "RequireeId", "RequirerId", "IsAdvisory" },
                values: new object[] { -2L, -1L, false });

            migrationBuilder.CreateIndex(
                name: "IX_Requirement_AuthorId",
                table: "Requirement",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_RequirementPrerequisite_RequireeId",
                table: "RequirementPrerequisite",
                column: "RequireeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaggedWiths_TagId",
                table: "TaggedWiths",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CategoryId",
                table: "Tags",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequirementPrerequisite");

            migrationBuilder.DropTable(
                name: "TaggedWiths");

            migrationBuilder.DropTable(
                name: "Requirement");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
