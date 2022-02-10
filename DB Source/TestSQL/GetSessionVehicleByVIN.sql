select VIN, Date, VersionDB,VCISN, Mileage
from Vehicles, Sessions
where Vehicles.VIN = 'XTC549015J2517503' and Sessions.id_Vehicle = Vehicles.id