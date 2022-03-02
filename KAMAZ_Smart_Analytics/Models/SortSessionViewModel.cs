namespace KAMAZ_Smart_Analytics.Models
{
    public class SortSessionViewModel
    {
        public SortSessionState DateSort { get; set; }
        public SortSessionState VersionDbSort { get; set; }
        public SortSessionState Current { get; set; }

        public SortSessionViewModel(SortSessionState sortOrder)
        {
            DateSort = sortOrder == SortSessionState.DateAsc ? SortSessionState.DateDesc : SortSessionState.DateAsc;
            VersionDbSort = sortOrder == SortSessionState.VersionDbAsc ? SortSessionState.VersionDbDesc : SortSessionState.VersionDbAsc;
            Current = sortOrder;
        }
    }

    public enum SortSessionState
    {
        DateAsc,    // по имени по возрастанию
        DateDesc,   // по имени по убыванию
        VersionDbAsc, // по возрасту по возрастанию
        VersionDbDesc    // по возрасту по убыванию
    }
}
