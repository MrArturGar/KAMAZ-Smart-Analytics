using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSA_Collector.Controllers
{
    internal class DBController : IDisposable
    {
        swaggerClient client;

        public DBController()
        {
            client = new swaggerClient("127.0.0.1:7234", new HttpClient());
            //var loginRequest = new LoginRequest();
            //client.
        }
        public void Dispose()
        {
            if (client != null)
            {
                client = null;
            }
        }

        internal void SaveSessionEcuIdentification(SessionEcuidentification sessionEcuidentification)
        {
            client.PostSessionEcuIdentificationAsync(sessionEcuidentification);
        }

        internal void SaveSession(Session session)
        {
            client.PostSessionAsync(session);
        }
        internal void SaveComposite(Composite composite)
        {
            client.PostComposite(composite);
        }

        internal void SaveServiceCenter(ServiceCenter serviceCenter)
        {
            client.PostServiceCenter(serviceCenter);
        }

        internal void SaveAoglonassReport(AoglonassReport aoglonassReport)
        {
            client.PostAoglonassReportAsync(aoglonassReport);
        }

        internal void SaveProcedureReport(ProcedureReport procedureReport)
        {
            client.PostProcedureReportAsync(procedureReport);
        }

        internal Gear GetGear(Gear gear)
        {
            client.GetGear(gear.Name, gear.Domain);///////////////////////////////////////////////////////////////////////////////
        }

        internal Ecu GetECU(Ecu ecu)
        {
            client.GetEcuAsync(ecu.Codifier);
        }

        internal Identification GetIdentification(Identification identification)
        {
            Identification tmp = Context.Identifications.Where(c => c.Name == identification.Name && c.Value == identification.Value).SingleOrDefault();

            if (tmp != null)
                return tmp;
            else
            {
                Context.Identifications.Add(identification);
                Context.SaveChanges();
                return identification;
            }
        }

        internal EcuIdentification GetEcuIdentification(EcuIdentification ecuIdentification)
        {
            EcuIdentification tmp = Context.EcuIdentifications.Where(c => c.IdEcuNavigation == ecuIdentification.IdEcuNavigation && c.IdIdentificationsNavigation == ecuIdentification.IdIdentificationsNavigation).SingleOrDefault();

            if (tmp != null)
                return tmp;
            else
                return ecuIdentification;
        }

        internal Session GetSession(Session session)
        {
            Session tmp = Context.Sessions.Where(c => c.SessionsName == session.SessionsName).SingleOrDefault();

            if (tmp != null)
                return tmp;
            else
                return session;
        }

        internal Vehicle GetVehicle(Vehicle vehicle)
        {
            Vehicle tmp = Context.Vehicles.Where(c => c.Vin == vehicle.Vin && c.Iccid == vehicle.Iccid && c.Iccidc == vehicle.Iccidc && c.Imei == vehicle.Imei && c.Type == vehicle.Type).SingleOrDefault();

            if (tmp != null)
                return tmp;
            else
                return vehicle;
        }

        internal ServiceCenter GetServiceCenter(string username)
        {
            try
            {
                var users = Context.ServiceCenters.Where(c => c.Username == username).ToList();


                if (users.Count == 1)
                    return users[0];
                else if (users.Count > 1)
                {
                    for (int i = 0; i < users.Count; i++)
                    {
                        if (users[i].Status == "")
                            return users[i];

                        if (i + 1 == users.Count)
                            return users[i];
                    }
                }
                else if (users.Count == 0)
                {
                    return new ServiceCenter()
                    {
                        Name = null,
                        Address = null,
                        City = null,
                        Country = null,
                        Postcode = null,
                        Region = null,
                        Username = username,
                        Status = null,
                        DilerTr = null
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                ServiceCentersControllers controller = new();
                controller.DeleteHash();
                throw ex;
            }
        }

    }
}
