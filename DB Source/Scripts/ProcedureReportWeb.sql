CREATE VIEW [dbo].[ProcedureReportWeb]
	AS SELECT ProcedureReports.id, ProcedureReports.id_ECU, Codifier, id_session, ProcedureReports.Type, Name, Result, Date_start,Date_end, Vin, Using_VIN, DataFiles FROM [ProcedureReports], [ECUs], [Vehicles], [Sessions]
	where id_ECU = ECUs.id and id_session = Sessions.id and id_Vehicle = Vehicles.id