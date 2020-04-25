USE [DncZero]
IF NOT EXISTS(SELECT * FROM [dbo].[Users] WHERE Id=N'3B92C145-E684-4F12-AFC7-74CDCED8FAEC')
BEGIN
	INSERT INTO [dbo].[Users] VALUES (N'3B92C145-E684-4F12-AFC7-74CDCED8FAEC'
	, 0, N'0', 1, NULL
	, N'向以胜', N'向', N'以胜', N'向以胜'
	, N'18636953187', N'18636953187@163.com', N'1995-01-23', N'0'
	, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0'
	, N'湖北省', N'随州市', N'广水市', N'应山县城郊乡街道', N'板子桥村七组'
	, NULL, NULL, NULL, NULL, NULL
	, N'2020-04-12 22:07:53.000', N'fe80::78e5:dfe7:7870:cc10%9'
	, N'2020-04-12 22:07:53.000', N'fe80::78e5:dfe7:7870:cc10%9'
	, N'2020-04-17 23:47:47.200', N'fe80::78e5:dfe7:7870:cc10%9')
END
SELECT * FROM Users
IF NOT EXISTS(SELECT * FROM [dbo].[UserAuths] WHERE Id=N'E33CE21E-CECB-4C78-9307-F39ABEED3816')
BEGIN
	INSERT INTO [dbo].[UserAuths] VALUES (N'E33CE21E-CECB-4C78-9307-F39ABEED3816'
	, N'3B92C145-E684-4F12-AFC7-74CDCED8FAEC'
	, N'email'
	, N'agooday@dnc.com'
	, N'790513ebe30e50cdbf8e31b42fddb777'
	, N'1'
	, GETDATE()
	, GETDATE()
	, 1
	, NULL)
END
SELECT * FROM UserAuths

--DELETE [UserAuths]
--SELECT NEWID()



-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SELECT
     表名       = Case When A.colorder=1 Then D.name Else '' End,
     表说明     = Case When A.colorder=1 Then isnull(F.value,'') Else '' End,
     字段序号   = A.colorder,
     字段名     = A.name,
     字段说明   = isnull(G.[value],''),
     标识       = Case When COLUMNPROPERTY( A.id,A.name,'IsIdentity')=1 Then N'是'Else '' End,
     主键       = Case When exists(SELECT 1 FROM sysobjects Where xtype='PK' and parent_obj=A.id and name in (
                      SELECT name FROM sysindexes WHERE indid in( SELECT indid FROM sysindexkeys WHERE id = A.id AND colid=A.colid))) then N'是' else '' end,
     类型       = B.name,
     占用字节数 = A.Length,
     长度       = COLUMNPROPERTY(A.id,A.name,'PRECISION'),
     小数位数   = isnull(COLUMNPROPERTY(A.id,A.name,'Scale'),0),
     允许空     = Case When A.isnullable=1 Then '√'Else N'' End,--
     默认值     = isnull(E.Text,'')
 FROM
     syscolumns A
 Left Join
     systypes B
 On
     A.xusertype=B.xusertype
 Inner Join
     sysobjects D
 On
     A.id=D.id  and D.xtype='U' and  D.name<>'dtproperties'
 Left Join
     syscomments E
 on
     A.cdefault=E.id
 Left Join
 sys.extended_properties  G
 on
     A.id=G.major_id and A.colid=G.minor_id
 Left Join
 sys.extended_properties F
 On
     D.id=F.major_id and F.minor_id=0
     --where d.name='OrderInfo'    --如果只查询指定表,加上此条件
 Order By
     A.id,A.colorder

