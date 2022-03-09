using KAMAZ_Smart_Analytics.Models.List;

namespace KAMAZ_Smart_Analytics.Models
{
    public class ListSessionViewModel
    {

        public IEnumerable<Session> Sessions { get; set;}
        public PageViewModel PageViewModel { get; set; }
        public FilterSessionViewModel FilterSessionViewModel { get; set; }
        public SortSessionViewModel SortSessionViewModel { get; set; }


    }
}
