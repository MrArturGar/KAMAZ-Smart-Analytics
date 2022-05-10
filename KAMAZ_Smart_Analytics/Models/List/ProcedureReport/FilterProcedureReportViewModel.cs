using Microsoft.AspNetCore.Mvc.Rendering;

namespace KAMAZ_Smart_Analytics.Models.List
{
    public class FilterProcedureReportViewModel
    {
        public FilterProcedureReportViewModel(List<string> types, List<string> results, string type, string vin, string ecu, string result, string file, DateTime? dateStart, DateTime? dateEnd)
        {
            Types = new SelectList(types, type);
            Results = new SelectList(results, result);
            SelectedType = type;
            SelectedVin = vin;
            SelectedEcu = ecu;
            SelectedResult = result;
            SelectedFile = file;
            SelectedDateStart = dateStart;
            SelectedDateEnd = dateEnd;
        }
        public SelectList Types { get; private set; }
        public SelectList Results { get; private set; }
        public string? SelectedVin { get; private set; }
        public string? SelectedType { get; private set; }
        public string? SelectedEcu { get; private set; }
        public string? SelectedResult { get; private set; }
        public string? SelectedFile { get; private set; }
        public DateTime? SelectedDateStart { get; private set; }
        public DateTime? SelectedDateEnd { get; private set; }
    }
}
