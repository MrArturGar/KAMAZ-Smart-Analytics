namespace KAMAZ_Smart_Analytics.Models
{
    public class IndexViewModel
    {
        public IEnumerable<object> Objects { get; set; }
        public FilterSessionViewModel FilterViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortSessionViewModel SortViewModel { get; set; }
    }
}
