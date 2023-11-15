using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgrammerStudio.Web.Migrations
{
    /// <inheritdoc />
    public partial class postsandtagtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Heading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlHandle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogPostBlogTag",
                columns: table => new
                {
                    BlogPostsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogTagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostBlogTag", x => new { x.BlogPostsId, x.BlogTagsId });
                    table.ForeignKey(
                        name: "FK_BlogPostBlogTag_Posts_BlogPostsId",
                        column: x => x.BlogPostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPostBlogTag_Tags_BlogTagsId",
                        column: x => x.BlogTagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostBlogTag_BlogTagsId",
                table: "BlogPostBlogTag",
                column: "BlogTagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPostBlogTag");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
