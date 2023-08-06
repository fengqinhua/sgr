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
                name: "sgr_organization",
                columns: table => new
                {
                    sgr_id = table.Column<long>(type: "bigint", nullable: false, comment: "主键"),
                    m_code = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "组织机构编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "组织机构名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_orgtypecode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "组织机构类型编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_areacode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "所属行政区划编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_ordernumber = table.Column<int>(type: "int", nullable: false, comment: "组织机构排序号"),
                    m_leader = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "机构负责人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_phone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "联系电话")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "联系邮箱")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_address = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "所在地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_logourl = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "Logo地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_state = table.Column<int>(type: "int", nullable: false, comment: "组织机构状态"),
                    sgr_parentid = table.Column<long>(type: "bigint", nullable: false, comment: "上级节点Id"),
                    sgr_nodepath = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "树节点层次目录")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sgr_rowversion = table.Column<long>(type: "bigint", nullable: false, comment: "行版本"),
                    sgr_extensiondata = table.Column<string>(type: "varchar(2047)", maxLength: 2047, nullable: true, comment: "扩展对象/实体")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sgr_creatoruserid = table.Column<long>(type: "bigint", nullable: false, comment: "创建的用户ID"),
                    sgr_creationtime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    sgr_lastmodifieruserid = table.Column<long>(type: "bigint", nullable: false, comment: "修改的用户ID"),
                    sgr_lastmodificationtime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true, comment: "修改时间"),
                    sgr_deleteruserid = table.Column<long>(type: "bigint", nullable: false, comment: "删除的用户ID"),
                    sgr_deletiontime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true, comment: "删除时间"),
                    sgr_isdeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否已经被软删除")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sgr_organization", x => x.sgr_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sgr_organization");
        }
    }
}
