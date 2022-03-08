using Microsoft.AspNetCore.Mvc.Rendering;

namespace KAMAZ_Smart_Analytics.Models
{
    public class FilterSessionViewModel
    {
        public FilterSessionViewModel(List<string> VersionDBs, string vin, string versionDb)
        {
            VersionDBs.Insert(0, "Все");

            Versions = new SelectList(VersionDBs, versionDb);
            SelectedVin = vin;
            SelectedVersionDB = versionDb;
        }
        public SelectList Versions { get; private set; }
        public string? SelectedVin { get; private set; }
        public string? SelectedVersionDB { get; private set; }
    }
}
