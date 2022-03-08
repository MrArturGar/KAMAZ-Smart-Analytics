using System.ComponentModel.DataAnnotations;

namespace KAMAZ_Smart_Analytics.Models
{
    public class SortVehicleViewModel
    {
        public SortVehicleState VinSort { get; set; }
        public SortVehicleState Design_numberSort { get; set; }
        public SortVehicleState IccidSort { get; set; }
        public SortVehicleState IccidcSort { get; set; }
        public SortVehicleState ImeiSort { get; set; }
        public SortVehicleState TypeSort { get; set; }
        public SortVehicleState Current { get; set; }

        public SortVehicleViewModel(SortVehicleState sortOrder)
        {
            VinSort = sortOrder == SortVehicleState.VinAsc ? SortVehicleState.VinDesc : SortVehicleState.VinAsc;
            Design_numberSort = sortOrder == SortVehicleState.Design_numberAsc ? SortVehicleState.Design_numberDesc : SortVehicleState.Design_numberAsc;
            IccidSort = sortOrder == SortVehicleState.IccidAsc ? SortVehicleState.IccidDesc : SortVehicleState.IccidAsc;
            IccidcSort = sortOrder == SortVehicleState.IccidcAsc ? SortVehicleState.IccidcDesc : SortVehicleState.IccidcAsc;
            ImeiSort = sortOrder == SortVehicleState.ImeiAsc ? SortVehicleState.ImeiDesc : SortVehicleState.ImeiAsc;
            TypeSort = sortOrder == SortVehicleState.TypeAsc ? SortVehicleState.TypeDesc : SortVehicleState.TypeAsc;
            Current = sortOrder;
        }
    }

    public enum SortVehicleState
    {
        [Display(Name = "Вин 🠗")] VinDesc,
        [Display(Name = "Вин 🠕")] VinAsc,
        [Display(Name = "Комплектация 🠗")] Design_numberDesc,
        [Display(Name = "Комплектация 🠕")] Design_numberAsc,
        [Display(Name = "ICCID 🠗")] IccidDesc,
        [Display(Name = "ICCID 🠕")] IccidAsc,
        [Display(Name = "ICCIDC 🠗")] IccidcDesc,
        [Display(Name = "ICCIDC 🠕")] IccidcAsc,
        [Display(Name = "IMEI 🠗")] ImeiDesc,
        [Display(Name = "IMEI 🠕")] ImeiAsc,
        [Display(Name = "Тип 🠗")] TypeDesc,
        [Display(Name = "Тип 🠕")] TypeAsc
    }
}
