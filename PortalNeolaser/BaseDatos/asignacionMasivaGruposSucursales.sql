DELETE FROM Surcusales_Grupos

DBCC CHECKIDENT ('Surcusales_Grupos', RESEED, 0)  

DECLARE @cntSucursales INT = 0
DECLARE @maxSucursal  INT = 0
DECLARE @cntGrupos INT = 0
DECLARE @maxGrupos INT = 0
SET @cntSucursales = 422
SET @maxSucursal = 842

SET @cntGrupos = (Select Min(Id) from GruposElementos)
SET @maxGrupos = (Select Max(Id) from GruposElementos)

WHILE @cntSucursales <= @maxSucursal
BEGIN  

	SET @cntGrupos = (Select Min(Id) from GruposElementos)
	
	WHILE @cntGrupos <= @maxGrupos
	BEGIN
			Insert into Surcusales_Grupos (FkSucursal, FkGrupo)
			Values (@cntSucursales, @cntGrupos)
			SET @cntGrupos = @cntGrupos + 1
	END
	SET @cntSucursales = @cntSucursales + 1
END  