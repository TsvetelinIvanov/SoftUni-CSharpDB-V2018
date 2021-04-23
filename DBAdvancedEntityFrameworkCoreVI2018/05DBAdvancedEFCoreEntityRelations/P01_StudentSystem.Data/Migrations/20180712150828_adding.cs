using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace P01_StudentSystem.Data.Migrations
{
    public partial class adding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "CHAR(10)", unicode: false, nullable: true),
                    RegisteredOn = table.Column<DateTime>(nullable: false),
                    Birthday = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    ResourceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Url = table.Column<string>(unicode: false, nullable: false),
                    ResourceType = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.ResourceId);
                    table.ForeignKey(
                        name: "FK_Resources_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomeworkSubmissions",
                columns: table => new
                {
                    HomeworkId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(unicode: false, nullable: false),
                    ContentType = table.Column<int>(nullable: false),
                    SubmissionTime = table.Column<DateTime>(nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeworkSubmissions", x => x.HomeworkId);
                    table.ForeignKey(
                        name: "FK_HomeworkSubmissions_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeworkSubmissions_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourses",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourses", x => new { x.StudentId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_StudentCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourses_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "Description", "EndDate", "Name", "Price", "StartDate" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2018, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Statistics", 390m, new DateTime(2018, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, null, new DateTime(2018, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marketing", 390m, new DateTime(2018, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, null, new DateTime(2018, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Economics", 390m, new DateTime(2018, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, null, new DateTime(2018, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Management", 390m, new DateTime(2018, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "Birthday", "Name", "PhoneNumber", "RegisteredOn" },
                values: new object[,]
                {
                    { 1, null, "Gun Xo", null, new DateTime(2018, 5, 1, 8, 30, 52, 0, DateTimeKind.Unspecified) },
                    { 2, null, "Run Jo", null, new DateTime(2018, 5, 1, 8, 30, 53, 0, DateTimeKind.Unspecified) },
                    { 3, null, "Vu Min", null, new DateTime(2018, 5, 1, 8, 30, 54, 0, DateTimeKind.Unspecified) },
                    { 4, null, "Ran Pi", null, new DateTime(2018, 5, 1, 8, 30, 55, 0, DateTimeKind.Unspecified) },
                    { 5, null, "Tan Su", null, new DateTime(2018, 5, 1, 8, 30, 56, 0, DateTimeKind.Unspecified) },
                    { 6, null, "Mo Joy", null, new DateTime(2018, 5, 1, 8, 30, 57, 0, DateTimeKind.Unspecified) },
                    { 7, null, "Min Xo", null, new DateTime(2018, 5, 1, 8, 30, 58, 0, DateTimeKind.Unspecified) },
                    { 8, null, "Tin Pu", null, new DateTime(2018, 5, 1, 8, 30, 59, 0, DateTimeKind.Unspecified) },
                    { 9, null, "Han Zin", null, new DateTime(2018, 5, 1, 8, 31, 1, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "HomeworkSubmissions",
                columns: new[] { "HomeworkId", "Content", "ContentType", "CourseId", "StudentId", "SubmissionTime" },
                values: new object[,]
                {
                    { 1, "01Introduction", 2, 1, 1, new DateTime(2018, 6, 1, 11, 30, 58, 0, DateTimeKind.Unspecified) },
                    { 2, "01Introduction", 2, 1, 2, new DateTime(2018, 6, 1, 11, 31, 58, 0, DateTimeKind.Unspecified) },
                    { 3, "01Introduction", 2, 1, 3, new DateTime(2018, 6, 1, 11, 32, 58, 0, DateTimeKind.Unspecified) },
                    { 4, "01Introduction", 2, 1, 4, new DateTime(2018, 6, 1, 11, 33, 58, 0, DateTimeKind.Unspecified) },
                    { 5, "01Introduction", 2, 1, 5, new DateTime(2018, 6, 1, 11, 34, 58, 0, DateTimeKind.Unspecified) },
                    { 6, "01Introduction", 2, 1, 6, new DateTime(2018, 6, 1, 11, 35, 58, 0, DateTimeKind.Unspecified) },
                    { 7, "01Introduction", 2, 1, 7, new DateTime(2018, 6, 1, 11, 36, 58, 0, DateTimeKind.Unspecified) },
                    { 8, "01Introduction", 2, 1, 8, new DateTime(2018, 6, 1, 11, 37, 58, 0, DateTimeKind.Unspecified) },
                    { 9, "01Introduction", 2, 1, 9, new DateTime(2018, 6, 1, 11, 38, 58, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "ResourceId", "CourseId", "Name", "ResourceType", "Url" },
                values: new object[,]
                {
                    { 1, 1, "Introduction", 0, "Url" },
                    { 2, 2, "Introduction", 0, "Url" },
                    { 3, 3, "Introduction", 0, "Url" },
                    { 4, 4, "Introduction", 0, "Url" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkSubmissions_CourseId",
                table: "HomeworkSubmissions",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkSubmissions_StudentId",
                table: "HomeworkSubmissions",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_CourseId",
                table: "Resources",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_CourseId",
                table: "StudentCourses",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeworkSubmissions");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "StudentCourses");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}