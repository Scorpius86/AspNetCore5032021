CREATE FUNCTION [Security].[CheckRoleByUserId]
(
	@UserId NVARCHAR(450),
	@RoleName NVARCHAR(256)
)
RETURNS VARCHAR(5)
AS
BEGIN
	IF EXISTS (
		SELECT 
			* 
		FROM 
			[Security].Users u 
			INNER JOIN [Security].UserRoles ur ON u.Id = ur.UserId
			INNER JOIN [Security].Roles r ON ur.RoleId = r.Id
		WHERE 
			u.Id = @UserId
			AND r.[Name] = @RoleName
	)
        RETURN 'True'
    RETURN 'False'
END
