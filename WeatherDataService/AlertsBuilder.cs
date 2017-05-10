using System;
using System.Collections.Generic;
using WeatherDataService.Models;
using Newtonsoft.Json.Linq;

namespace WeatherDataService
{
    public enum WarningType
    {
        Hurricane,
        TornadoWarning,
        TornadoWatch,
        SevereTStormWarning,
        SevereTStormWatch,
        WinterWeatherAdv,
        FloodWarning,
        FloodWatch,
        HighWindAdv,
        SevereWeatherStatement,
        HeatAdv,
        DenseFog,
        SpecialWeatherStatement,
        FireWeatherAdv,
        VolcanicActivity,
        HurricaneWindWarning,
        RecordSet,
        PublicReports,
        PublicInformationStatement
    }

    public enum WarningSignificance
    {
        Warning,
        Watch,
        Advisory,
        Statement,
        Forecast,
        Outlook,
        Synopsis
    }

    public static class AlertsBuilder
    {
        private static Dictionary<string, WarningType> warningTypeMapper = new Dictionary<string, WarningType>
        {
            ["HUR"] = WarningType.Hurricane,
            ["TOR"] = WarningType.TornadoWarning,
            ["TOW"] = WarningType.TornadoWatch,
            ["WRN"] = WarningType.SevereTStormWarning,
            ["SEW"] = WarningType.SevereTStormWatch,
            ["WIN"] = WarningType.WinterWeatherAdv,
            ["FLO"] = WarningType.FloodWarning,
            ["WAT"] = WarningType.FloodWatch,
            ["WND"] = WarningType.HighWindAdv,
            ["SVR"] = WarningType.SevereWeatherStatement,
            ["HEA"] = WarningType.HeatAdv,
            ["FOG"] = WarningType.DenseFog,
            ["SPE"] = WarningType.SpecialWeatherStatement,
            ["FIR"] = WarningType.FireWeatherAdv,
            ["VOL"] = WarningType.VolcanicActivity,
            ["HWW"] = WarningType.HurricaneWindWarning,
            ["REC"] = WarningType.RecordSet,
            ["REP"] = WarningType.PublicReports,
            ["PUB"] = WarningType.PublicInformationStatement
        };

        private static Dictionary<string, WarningSignificance> significanceMapper = new Dictionary<string, WarningSignificance>
        {
            ["W"] = WarningSignificance.Warning,
            ["A"] = WarningSignificance.Watch,
            ["Y"] = WarningSignificance.Advisory,
            ["S"] = WarningSignificance.Statement,
            ["F"] = WarningSignificance.Forecast,
            ["O"] = WarningSignificance.Outlook,
            ["N"] = WarningSignificance.Synopsis
        };

        public static List<Alert> Build(string rawJson)
        {
            List<Alert> alerts = new List<Alert>();
            JObject json = JObject.Parse(rawJson);

            JArray alertsArray = (JArray)json["alerts"];

            foreach (var a in alertsArray)
            {
                Alert alert = new Alert();
                alert.Type = warningTypeMapper[(string)a["type"]];
                alert.Message = (string)a["message"];
                alert.Significance = significanceMapper[(string)a["significance"]];

                DateTimeOffset alertOffset = DateTimeOffset.FromUnixTimeMilliseconds((long)a["date_epoch"]);
                alert.Date = alertOffset.UtcDateTime.ToLocalTime();

                DateTimeOffset expiresOffset = DateTimeOffset.FromUnixTimeMilliseconds((long)a["expires_epoch"]);
                alert.Expires = expiresOffset.UtcDateTime.ToLocalTime();

                alerts.Add(alert);
            }

            return alerts;
        }
    }
}
