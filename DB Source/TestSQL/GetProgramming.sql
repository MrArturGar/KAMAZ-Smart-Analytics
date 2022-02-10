select ECUs.Codifier, Type, Name, Date_start,Date_end,Using_VIN,DataFiles, Result
From ProcedureReports, ECUs
where Type='Flash' and id_ECU = ECUs.id