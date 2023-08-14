CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `sgr_department` (
    `sgr_id` bigint NOT NULL AUTO_INCREMENT COMMENT '主键',
    `m_code` varchar(200) CHARACTER SET utf8mb4 NOT NULL COMMENT '部门编码',
    `m_name` varchar(200) CHARACTER SET utf8mb4 NOT NULL COMMENT '部门名称',
    `m_ordernumber` int NOT NULL COMMENT '组织排序号',
    `m_leader` varchar(50) CHARACTER SET utf8mb4 NULL COMMENT '负责人',
    `m_phone` varchar(50) CHARACTER SET utf8mb4 NULL COMMENT '联系电话',
    `m_email` varchar(50) CHARACTER SET utf8mb4 NULL COMMENT '联系邮箱',
    `m_remarks` varchar(500) CHARACTER SET utf8mb4 NULL COMMENT '描述',
    `m_state` int NOT NULL COMMENT '部门状态',
    `sgr_parentid` bigint NOT NULL COMMENT '上级节点Id',
    `sgr_nodepath` varchar(255) CHARACTER SET utf8mb4 NOT NULL COMMENT '树节点层次目录',
    `sgr_rowversion` bigint NOT NULL COMMENT '行版本',
    `sgr_orgid` bigint NOT NULL COMMENT '所在组织ID',
    `sgr_creatoruserid` bigint NOT NULL COMMENT '创建的用户ID',
    `sgr_creationtime` datetime(6) NOT NULL COMMENT '创建时间',
    `sgr_lastmodifieruserid` bigint NOT NULL COMMENT '修改的用户ID',
    `sgr_lastmodificationtime` datetime(6) NULL COMMENT '修改时间',
    CONSTRAINT `PK_sgr_department` PRIMARY KEY (`sgr_id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `sgr_duty` (
    `sgr_id` bigint NOT NULL AUTO_INCREMENT COMMENT '主键',
    `m_code` varchar(200) CHARACTER SET utf8mb4 NOT NULL COMMENT '职务编码',
    `m_name` varchar(200) CHARACTER SET utf8mb4 NOT NULL COMMENT '职务名称',
    `m_ordernumber` int NOT NULL COMMENT '排序号',
    `m_remarks` varchar(500) CHARACTER SET utf8mb4 NULL COMMENT '职务备注',
    `m_state` int NOT NULL COMMENT '职务状态',
    `sgr_rowversion` bigint NOT NULL COMMENT '行版本',
    `sgr_orgid` bigint NOT NULL COMMENT '所在组织ID',
    `sgr_creatoruserid` bigint NOT NULL COMMENT '创建的用户ID',
    `sgr_creationtime` datetime(6) NOT NULL COMMENT '创建时间',
    `sgr_lastmodifieruserid` bigint NOT NULL COMMENT '修改的用户ID',
    `sgr_lastmodificationtime` datetime(6) NULL COMMENT '修改时间',
    CONSTRAINT `PK_sgr_duty` PRIMARY KEY (`sgr_id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `sgr_loglogin` (
    `sgr_id` bigint NOT NULL AUTO_INCREMENT COMMENT '主键',
    `m_loginname` varchar(200) CHARACTER SET utf8mb4 NOT NULL COMMENT '登录账号',
    `m_username` varchar(200) CHARACTER SET utf8mb4 NULL COMMENT '用户姓名',
    `m_logintime` datetime(6) NOT NULL COMMENT '登录时间',
    `m_ipaddress` varchar(50) CHARACTER SET utf8mb4 NULL COMMENT '登录Ip地址',
    `m_location` varchar(200) CHARACTER SET utf8mb4 NULL COMMENT '登录地点',
    `m_loginway` varchar(20) CHARACTER SET utf8mb4 NULL COMMENT '登录途径',
    `m_loginprovider` varchar(500) CHARACTER SET utf8mb4 NULL,
    `m_providerkey` varchar(500) CHARACTER SET utf8mb4 NULL,
    `m_providerdisplayname` varchar(500) CHARACTER SET utf8mb4 NULL,
    `m_clientbrowser` varchar(50) CHARACTER SET utf8mb4 NULL COMMENT '客户端浏览器',
    `m_clientos` varchar(50) CHARACTER SET utf8mb4 NULL COMMENT '客户端系统',
    `m_status` tinyint(1) NOT NULL COMMENT '登录状态',
    `m_remark` varchar(200) CHARACTER SET utf8mb4 NULL COMMENT '登录描述',
    `sgr_orgid` bigint NOT NULL COMMENT '所在组织ID',
    `sgr_creatoruserid` bigint NOT NULL COMMENT '创建的用户ID',
    `sgr_creationtime` datetime(6) NOT NULL COMMENT '创建时间',
    CONSTRAINT `PK_sgr_loglogin` PRIMARY KEY (`sgr_id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `sgr_logoperate` (
    `sgr_id` bigint NOT NULL AUTO_INCREMENT COMMENT '主键',
    `m_loginname` varchar(200) CHARACTER SET utf8mb4 NULL COMMENT '登录账号',
    `m_username` varchar(200) CHARACTER SET utf8mb4 NULL COMMENT '用户姓名',
    `m_ipaddress` varchar(50) CHARACTER SET utf8mb4 NULL COMMENT '登录Ip地址',
    `m_location` varchar(200) CHARACTER SET utf8mb4 NULL COMMENT '登录地点',
    `m_clientbrowser` varchar(50) CHARACTER SET utf8mb4 NULL COMMENT '客户端浏览器',
    `m_clientos` varchar(50) CHARACTER SET utf8mb4 NULL COMMENT '客户端系统',
    `m_loginway` varchar(20) CHARACTER SET utf8mb4 NULL COMMENT '登录途径',
    `m_requestdescription` varchar(200) CHARACTER SET utf8mb4 NOT NULL COMMENT '请求说明',
    `m_requesturl` varchar(500) CHARACTER SET utf8mb4 NOT NULL COMMENT '请求地址',
    `m_httpmethod` varchar(20) CHARACTER SET utf8mb4 NOT NULL COMMENT '请求方法',
    `m_requestparam` varchar(2000) CHARACTER SET utf8mb4 NOT NULL COMMENT '请求参数',
    `m_requesttime` datetime(6) NOT NULL COMMENT '请求时间',
    `m_requestduration` int NULL COMMENT '请求耗时',
    `m_status` tinyint(1) NOT NULL COMMENT '请求结果',
    `m_remark` varchar(200) CHARACTER SET utf8mb4 NULL COMMENT '请求结果描述',
    `sgr_orgid` bigint NOT NULL COMMENT '所在组织ID',
    `sgr_creatoruserid` bigint NOT NULL COMMENT '创建的用户ID',
    `sgr_creationtime` datetime(6) NOT NULL COMMENT '创建时间',
    CONSTRAINT `PK_sgr_logoperate` PRIMARY KEY (`sgr_id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `sgr_organization` (
    `sgr_id` bigint NOT NULL AUTO_INCREMENT COMMENT '主键',
    `m_code` varchar(200) CHARACTER SET utf8mb4 NOT NULL COMMENT '组织机构编码',
    `m_name` varchar(200) CHARACTER SET utf8mb4 NOT NULL COMMENT '组织机构名称',
    `m_orgtypecode` varchar(50) CHARACTER SET utf8mb4 NOT NULL COMMENT '组织机构类型编码',
    `m_areacode` varchar(50) CHARACTER SET utf8mb4 NOT NULL COMMENT '所属行政区划编码',
    `m_ordernumber` int NOT NULL COMMENT '组织机构排序号',
    `m_leader` varchar(50) CHARACTER SET utf8mb4 NULL COMMENT '机构负责人',
    `m_phone` varchar(50) CHARACTER SET utf8mb4 NULL COMMENT '联系电话',
    `m_email` varchar(50) CHARACTER SET utf8mb4 NULL COMMENT '联系邮箱',
    `m_address` varchar(200) CHARACTER SET utf8mb4 NULL COMMENT '所在地址',
    `m_logourl` varchar(200) CHARACTER SET utf8mb4 NULL COMMENT 'Logo地址',
    `m_remarks` varchar(500) CHARACTER SET utf8mb4 NULL COMMENT '描述',
    `m_state` int NOT NULL COMMENT '组织机构状态',
    `sgr_parentid` bigint NOT NULL COMMENT '上级节点Id',
    `sgr_nodepath` varchar(255) CHARACTER SET utf8mb4 NOT NULL COMMENT '树节点层次目录',
    `sgr_rowversion` bigint NOT NULL COMMENT '行版本',
    `sgr_extensiondata` varchar(2047) CHARACTER SET utf8mb4 NULL COMMENT '扩展对象/实体',
    `sgr_creatoruserid` bigint NOT NULL COMMENT '创建的用户ID',
    `sgr_creationtime` datetime(6) NOT NULL COMMENT '创建时间',
    `sgr_lastmodifieruserid` bigint NOT NULL COMMENT '修改的用户ID',
    `sgr_lastmodificationtime` datetime(6) NULL COMMENT '修改时间',
    CONSTRAINT `PK_sgr_organization` PRIMARY KEY (`sgr_id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `sgr_role` (
    `sgr_id` bigint NOT NULL AUTO_INCREMENT COMMENT '主键',
    `m_code` varchar(200) CHARACTER SET utf8mb4 NOT NULL COMMENT '角色编码',
    `m_rolename` varchar(200) CHARACTER SET utf8mb4 NOT NULL COMMENT '角色名称',
    `m_ordernumber` int NOT NULL COMMENT '排序号',
    `m_remarks` varchar(500) CHARACTER SET utf8mb4 NULL COMMENT '备注',
    `m_state` int NOT NULL COMMENT '状态',
    `sgr_rowversion` bigint NOT NULL COMMENT '行版本',
    `sgr_orgid` bigint NOT NULL COMMENT '所在组织ID',
    `sgr_creatoruserid` bigint NOT NULL COMMENT '创建的用户ID',
    `sgr_creationtime` datetime(6) NOT NULL COMMENT '创建时间',
    `sgr_lastmodifieruserid` bigint NOT NULL COMMENT '修改的用户ID',
    `sgr_lastmodificationtime` datetime(6) NULL COMMENT '修改时间',
    CONSTRAINT `PK_sgr_role` PRIMARY KEY (`sgr_id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `sgr_user` (
    `sgr_id` bigint NOT NULL AUTO_INCREMENT COMMENT '主键',
    `m_loginname` varchar(200) CHARACTER SET utf8mb4 NOT NULL COMMENT '登录名称',
    `m_loginpassword` varchar(100) CHARACTER SET utf8mb4 NOT NULL COMMENT '登录密码',
    `m_firstlogintime` datetime(6) NULL COMMENT '首次登录时间',
    `m_lastlogintime` datetime(6) NULL COMMENT '最近一次登录时间',
    `m_loginsuccesscount` int NOT NULL COMMENT '登录成功次数',
    `m_loginfailcount` int NOT NULL COMMENT '登录失败次数',
    `m_username` varchar(200) CHARACTER SET utf8mb4 NULL COMMENT '用户姓名',
    `m_userphone` varchar(30) CHARACTER SET utf8mb4 NULL COMMENT '用户绑定的手机号码',
    `m_useremail` varchar(100) CHARACTER SET utf8mb4 NULL COMMENT '用户邮箱地址',
    `m_qq` varchar(20) CHARACTER SET utf8mb4 NULL COMMENT '用户QQ号码',
    `m_wechat` varchar(50) CHARACTER SET utf8mb4 NULL COMMENT '用户微信号码',
    `m_issuperadmin` tinyint(1) NOT NULL,
    `m_state` int NOT NULL COMMENT '状态',
    `m_departmentid` bigint NULL COMMENT '所属部门Id',
    `m_departmentname` varchar(200) CHARACTER SET utf8mb4 NOT NULL COMMENT '所属部门名称',
    `sgr_rowversion` bigint NOT NULL COMMENT '行版本',
    `sgr_orgid` bigint NOT NULL COMMENT '所在组织ID',
    `sgr_creatoruserid` bigint NOT NULL COMMENT '创建的用户ID',
    `sgr_creationtime` datetime(6) NOT NULL COMMENT '创建时间',
    `sgr_lastmodifieruserid` bigint NOT NULL COMMENT '修改的用户ID',
    `sgr_lastmodificationtime` datetime(6) NULL COMMENT '修改时间',
    CONSTRAINT `PK_sgr_user` PRIMARY KEY (`sgr_id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `sgr_role_resource` (
    `sgr_id` varchar(255) CHARACTER SET utf8mb4 NOT NULL COMMENT '主键',
    `m_resourcecode` varchar(200) CHARACTER SET utf8mb4 NOT NULL COMMENT '资源标识',
    `m_resourcetype` int NOT NULL COMMENT '资源类型',
    `m_roleid` bigint NOT NULL COMMENT '角色标识',
    CONSTRAINT `PK_sgr_role_resource` PRIMARY KEY (`sgr_id`),
    CONSTRAINT `FK_sgr_role_resource_sgr_role_m_roleid` FOREIGN KEY (`m_roleid`) REFERENCES `sgr_role` (`sgr_id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `sgr_user_duty` (
    `sgr_id` varchar(255) CHARACTER SET utf8mb4 NOT NULL COMMENT '主键',
    `m_dutyid` bigint NOT NULL COMMENT '岗位标识',
    `m_userid` bigint NOT NULL COMMENT '用户标识',
    CONSTRAINT `PK_sgr_user_duty` PRIMARY KEY (`sgr_id`),
    CONSTRAINT `FK_sgr_user_duty_sgr_user_m_userid` FOREIGN KEY (`m_userid`) REFERENCES `sgr_user` (`sgr_id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `sgr_user_role` (
    `sgr_id` varchar(255) CHARACTER SET utf8mb4 NOT NULL COMMENT '主键',
    `m_roleid` bigint NOT NULL COMMENT '角色标识',
    `m_userid` bigint NOT NULL COMMENT '用户标识',
    CONSTRAINT `PK_sgr_user_role` PRIMARY KEY (`sgr_id`),
    CONSTRAINT `FK_sgr_user_role_sgr_user_m_userid` FOREIGN KEY (`m_userid`) REFERENCES `sgr_user` (`sgr_id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_sgr_department_sgr_nodepath` ON `sgr_department` (`sgr_nodepath`);

CREATE INDEX `IX_sgr_department_sgr_orgid` ON `sgr_department` (`sgr_orgid`);

CREATE INDEX `IX_sgr_department_sgr_parentid` ON `sgr_department` (`sgr_parentid`);

CREATE INDEX `IX_sgr_duty_sgr_orgid` ON `sgr_duty` (`sgr_orgid`);

CREATE INDEX `IX_sgr_loglogin_sgr_orgid` ON `sgr_loglogin` (`sgr_orgid`);

CREATE INDEX `IX_sgr_logoperate_sgr_orgid` ON `sgr_logoperate` (`sgr_orgid`);

CREATE INDEX `IX_sgr_organization_m_code` ON `sgr_organization` (`m_code`);

CREATE INDEX `IX_sgr_organization_sgr_nodepath` ON `sgr_organization` (`sgr_nodepath`);

CREATE INDEX `IX_sgr_organization_sgr_parentid` ON `sgr_organization` (`sgr_parentid`);

CREATE INDEX `IX_sgr_role_sgr_orgid` ON `sgr_role` (`sgr_orgid`);

CREATE INDEX `IX_sgr_role_resource_m_roleid` ON `sgr_role_resource` (`m_roleid`);

CREATE INDEX `IX_sgr_user_m_departmentid` ON `sgr_user` (`m_departmentid`);

CREATE INDEX `IX_sgr_user_m_loginname` ON `sgr_user` (`m_loginname`);

CREATE INDEX `IX_sgr_user_sgr_orgid` ON `sgr_user` (`sgr_orgid`);

CREATE INDEX `IX_sgr_user_duty_m_userid` ON `sgr_user_duty` (`m_userid`);

CREATE INDEX `IX_sgr_user_role_m_userid` ON `sgr_user_role` (`m_userid`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230814004810_Sgr0815', '7.0.10');

COMMIT;

