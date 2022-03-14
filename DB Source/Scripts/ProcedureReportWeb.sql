CREATE VIEW [dbo].[ProcedureReportWeb]
	AS SELECT ProcedureReports.id, ProcedureReports.id_ECU, Codifier, id_session, type, Name, Result, Date_start,Date_end, Using_VIN, DataFiles FROM [ProcedureReports], [ECUs]
	where id_ECU = ECUs.id
