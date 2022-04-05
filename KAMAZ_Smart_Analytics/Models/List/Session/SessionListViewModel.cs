namespace KAMAZ_Smart_Analytics.Models.List
{
    public class SessionListViewModel
    {
        public IEnumerable<object> Objects { get; set; }
        public FilterSessionViewModel FilterViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortSessionViewModel SortViewModel { get; set; }
    }
}
