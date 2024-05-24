using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Auctions_Web_API.Migrations
{
    /// <inheritdoc />
    public partial class Init_database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PermissionEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionEntity", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoleEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleEntities", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AboutMe = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SurName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Age = table.Column<int>(type: "int", nullable: true),
                    AboutCompany = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntities", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RolePermissionsEntity",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissionsEntity", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissionsEntity_PermissionEntity_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "PermissionEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissionsEntity_RoleEntities_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AuctionEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TitleName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Finished = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RatingId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuctionEntities_UserEntities_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "UserEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserRoleEntity",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleEntity", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserRoleEntity_RoleEntities_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleEntity_UserEntities_UserId",
                        column: x => x.UserId,
                        principalTable: "UserEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RatingEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AuctionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Point = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingEntities_AuctionEntities_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "AuctionEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RatingEntities_UserEntities_StudentId",
                        column: x => x.StudentId,
                        principalTable: "UserEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TaskEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AuctionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskEntities_AuctionEntities_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "AuctionEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskEntities_UserEntities_StudentId",
                        column: x => x.StudentId,
                        principalTable: "UserEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "PermissionEntity",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Create" },
                    { 2, "Read" },
                    { 3, "Update" },
                    { 4, "Delete" }
                });

            migrationBuilder.InsertData(
                table: "RoleEntities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissionsEntity",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuctionEntities_CreatorId",
                table: "AuctionEntities",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingEntities_AuctionId",
                table: "RatingEntities",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingEntities_StudentId",
                table: "RatingEntities",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionsEntity_PermissionId",
                table: "RolePermissionsEntity",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEntities_AuctionId",
                table: "TaskEntities",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEntities_StudentId",
                table: "TaskEntities",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleEntity_UserId",
                table: "UserRoleEntity",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RatingEntities");

            migrationBuilder.DropTable(
                name: "RolePermissionsEntity");

            migrationBuilder.DropTable(
                name: "TaskEntities");

            migrationBuilder.DropTable(
                name: "UserRoleEntity");

            migrationBuilder.DropTable(
                name: "PermissionEntity");

            migrationBuilder.DropTable(
                name: "AuctionEntities");

            migrationBuilder.DropTable(
                name: "RoleEntities");

            migrationBuilder.DropTable(
                name: "UserEntities");
        }
    }
}
