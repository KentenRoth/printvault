using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PrintVault.Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrintModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<string>(type: "text", nullable: false),
                    ProfileTitle = table.Column<string>(type: "text", nullable: false),
                    ProfileDescription = table.Column<string>(type: "text", nullable: false),
                    TotalFilamentUsedG = table.Column<double>(type: "double precision", nullable: false),
                    TotalPrintTimeSeconds = table.Column<int>(type: "integer", nullable: false),
                    NumberOfPlates = table.Column<int>(type: "integer", nullable: false),
                    IsFavorite = table.Column<bool>(type: "boolean", nullable: false),
                    ImportedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrintModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrintModels_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ModelImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    ImageType = table.Column<string>(type: "text", nullable: false),
                    PrintModelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModelImages_PrintModels_PrintModelId",
                        column: x => x.PrintModelId,
                        principalTable: "PrintModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModelTags",
                columns: table => new
                {
                    PrintModelId = table.Column<int>(type: "integer", nullable: false),
                    TagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelTags", x => new { x.PrintModelId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ModelTags_PrintModels_PrintModelId",
                        column: x => x.PrintModelId,
                        principalTable: "PrintModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModelTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlateNumber = table.Column<int>(type: "integer", nullable: false),
                    FilamentUsedG = table.Column<double>(type: "double precision", nullable: false),
                    PrintTimeSeconds = table.Column<int>(type: "integer", nullable: false),
                    ThumbnailSmallPath = table.Column<string>(type: "text", nullable: false),
                    ThumbnailMediumPath = table.Column<string>(type: "text", nullable: false),
                    ThumbnailLargePath = table.Column<string>(type: "text", nullable: false),
                    PrintModelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plates_PrintModels_PrintModelId",
                        column: x => x.PrintModelId,
                        principalTable: "PrintModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModelImages_PrintModelId",
                table: "ModelImages",
                column: "PrintModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelTags_TagId",
                table: "ModelTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Plates_PrintModelId",
                table: "Plates",
                column: "PrintModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PrintModels_CategoryId",
                table: "PrintModels",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModelImages");

            migrationBuilder.DropTable(
                name: "ModelTags");

            migrationBuilder.DropTable(
                name: "Plates");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "PrintModels");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
