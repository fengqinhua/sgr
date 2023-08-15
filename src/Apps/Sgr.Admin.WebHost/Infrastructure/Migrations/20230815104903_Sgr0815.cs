using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sgr.Admin.WebHost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Sgr0815 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sgr_datacategoryitem",
                columns: table => new
                {
                    sgr_id = table.Column<long>(type: "bigint", nullable: false, comment: "主键"),
                    m_categorytypecode = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "字典分类标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_dcitemname = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "字典项名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_dcitemvalue = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "字典项值")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_remarks = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_iseditable = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否可编辑"),
                    m_ordernumber = table.Column<int>(type: "int", nullable: false, comment: "排序号"),
                    m_state = table.Column<int>(type: "int", nullable: false, comment: "组织机构状态"),
                    sgr_parentid = table.Column<long>(type: "bigint", nullable: false, comment: "上级节点Id"),
                    sgr_nodepath = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "树节点层次目录")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sgr_rowversion = table.Column<long>(type: "bigint", nullable: false, comment: "行版本"),
                    sgr_creatoruserid = table.Column<long>(type: "bigint", nullable: false, comment: "创建的用户ID"),
                    sgr_creationtime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    sgr_lastmodifieruserid = table.Column<long>(type: "bigint", nullable: false, comment: "修改的用户ID"),
                    sgr_lastmodificationtime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true, comment: "修改时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sgr_datacategoryitem", x => x.sgr_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sgr_department",
                columns: table => new
                {
                    sgr_id = table.Column<long>(type: "bigint", nullable: false, comment: "主键"),
                    m_code = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "部门编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "部门名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_ordernumber = table.Column<int>(type: "int", nullable: false, comment: "组织排序号"),
                    m_leader = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "负责人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_phone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "联系电话")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "联系邮箱")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_remarks = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_state = table.Column<int>(type: "int", nullable: false, comment: "部门状态"),
                    sgr_parentid = table.Column<long>(type: "bigint", nullable: false, comment: "上级节点Id"),
                    sgr_nodepath = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "树节点层次目录")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sgr_rowversion = table.Column<long>(type: "bigint", nullable: false, comment: "行版本"),
                    sgr_orgid = table.Column<long>(type: "bigint", nullable: false, comment: "所在组织ID"),
                    sgr_creatoruserid = table.Column<long>(type: "bigint", nullable: false, comment: "创建的用户ID"),
                    sgr_creationtime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    sgr_lastmodifieruserid = table.Column<long>(type: "bigint", nullable: false, comment: "修改的用户ID"),
                    sgr_lastmodificationtime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true, comment: "修改时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sgr_department", x => x.sgr_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sgr_duty",
                columns: table => new
                {
                    sgr_id = table.Column<long>(type: "bigint", nullable: false, comment: "主键"),
                    m_code = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "职务编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "职务名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_ordernumber = table.Column<int>(type: "int", nullable: false, comment: "排序号"),
                    m_remarks = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "职务备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_state = table.Column<int>(type: "int", nullable: false, comment: "职务状态"),
                    sgr_rowversion = table.Column<long>(type: "bigint", nullable: false, comment: "行版本"),
                    sgr_orgid = table.Column<long>(type: "bigint", nullable: false, comment: "所在组织ID"),
                    sgr_creatoruserid = table.Column<long>(type: "bigint", nullable: false, comment: "创建的用户ID"),
                    sgr_creationtime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    sgr_lastmodifieruserid = table.Column<long>(type: "bigint", nullable: false, comment: "修改的用户ID"),
                    sgr_lastmodificationtime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true, comment: "修改时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sgr_duty", x => x.sgr_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sgr_loglogin",
                columns: table => new
                {
                    sgr_id = table.Column<long>(type: "bigint", nullable: false, comment: "主键"),
                    m_loginname = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "登录账号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_username = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "用户姓名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_logintime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false, comment: "登录时间"),
                    m_ipaddress = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "登录Ip地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_location = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "登录地点")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_loginway = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "登录途径")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_loginprovider = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_providerkey = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_providerdisplayname = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_clientbrowser = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "客户端浏览器")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_clientos = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "客户端系统")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_status = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "登录状态"),
                    m_remark = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "登录描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sgr_orgid = table.Column<long>(type: "bigint", nullable: false, comment: "所在组织ID"),
                    sgr_creatoruserid = table.Column<long>(type: "bigint", nullable: false, comment: "创建的用户ID"),
                    sgr_creationtime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false, comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sgr_loglogin", x => x.sgr_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sgr_logoperate",
                columns: table => new
                {
                    sgr_id = table.Column<long>(type: "bigint", nullable: false, comment: "主键"),
                    m_loginname = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "登录账号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_username = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "用户姓名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_ipaddress = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "登录Ip地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_location = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "登录地点")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_clientbrowser = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "客户端浏览器")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_clientos = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "客户端系统")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_loginway = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "登录途径")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_requestdescription = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "请求说明")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_requesturl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, comment: "请求地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_httpmethod = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, comment: "请求方法")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_requestparam = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false, comment: "请求参数")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_requesttime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false, comment: "请求时间"),
                    m_requestduration = table.Column<int>(type: "int", nullable: true, comment: "请求耗时"),
                    m_status = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "请求结果"),
                    m_remark = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "请求结果描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sgr_orgid = table.Column<long>(type: "bigint", nullable: false, comment: "所在组织ID"),
                    sgr_creatoruserid = table.Column<long>(type: "bigint", nullable: false, comment: "创建的用户ID"),
                    sgr_creationtime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false, comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sgr_logoperate", x => x.sgr_id);
                })
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
                    m_staffsizecode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "人员规模编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_ordernumber = table.Column<int>(type: "int", nullable: false, comment: "组织机构排序号"),
                    m_logourl = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "Logo地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_remarks = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_leader = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "机构负责人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_phone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "联系电话")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "联系邮箱")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_address = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "所在地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_businesslicensepath = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "营业执照路径")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_isconfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否已完成认证"),
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
                    sgr_lastmodificationtime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true, comment: "修改时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sgr_organization", x => x.sgr_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sgr_role",
                columns: table => new
                {
                    sgr_id = table.Column<long>(type: "bigint", nullable: false, comment: "主键"),
                    m_code = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "角色编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_rolename = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "角色名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_ordernumber = table.Column<int>(type: "int", nullable: false, comment: "排序号"),
                    m_remarks = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_state = table.Column<int>(type: "int", nullable: false, comment: "状态"),
                    sgr_rowversion = table.Column<long>(type: "bigint", nullable: false, comment: "行版本"),
                    sgr_orgid = table.Column<long>(type: "bigint", nullable: false, comment: "所在组织ID"),
                    sgr_creatoruserid = table.Column<long>(type: "bigint", nullable: false, comment: "创建的用户ID"),
                    sgr_creationtime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    sgr_lastmodifieruserid = table.Column<long>(type: "bigint", nullable: false, comment: "修改的用户ID"),
                    sgr_lastmodificationtime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true, comment: "修改时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sgr_role", x => x.sgr_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sgr_user",
                columns: table => new
                {
                    sgr_id = table.Column<long>(type: "bigint", nullable: false, comment: "主键"),
                    m_loginname = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "登录名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_loginpassword = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "登录密码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_firstlogintime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true, comment: "首次登录时间"),
                    m_lastlogintime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true, comment: "最近一次登录时间"),
                    m_loginsuccesscount = table.Column<int>(type: "int", nullable: false, comment: "登录成功次数"),
                    m_loginfailcount = table.Column<int>(type: "int", nullable: false, comment: "登录失败次数"),
                    m_username = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "用户姓名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_userphone = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true, comment: "用户绑定的手机号码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_useremail = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "用户邮箱地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_qq = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "用户QQ号码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_wechat = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "用户微信号码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_issuperadmin = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    m_state = table.Column<int>(type: "int", nullable: false, comment: "状态"),
                    m_departmentid = table.Column<long>(type: "bigint", nullable: true, comment: "所属部门Id"),
                    m_departmentname = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "所属部门名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sgr_rowversion = table.Column<long>(type: "bigint", nullable: false, comment: "行版本"),
                    sgr_orgid = table.Column<long>(type: "bigint", nullable: false, comment: "所在组织ID"),
                    sgr_creatoruserid = table.Column<long>(type: "bigint", nullable: false, comment: "创建的用户ID"),
                    sgr_creationtime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    sgr_lastmodifieruserid = table.Column<long>(type: "bigint", nullable: false, comment: "修改的用户ID"),
                    sgr_lastmodificationtime = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true, comment: "修改时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sgr_user", x => x.sgr_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sgr_role_resource",
                columns: table => new
                {
                    sgr_id = table.Column<string>(type: "varchar(255)", nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_resourcecode = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "资源标识")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_resourcetype = table.Column<int>(type: "int", nullable: false, comment: "资源类型"),
                    m_roleid = table.Column<long>(type: "bigint", nullable: false, comment: "角色标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sgr_role_resource", x => x.sgr_id);
                    table.ForeignKey(
                        name: "FK_sgr_role_resource_sgr_role_m_roleid",
                        column: x => x.m_roleid,
                        principalTable: "sgr_role",
                        principalColumn: "sgr_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sgr_user_duty",
                columns: table => new
                {
                    sgr_id = table.Column<string>(type: "varchar(255)", nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_dutyid = table.Column<long>(type: "bigint", nullable: false, comment: "岗位标识"),
                    m_userid = table.Column<long>(type: "bigint", nullable: false, comment: "用户标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sgr_user_duty", x => x.sgr_id);
                    table.ForeignKey(
                        name: "FK_sgr_user_duty_sgr_user_m_userid",
                        column: x => x.m_userid,
                        principalTable: "sgr_user",
                        principalColumn: "sgr_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sgr_user_role",
                columns: table => new
                {
                    sgr_id = table.Column<string>(type: "varchar(255)", nullable: false, comment: "主键")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    m_roleid = table.Column<long>(type: "bigint", nullable: false, comment: "角色标识"),
                    m_userid = table.Column<long>(type: "bigint", nullable: false, comment: "用户标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sgr_user_role", x => x.sgr_id);
                    table.ForeignKey(
                        name: "FK_sgr_user_role_sgr_user_m_userid",
                        column: x => x.m_userid,
                        principalTable: "sgr_user",
                        principalColumn: "sgr_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_sgr_datacategoryitem_m_categorytypecode",
                table: "sgr_datacategoryitem",
                column: "m_categorytypecode");

            migrationBuilder.CreateIndex(
                name: "IX_sgr_datacategoryitem_sgr_nodepath",
                table: "sgr_datacategoryitem",
                column: "sgr_nodepath");

            migrationBuilder.CreateIndex(
                name: "IX_sgr_datacategoryitem_sgr_parentid",
                table: "sgr_datacategoryitem",
                column: "sgr_parentid");

            migrationBuilder.CreateIndex(
                name: "IX_sgr_department_sgr_nodepath",
                table: "sgr_department",
                column: "sgr_nodepath");

            migrationBuilder.CreateIndex(
                name: "IX_sgr_department_sgr_orgid",
                table: "sgr_department",
                column: "sgr_orgid");

            migrationBuilder.CreateIndex(
                name: "IX_sgr_department_sgr_parentid",
                table: "sgr_department",
                column: "sgr_parentid");

            migrationBuilder.CreateIndex(
                name: "IX_sgr_duty_sgr_orgid",
                table: "sgr_duty",
                column: "sgr_orgid");

            migrationBuilder.CreateIndex(
                name: "IX_sgr_loglogin_sgr_orgid",
                table: "sgr_loglogin",
                column: "sgr_orgid");

            migrationBuilder.CreateIndex(
                name: "IX_sgr_logoperate_sgr_orgid",
                table: "sgr_logoperate",
                column: "sgr_orgid");

            migrationBuilder.CreateIndex(
                name: "IX_sgr_organization_m_code",
                table: "sgr_organization",
                column: "m_code");

            migrationBuilder.CreateIndex(
                name: "IX_sgr_organization_sgr_nodepath",
                table: "sgr_organization",
                column: "sgr_nodepath");

            migrationBuilder.CreateIndex(
                name: "IX_sgr_organization_sgr_parentid",
                table: "sgr_organization",
                column: "sgr_parentid");

            migrationBuilder.CreateIndex(
                name: "IX_sgr_role_sgr_orgid",
                table: "sgr_role",
                column: "sgr_orgid");

            migrationBuilder.CreateIndex(
                name: "IX_sgr_role_resource_m_roleid",
                table: "sgr_role_resource",
                column: "m_roleid");

            migrationBuilder.CreateIndex(
                name: "IX_sgr_user_m_departmentid",
                table: "sgr_user",
                column: "m_departmentid");

            migrationBuilder.CreateIndex(
                name: "IX_sgr_user_m_loginname",
                table: "sgr_user",
                column: "m_loginname");

            migrationBuilder.CreateIndex(
                name: "IX_sgr_user_sgr_orgid",
                table: "sgr_user",
                column: "sgr_orgid");

            migrationBuilder.CreateIndex(
                name: "IX_sgr_user_duty_m_userid",
                table: "sgr_user_duty",
                column: "m_userid");

            migrationBuilder.CreateIndex(
                name: "IX_sgr_user_role_m_userid",
                table: "sgr_user_role",
                column: "m_userid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sgr_datacategoryitem");

            migrationBuilder.DropTable(
                name: "sgr_department");

            migrationBuilder.DropTable(
                name: "sgr_duty");

            migrationBuilder.DropTable(
                name: "sgr_loglogin");

            migrationBuilder.DropTable(
                name: "sgr_logoperate");

            migrationBuilder.DropTable(
                name: "sgr_organization");

            migrationBuilder.DropTable(
                name: "sgr_role_resource");

            migrationBuilder.DropTable(
                name: "sgr_user_duty");

            migrationBuilder.DropTable(
                name: "sgr_user_role");

            migrationBuilder.DropTable(
                name: "sgr_role");

            migrationBuilder.DropTable(
                name: "sgr_user");
        }
    }
}
