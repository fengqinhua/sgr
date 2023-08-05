using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sgr.Database.Tool.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "organization",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "主键"),
                    Code = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "组织机构编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "组织机构名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrgTypeCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "组织机构类型编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AreaCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "所属行政区划编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderNumber = table.Column<int>(type: "int", nullable: false, comment: "组织机构排序号"),
                    Leader = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "机构负责人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "联系电话")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "联系邮箱")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "所在地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LogoUrl = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "Logo地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    State = table.Column<int>(type: "int", nullable: false, comment: "组织机构状态"),
                    ParentId = table.Column<long>(type: "bigint", nullable: false, comment: "上级节点Id"),
                    NodePath = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "树节点层次目录")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RowVersion = table.Column<long>(type: "bigint", nullable: false, comment: "行版本"),
                    ExtensionData = table.Column<string>(type: "varchar(2047)", maxLength: 2047, nullable: true, comment: "扩展对象/实体")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: false),
                    LastModificationTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: false),
                    DeletionTime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否已经被软删除")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "organization");
        }
    }
}
