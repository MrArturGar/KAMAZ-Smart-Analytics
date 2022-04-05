using System.ComponentModel.DataAnnotations;

namespace KAMAZ_Smart_Analytics.Models.List
{
    public class SortProcedureReportViewModel
    {
        public SortProcedureReportState DateSort { get; set; }
        public SortProcedureReportState TypeSort { get; set; }
        public SortProcedureReportState VinSort { get; set; }
        public SortProcedureReportState EcuSort { get; set; }
        public SortProcedureReportState ResultSort { get; set; }
        public SortProcedureReportState FileSort { get; set; }
        public SortProcedureReportState Current { get; set; }

        public SortProcedureReportViewModel(SortProcedureReportState sortOrder)
        {
            DateSort = sortOrder == SortProcedureReportState.DateAsc ? SortProcedureReportState.DateDesc : SortProcedureReportState.DateAsc;
            TypeSort = sortOrder == SortProcedureReportState.TypeAsc ? SortProcedureReportState.TypeDesc : SortProcedureReportState.TypeAsc;
            VinSort = sortOrder == SortProcedureReportState.VinAsc ? SortProcedureReportState.VinDesc : SortProcedureReportState.VinAsc;
            EcuSort = sortOrder == SortProcedureReportState.EcuAsc ? SortProcedureReportState.EcuDesc : SortProcedureReportState.EcuAsc;
            ResultSort = sortOrder == SortProcedureReportState.ResultAsc ? SortProcedureReportState.ResultDesc : SortProcedureReportState.ResultAsc;
            FileSort = sortOrder == SortProcedureReportState.FileAsc ? SortProcedureReportState.FileDesc : SortProcedureReportState.FileAsc;
            Current = sortOrder;
        }
    }

    public enum SortProcedureReportState
    {
        [Display(Name = "Дата 🠗")] DateDesc,
        [Display(Name = "Дата 🠕")] DateAsc,
        [Display(Name = "Тип 🠗")] TypeDesc,
        [Display(Name = "Тип 🠕")] TypeAsc,
        [Display(Name = "Вин 🠗")] VinDesc,
        [Display(Name = "Вин 🠕")] VinAsc,
        [Display(Name = "ЭБУ 🠗")] EcuDesc,
        [Display(Name = "ЭБУ 🠕")] EcuAsc,
        [Display(Name = "Результат 🠗")] ResultDesc,
        [Display(Name = "Результат 🠕")] ResultAsc,
        [Display(Name = "Файл 🠗")] FileDesc,
        [Display(Name = "Файл 🠕")] FileAsc
    }
}
