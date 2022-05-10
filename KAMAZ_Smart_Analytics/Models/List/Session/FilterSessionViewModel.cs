using Microsoft.AspNetCore.Mvc.Rendering;

namespace KAMAZ_Smart_Analytics.Models.List
{
    public class FilterSessionViewModel
    {

        public FilterSessionViewModel(List<string> VersionDBs, string VersionDb, string? Vin, string? Type, string? Username, 
            string? Vcisn, double? MileageStart, double? MileageEnd, bool? HasIdentifications, bool? HasDtc, bool? HasTests, 
            bool? HasFlash, DateTime? dateStart, DateTime? dateEnd)
        {
            Versions = new SelectList(VersionDBs, VersionDb);
            SelectedVin = Vin;
            SelectedType = Type;
            SelectedUsername = Username;
            SelectedVersionDB = VersionDb;
            SelectedVcisn = Vcisn;
            SelectedMileageStart = MileageStart;
            SelectedMileageEnd = MileageEnd;
            SelectedHasIdentifications = HasIdentifications;
            SelectedHasDtc = HasDtc;
            SelectedHasTests = HasTests;
            SelectedHasFlash = HasFlash;
            SelectedDateStart = dateStart;
            SelectedDateEnd = dateEnd;
        }

        public SelectList Versions { get; private set; }
        public string? SelectedVin { get; private set; }
        public string? SelectedType { get; private set; }
        public string? SelectedUsername { get; private set; }
        public string? SelectedVersionDB { get; private set; }
        public string? SelectedVcisn { get; private set; }
        public double? SelectedMileageStart { get; private set; }
        public double? SelectedMileageEnd { get; private set; }
        public bool? SelectedHasIdentifications { get; private set; }
        public bool? SelectedHasDtc { get; private set; }
        public bool? SelectedHasTests { get; private set; }
        public bool? SelectedHasFlash { get; private set; }
        public DateTime? SelectedDateStart { get; private set; }
        public DateTime? SelectedDateEnd { get; private set; }
    }
}
