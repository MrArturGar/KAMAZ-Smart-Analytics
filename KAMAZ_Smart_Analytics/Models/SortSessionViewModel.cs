using System.ComponentModel.DataAnnotations;

namespace KAMAZ_Smart_Analytics.Models
{
    public class SortSessionViewModel
    {
        public SortSessionState TypeSort { get; set; }
        public SortSessionState VinSort { get; set; }
        public SortSessionState DateSort { get; set; }
        public SortSessionState VersionDbSort { get; set; }
        public SortSessionState Current { get; set; }

        public SortSessionViewModel(SortSessionState sortOrder)
        {
            TypeSort = sortOrder == SortSessionState.TypeAsc ? SortSessionState.TypeDesc : SortSessionState.TypeAsc;
            VinSort = sortOrder == SortSessionState.VinAsc ? SortSessionState.VinDesc : SortSessionState.VinAsc;
            DateSort = sortOrder == SortSessionState.DateAsc ? SortSessionState.DateDesc : SortSessionState.DateAsc;
            VersionDbSort = sortOrder == SortSessionState.VersionDbAsc ? SortSessionState.VersionDbDesc : SortSessionState.VersionDbAsc;
            Current = sortOrder;
        }
    }

    public enum SortSessionState
    {
        [Display(Name = "Дата 🠗")] DateDesc,
        [Display(Name = "Дата 🠕")] DateAsc,
        [Display(Name = "Тип 🠗")] TypeDesc,
        [Display(Name = "Тип 🠕")] TypeAsc,
        [Display(Name = "Вин 🠗")] VinDesc,
        [Display(Name = "Вин 🠕")] VinAsc,
        [Display(Name = "Версия базы 🠗")] VersionDbDesc,
        [Display(Name = "Версия базы 🠕")] VersionDbAsc
    }
}
