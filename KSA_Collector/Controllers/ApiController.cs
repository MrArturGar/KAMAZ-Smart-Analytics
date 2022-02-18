using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableModelLibrary.Models;

namespace KSA_Collector.Controllers
{
    internal class ApiController : IDisposable
    {
        swaggerClient client;

        public ApiController()
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

        internal int SaveSessionEcuIdentification(SessionEcuidentification sessionEcuidentification)
        {
            Task<int> response = client.PostSessionEcuIdentificationAsync(sessionEcuidentification);
            response.Wait();
            return response.Result;
        }

        internal int SaveSession(Session session)
        {
            Task<int> response = client.PostSessionAsync(session);
            response.Wait();
            return response.Result;
        }
        internal void SaveComposite(Composite composite)
        {
            client.PostCompositeAsync(composite);
        }

        internal int SaveServiceCenter(ServiceCenter serviceCenter)
        {
            Task<int> response = client.PostServiceCenterAsync(serviceCenter);
            response.Wait();
            return response.Result;
        }

        internal int SaveAoglonassReport(AoglonassReport aoglonassReport)
        {
            Task<int> response = client.PostAoglonassReportAsync(aoglonassReport);
            response.Wait();
            return response.Result;
        }

        internal int SaveProcedureReport(ProcedureReport procedureReport)
        {
            Task<int> response = client.PostProcedureReportAsync(procedureReport);
            response.Wait();
            return response.Result;
        }

        internal int SaveControlSystem(ControlSystem controlSystem)
        {
            Task<int> response = client.PostControlSystemAsync(controlSystem);
            response.Wait();
            return response.Result;
        }

        internal int SaveEcu(Ecu ecu)
        {
            Task<int> response = client.PostEcuAsync(ecu);
            response.Wait();
            return response.Result;
        }

        internal int SaveVehicle(Vehicle vehicle)
        {
            Task<int> response = client.PostVehicleAsync(vehicle);
            response.Wait();
            return response.Result;
        }

        internal int SaveEcuIdentification(EcuIdentification ecuIdentification)
        {
            Task<int> response = client.PostEcuIdentificationAsync(ecuIdentification);
            response.Wait();
            return response.Result;
        }
        internal int SaveIdentification(Identification identification)
        {
            Task<int> response = client.PostIdentificationAsync(identification);
            response.Wait();
            return response.Result;
        }

        internal int GetControlSystemId(ControlSystem controlSystem)
        {
            Task<ControlSystem> response = client.GetControlSystemAsync(controlSystem.Name, controlSystem.Domain);
            try
            {
                response.Wait();
                return response.Result.Id;
            }
            catch (AggregateException ex)
            {
                ExceptionResponse(ex);
                return SaveControlSystem(controlSystem);
            }
        }

        internal int GetEcuId(Ecu ecu)
        {
            Task<Ecu> response = client.GetEcuAsync(ecu.Codifier);
            try
            {
                response.Wait();
                return response.Result.Id;
            }
            catch (AggregateException ex)
            {
                ExceptionResponse(ex);
                return SaveEcu(ecu);
            }
        }

        internal int GetIdentificationId(Identification identification)
        {
            Task<Identification> response = client.GetIdentificationAsync(identification.Name, identification.Value);
            try
            {
                response.Wait();
                return response.Result.Id;

            }
            catch (AggregateException ex)
            {
                ExceptionResponse(ex);

                return SaveIdentification(identification);
            }
        }

        internal int GetEcuIdentificationId(EcuIdentification ecuIdentification)
        {
            Task<EcuIdentification> response = client.GetEcuIdentificationAsync(ecuIdentification.IdEcu, ecuIdentification.IdIdentifications);
            try
            {
                response.Wait();
                return response.Result.Id;
            }
            catch (AggregateException ex)
            {
                ExceptionResponse(ex);

                return SaveEcuIdentification(ecuIdentification);
            }
        }

        internal int GetSessionId(Session session)
        {
            Task<Session> response = client.GetSessionAsync(session.SessionsName);
            try
            {
                response.Wait();
                return response.Result.Id;
            }
            catch (AggregateException ex)
            {
                ExceptionResponse(ex);
                return SaveSession(session);
            }
        }

        internal int GetVehicleId(Vehicle vehicle)
        {
            Task<Vehicle> response = client.GetVehicleAsync(vehicle.Vin, vehicle.DesignNumber, vehicle.Iccid, vehicle.Iccidc, vehicle.Imei, vehicle.Type);
            try
            {
                response.Wait();
                return response.Result.Id;
            }
            catch (AggregateException ex)
            {
                ExceptionResponse(ex);
                return SaveVehicle(vehicle);
            }
        }

        internal int GetServiceCenterId(string username)
        {
            Task<ServiceCenter> response = client.GetServiceCenterAsync(username);
            try
            {
                response.Wait();
                return response.Result.Id;
            }
            catch (AggregateException ex)
            {
                ExceptionResponse(ex);
                return SaveServiceCenter(new ServiceCenter()
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
                });
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
