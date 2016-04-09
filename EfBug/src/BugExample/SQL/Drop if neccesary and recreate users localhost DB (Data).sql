-- Create standard login
DECLARE @spidstr varchar(8000)
IF EXISTS 
    (SELECT name  
     FROM master.sys.server_principals
     WHERE name = 'hyperpragiaLogin')
BEGIN
	SET @spidstr = NULL
	SELECT @spidstr=coalesce(@spidstr,'' )+'kill '+convert(varchar, session_id)+ '; '
	FROM sys.dm_exec_sessions WHERE login_name = 'hyperpragiaLogin'
	IF LEN(@spidstr) > 0
	BEGIN
		EXEC(@spidstr)
	END

	ALTER LOGIN [hyperpragiaLogin] DISABLE;
	DROP LOGIN [hyperpragiaLogin]
END
/* CREATE */ CREATE LOGIN [hyperpragiaLogin] WITH PASSWORD=N'Ho*ncBr@q4IfiB0}99za&2nY!', DEFAULT_DATABASE=[BugExample.App.Data], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF

-- Create migrations login
IF EXISTS 
    (SELECT name  
     FROM master.sys.server_principals
     WHERE name = 'repulsingLogin')
BEGIN
	SET @spidstr = NULL
	SELECT @spidstr=coalesce(@spidstr,'' )+'kill '+convert(varchar, session_id)+ '; '
	FROM sys.dm_exec_sessions WHERE login_name = 'repulsingLogin'

	IF LEN(@spidstr) > 0
	BEGIN
		EXEC(@spidstr)
	END

	DROP LOGIN [repulsingLogin]
END
/* END IF NO CREATE */
CREATE LOGIN [repulsingLogin] WITH PASSWORD=N'5UmC*h8}po@:C>#3WP.j4|^p9', DEFAULT_DATABASE=[BugExample.App.Data], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
--EXEC sp_addsrvrolemember @loginame = N'repulsingLogin', @rolename = N'dbcreator'

-- Create data user
USE [BugExample.App.Data]
IF EXISTS
    (SELECT name
     FROM sys.database_principals
     WHERE name = 'hyperpragia')
BEGIN
	DROP USER [hyperpragia]
END
CREATE USER [hyperpragia] FOR LOGIN [hyperpragiaLogin]
-- Configure standard user read/write permissions
EXEC SP_ADDROLEMEMBER DB_DATAREADER, [hyperpragia]
EXEC SP_ADDROLEMEMBER DB_DATAWRITER, [hyperpragia]
GO

-- Create data migrations user
USE [BugExample.App.Data]
IF EXISTS
    (SELECT name
     FROM sys.database_principals
     WHERE name = 'repulsing')
BEGIN
	DROP USER [repulsing]
END
CREATE USER [repulsing] FOR LOGIN [repulsingLogin]
EXEC SP_ADDROLEMEMBER N'db_owner', N'repulsing'
GO

