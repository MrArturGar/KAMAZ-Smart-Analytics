﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace KAMAZ_Smart_Analytics.Models.List
{
    public class FilterSessionViewModel
    {
        public FilterSessionViewModel(List<string> VersionDBs, string vin, string versionDb)
        {
            Versions = new SelectList(VersionDBs, versionDb);
            SelectedVin = vin;
            SelectedVersionDB = versionDb;
        }
        public SelectList Versions { get; private set; }
        public string? SelectedVin { get; private set; }
        public string? SelectedVersionDB { get; private set; }
    }
}