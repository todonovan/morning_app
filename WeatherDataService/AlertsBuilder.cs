using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherDataService.Exceptions;
using WeatherDataService.Models;

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

        public static Alerts Build(ParsedWeatherAPIWrapper data)
        {
            Alerts alertsObject = new Alerts();
            alertsObject.Type = warningTypeMapper[data.Data["type"]];

            DateTimeOffset alertOffset = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(data.Data["date_epoch"]));
            alertsObject.Date = alertOffset.UtcDateTime.ToLocalTime();

            DateTimeOffset expiresOffset = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(data.Data["expires_epoch"]));
            alertsObject.Expires = expiresOffset.UtcDateTime.ToLocalTime();

            alertsObject.Message = data.Data["message"];

            alertsObject.Significance = significanceMapper[data.Data["significance"]];

            return alertsObject;
        }
    }
}
