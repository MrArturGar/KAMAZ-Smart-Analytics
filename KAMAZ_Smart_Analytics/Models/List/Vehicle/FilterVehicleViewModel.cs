using Microsoft.AspNetCore.Mvc.Rendering;

namespace KAMAZ_Smart_Analytics.Models.List
{
    public class FilterVehicleViewModel
    {
        public FilterVehicleViewModel(string vin, string design_number, string iccid, string iccidc, string imei, string type)
        {
            SelectedVin = vin;
            SelectedDesign_number = design_number;
            SelectedIccid = iccid;
            SelectedIccidc = iccidc;
            SelectedImei = imei;
            SelectedType = type;
        }
        public string? SelectedVin { get; private set; }
        public string? SelectedDesign_number { get; private set; }
        public string? SelectedIccid { get; private set; }
        public string? SelectedIccidc { get; private set; }
        public string? SelectedImei { get; private set; }
        public string? SelectedType { get; private set; }
    }
}
