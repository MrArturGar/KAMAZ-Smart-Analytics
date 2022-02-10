select TOP (1000) ECUs.Codifier, Identifications.Name, Identifications.Value, Sessions.Date

from Identifications, ECU_Identification,Session_ECUIdentification, ECUs ,Sessions,Vehicles

where 
Identifications.id = ECU_Identification.id_Identifications and 
ECUs.id = ECU_Identification.id_ECU and 
ECU_Identification.id = Session_ECUIdentification.id_ECUIdentifications and 
Sessions.id = Session_ECUIdentification.id_Session and
Vehicles.VIN = 'XTC549015J2517503' and
Sessions.Date > '2021-12-30 18:00:00'

GROUP BY ECUs.Codifier, Sessions.Date, Identifications.Name, Identifications.Value
ORDER BY ECUs.Codifier