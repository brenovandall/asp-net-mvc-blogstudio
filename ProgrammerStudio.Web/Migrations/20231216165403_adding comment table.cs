using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgrammerStudio.Web.Migrations
{
    /// <inheritdoc />
    public partial class addingcommenttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Posts_BlogPostId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_BlogPostId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "BlogPostId",
                table: "Likes");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BlogPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BlogPostId",
                table: "Comments",
                column: "BlogPostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.AddColumn<Guid>(
                name: "BlogPostId",
                table: "Likes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Likes_BlogPostId",
                table: "Likes",
                column: "BlogPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Posts_BlogPostId",
                table: "Likes",
                column: "BlogPostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
