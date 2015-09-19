

USE Wisconsin
GO

UPDATE tblAgencyFull
SET  SchoolType = '3'
WHERE 
fullkey = '013619040020'
AND year = 2011

GO 

UPDATE tblAgencyFullDistinct
SET  SchoolType = '3'
WHERE 
fullkey = '013619040020'

GO

UPDATE tblPubEnrWWoDisSuppressed
SET  SchoolType = '3'
--SELECT * FROM tblPubEnrWWoDisSuppressed
WHERE 
fullkey = '013619040020'
AND YEAR = 2011

GO

UPDATE tblPubEnrWWoDisSuppressedEconELP
SET  SchoolType = '3'
--SELECT * FROM tblPubEnrWWoDisSuppressedEconELP
WHERE 
fullkey = '013619040020'
AND YEAR = 2011
GO


UPDATE tblPubEnrWWoDisSuppressedEconELPSchoolDistStateFlat
SET  SchoolType = '3'
--SELECT * FROM tblPubEnrWWoDisSuppressedEconELPSchoolDistStateFlat
WHERE 
fullkey = '013619040020'
AND YEAR = 2011
GO

UPDATE tblPubEnrSuppressedStateAll
SET  SchoolType = '3'
--SELECT * FROM tblPubEnrSuppressedStateAll
WHERE 
fullkey = '013619040020'
AND YEAR = 2011
GO
