using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSA_Collector.Controllers
{
    internal class ApiController : IDisposable
    {
        swaggerClient _client;
        string Token;
        string APP_PATH = "https://localhost:7234/";

        public ApiController()
        {
            _client = Init();

            if (!CheckAuth())
            {
                Auth();
                _client = Init();
            }
        }
        public swaggerClient GetClient
        {
            get { return _client; }
        }
        private swaggerClient Init()
        {
            HttpClient httpClient = new HttpClient();
            var header = httpClient.DefaultRequestHeaders;

            if (Token != null)
                header.Add("Authorization", $"Bearer {Token}");

            return new swaggerClient(APP_PATH, httpClient);
        }

        public bool CheckAuth()
        {
            Task result = _client.GetResultAsync();
            try
            {
                result.Wait();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void Auth()
        {
            //var result = Client.AuthAsync("123", "123");
            //result.Wait();
            string username = "123";
            string password = "123";

            HttpClient client_ = new HttpClient();
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(APP_PATH != null ? APP_PATH.TrimEnd('/') : "").Append("/api/Auth/Auth?");
            if (username != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("Username") + "=").Append(System.Uri.EscapeDataString(username) + "&");
            }
            if (password != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("Password") + "=").Append(System.Uri.EscapeDataString(password) + "&");
            }
            urlBuilder_.Length--;

            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Content = new System.Net.Http.StringContent(string.Empty, System.Text.Encoding.UTF8, "application/json");
                    request_.Method = new System.Net.Http.HttpMethod("POST");


                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);


                    var response_ = client_.Send(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, CancellationToken.None);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }


                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var task = response_.Content == null ? null : response_.Content.ReadAsStringAsync();
                            task.Wait();
                            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(task.Result);
                            if (values["message"] == "Success")
                                Token = values["token"];
                        }
                        else
                        {
                            var task = response_.Content == null ? null : response_.Content.ReadAsStringAsync();
                            task.Wait();
                            var responseData_ = task.Result;
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
            //response_.Content.ReadAsStringAsync().ConfigureAwait(false);

        }


        public void Dispose()
        {
            if (_client != null)
            {
                _client = null;
            }
        }

        internal int SaveSessionEcuIdentification(SessionEcuidentification sessionEcuidentification)
        {
            Task<int> response = _client.PostSessionEcuIdentificationAsync(sessionEcuidentification);
            response.Wait();
            return response.Result;
        }

        internal int SaveSessionDtc(SessionDtc sessionDtc)
        {
            Task<int> response = _client.PostSessionDtcAsync(sessionDtc);
            response.Wait();
            return response.Result;
        }

        internal int SaveSession(Session session)
        {
            Task<int> response = _client.PostSessionAsync(session);
            response.Wait();
            return response.Result;
        }
        internal void SaveComposite(Composite composite)
        {
            _client.PostCompositeAsync(composite);
        }

        internal int SaveServiceCenter(ServiceCenter serviceCenter)
        {
            Task<int> response = _client.PostServiceCenterAsync(serviceCenter);
            response.Wait();
            return response.Result;
        }

        internal int SaveAoglonassReport(AoglonassReport aoglonassReport)
        {
            Task<int> response = _client.PostAoglonassReportAsync(aoglonassReport);
            response.Wait();
            return response.Result;
        }

        internal int SaveProcedureReport(ProcedureReport procedureReport)
        {
            Task<int> response = _client.PostProcedureReportAsync(procedureReport);
            response.Wait();
            return response.Result;
        }

        internal int SaveControlSystem(ControlSystem controlSystem)
        {
            Task<int> response = _client.PostControlSystemAsync(controlSystem);
            response.Wait();
            return response.Result;
        }

        internal int SaveEcu(Ecu ecu)
        {
            Task<int> response = _client.PostEcuAsync(ecu);
            response.Wait();
            return response.Result;
        }

        internal int SaveVehicle(Vehicle vehicle)
        {
            Task<int> response = _client.PostVehicleAsync(vehicle);
            response.Wait();
            return response.Result;
        }

        internal int SaveEcuIdentification(EcuIdentification ecuIdentification)
        {
            Task<int> response = _client.PostEcuIdentificationAsync(ecuIdentification);
            response.Wait();
            return response.Result;
        }
        internal int SaveDtc(Dtc dtc)
        {
            Task<int> response = _client.PostDtcAsync(dtc);
            response.Wait();
            return response.Result;
        }
        internal int SaveIdentification(Identification identification)
        {
            Task<int> response = _client.PostIdentificationAsync(identification);
            response.Wait();
            return response.Result;
        }

        internal int GetControlSystemId(ControlSystem controlSystem)
        {
            Task<ControlSystem> response = _client.GetControlSystemAsync(controlSystem.Name, controlSystem.Domain);
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
            Task<Ecu> response = _client.GetEcuAsync(ecu.Codifier);
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
            Task<Identification> response = _client.GetIdentificationAsync(identification.Name, identification.Value);
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
        internal int GetDtcId(Dtc dtc)
        {
            Task<Dtc> response = _client.GetDtcAsync(dtc.Code, dtc.TroubleCode, dtc.VehicleType);
            try
            {
                response.Wait();
                return response.Result.Id;

            }
            catch (AggregateException ex)
            {
                ExceptionResponse(ex);

                return SaveDtc(dtc);
            }
        }

        internal int GetEcuIdentificationId(EcuIdentification ecuIdentification)
        {
            Task<EcuIdentification> response = _client.GetEcuIdentificationAsync(ecuIdentification.IdEcu, ecuIdentification.IdIdentifications);
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
            Task<Session> response = _client.GetSessionByNameAsync(session.SessionsName);
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
            Task<Vehicle> response = _client.GetVehicleAsync(vehicle.Vin, vehicle.DesignNumber, vehicle.Iccid, vehicle.Iccidc, vehicle.Imei, vehicle.Type);
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
            Task<ServiceCenter> response = _client.GetServiceCenterAsync(username);
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
