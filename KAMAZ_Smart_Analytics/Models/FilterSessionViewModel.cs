using Microsoft.AspNetCore.Mvc.Rendering;

namespace KAMAZ_Smart_Analytics.Models
{
    public class FilterSessionViewModel
    {
        public FilterSessionViewModel(List<string> VersionDBs, string vin, string versionDb)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            VersionDBs.Insert(0, "Все");

            Versions = new SelectList(VersionDBs, versionDb);
            SelectedVin = vin;
            SelectedVersionDB = versionDb;
        }
        public SelectList Versions { get; private set; } // список компаний
        public string? SelectedVin { get; private set; }   // выбранная компания
        public string? SelectedVersionDB { get; private set; }    // введенное имя
    }
}
