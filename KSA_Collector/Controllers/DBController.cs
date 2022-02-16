using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableModelLibrary.Models;

namespace KSA_Collector.Controllers
{
    internal class DBController : IDisposable
    {
        swaggerClient client;

        public DBController()
        {
            client = new swaggerClient("https://localhost:7234/", new HttpClient());
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
            client.PostCompositeAsync(composite);
        }

        internal void SaveServiceCenter(ServiceCenter serviceCenter)
        {
            client.PostServiceCenterAsync(serviceCenter);
        }

        internal void SaveAoglonassReport(AoglonassReport aoglonassReport)
        {
            client.PostAoglonassReportAsync(aoglonassReport);
        }

        internal void SaveProcedureReport(ProcedureReport procedureReport)
        {
            client.PostProcedureReportAsync(procedureReport);
        }

        internal ControlSystem GetControlSystem(ControlSystem controlSystem)
        {
            Task<ControlSystem> response = client.GetControlSystemAsync(controlSystem.Name, controlSystem.Domain);
            try
            {
                response.Wait();
                return response.Result;
            }
            catch (AggregateException ex)
            {
                ExceptionResponse(ex);
                return controlSystem;
            }
        }

        internal Ecu GetECU(Ecu ecu)
        {
            Task<Ecu> response = client.GetEcuAsync(ecu.Codifier);
            try
            {
                response.Wait();
                return response.Result;
            }
            catch (AggregateException ex)
            {
                ExceptionResponse(ex);
                return ecu;
            }
        }

        internal Identification GetIdentification(Identification identification)
        {
            Task<Identification> response = client.GetIdentificationAsync(identification.Name, identification.Value);
            try
            {
                response.Wait();
                return response.Result;

            }
            catch (AggregateException ex)
            {
                ExceptionResponse(ex);

                Task<int> idResponse = client.PostIdentificationAsync(identification);
                idResponse.Wait();
                identification.Id = idResponse.Result;
                return identification;
            }
        }

        internal EcuIdentification GetEcuIdentification(EcuIdentification ecuIdentification)
        {
            Task<EcuIdentification> response = client.GetEcuIdentificationAsync(ecuIdentification.IdEcuNavigation.Id, ecuIdentification.IdIdentificationsNavigation.Id);
            try
            {
                response.Wait();
                return response.Result;
            }
            catch (AggregateException ex)
            {
                ExceptionResponse(ex);

                return ecuIdentification;
            }
        }

        internal Session GetSession(Session session)
        {
            Task<Session> response = client.GetSessionAsync(session.SessionsName);
            try
            {
                response.Wait();
                return response.Result;
            }
            catch (AggregateException ex)
            {
                ExceptionResponse(ex);
                return session;
            }
        }

        internal Vehicle GetVehicle(Vehicle vehicle)
        {
            Task<Vehicle> response = client.GetVehicleAsync(vehicle.Vin, vehicle.Iccid, vehicle.Iccidc, vehicle.Imei, vehicle.Type);
            try
            {
                response.Wait();
                return response.Result;
            }
            catch (AggregateException ex)
            {
                ExceptionResponse(ex);
                return vehicle;
            }
        }

        internal ServiceCenter GetServiceCenter(string username)
        {
            Task<ServiceCenter> response = client.GetServiceCenterAsync(username);
            try
            {
                response.Wait();
                return response.Result;
            }
            catch (AggregateException ex)
            {
                ExceptionResponse(ex);
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
        }

        private void ExceptionResponse(Exception exception)
        {
            ApiException ex = (ApiException)exception.InnerException;
            if (ex.StatusCode != 204)
                throw ex;
        }

    }
}
