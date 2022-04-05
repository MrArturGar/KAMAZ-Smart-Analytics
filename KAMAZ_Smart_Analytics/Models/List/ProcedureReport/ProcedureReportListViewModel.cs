namespace KAMAZ_Smart_Analytics.Models.List
{
    public class ProcedureReportListViewModel
    {
        public IEnumerable<object> Objects { get; set; }
        public FilterProcedureReportViewModel FilterViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortProcedureReportViewModel SortViewModel { get; set; }
    }
}
