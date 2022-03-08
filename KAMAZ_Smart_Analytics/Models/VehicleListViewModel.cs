namespace KAMAZ_Smart_Analytics.Models
{
    public class VehicleListViewModel
    {
        public IEnumerable<object> Objects { get; set; }
        public FilterVehicleViewModel FilterViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortVehicleViewModel SortViewModel { get; set; }
    }
}
