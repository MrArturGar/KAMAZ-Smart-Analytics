CREATE VIEW [dbo].[IdentificationWeb]
	AS select id_Session, Codifier, Name, Value from Sessions, Session_ECUIdentification, ECU_Identification,ECUs, Identifications
where Sessions.id = id_Session and id_ECUIdentifications =ECU_Identification.id and id_ECU = ECUs.id and  id_Identifications = Identifications.id