using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mini_LMS.Migrations
{
    /// <inheritdoc />
    public partial class New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LearnerId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    ProgressPercentage = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseProgresses_courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseProgresses_users_LearnerId",
                        column: x => x.LearnerId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateIndex(
                name: "IX_CourseProgresses_CourseId",
                table: "CourseProgresses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseProgresses_LearnerId",
                table: "CourseProgresses",
                column: "LearnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseProgresses");
        }
    }
}
