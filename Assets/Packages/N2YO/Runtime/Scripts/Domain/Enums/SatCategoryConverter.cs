using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace N2YO.Runtime.Domain.Enums
{
    public class SatCategoryConverter : JsonConverter<SatCategory>
    {
        public override SatCategory Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                if (reader.TryGetInt32(out int value))
                {
                    return (SatCategory)value;
                }
            }
            else if (reader.TokenType == JsonTokenType.String)
            {
                string stringValue = reader.GetString();

                // Case-insensitive comparison for string values
                if (string.Equals(stringValue, "Amateur radio", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.AmateurRadio;
                if (string.Equals(stringValue, "Beidou Navigation System", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.BeidouNavigationSystem;
                if (string.Equals(stringValue, "Brightest", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Brightest;
                if (string.Equals(stringValue, "Celestis", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Celestis;
                if (string.Equals(stringValue, "Chinese Space Station", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.ChineseSpaceStation;
                if (string.Equals(stringValue, "CubeSats", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.CubeStats;
                if (string.Equals(stringValue, "Disaster monitoring", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.DisasterMonitoring;
                if (string.Equals(stringValue, "Earth resources", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.EarthResources;
                if (string.Equals(stringValue, "Education", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Education;
                if (string.Equals(stringValue, "Engineering", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Engineering;
                if (string.Equals(stringValue, "Experimental", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Experimental;
                if (string.Equals(stringValue, "Flock", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Flock;
                if (string.Equals(stringValue, "Galileo", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Galileo;
                if (string.Equals(stringValue, "Geodetic", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Geodetic;
                if (string.Equals(stringValue, "Geostationary", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Geostationary;
                if (string.Equals(stringValue, "Global Positioning System (GPS) Constellation",
                        StringComparison.OrdinalIgnoreCase))
                    return SatCategory.GlobalPositioningSystemConstellation;
                if (string.Equals(stringValue, "Global Positioning System (GPS) Operational",
                        StringComparison.OrdinalIgnoreCase))
                    return SatCategory.GlobalPositioningSystemOperational;
                if (string.Equals(stringValue, "Globalstar", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Globalstart;
                if (string.Equals(stringValue, "Glonass Constellation", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.GlonassCosntellation;
                if (string.Equals(stringValue, "Glonass Operational", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.GlonassOperational;
                if (string.Equals(stringValue, "GOES", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.GOES;
                if (string.Equals(stringValue, "Gonets", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Gontes;
                if (string.Equals(stringValue, "Gorizont", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Gorizont;
                if (string.Equals(stringValue, "Intelsat", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Instelsat;
                if (string.Equals(stringValue, "Iridium", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Iridium;
                if (string.Equals(stringValue, "IRNSS", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.IRNSS;
                if (string.Equals(stringValue, "ISS", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.ISS;
                if (string.Equals(stringValue, "Kuiper", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Kuiper;
                if (string.Equals(stringValue, "Lemur", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Lemur;
                if (string.Equals(stringValue, "Military", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Military;
                if (string.Equals(stringValue, "Molniya", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Molniya;
                if (string.Equals(stringValue, "Navy Navigation Satellite System", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.NavyNavigationSatelliteSystem;
                if (string.Equals(stringValue, "NOAA", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.NOAA;
                if (string.Equals(stringValue, "O3B Networks", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.O3BNetworks;
                if (string.Equals(stringValue, "OneWeb", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.OneWeb;
                if (string.Equals(stringValue, "Orbcomm", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Orbcomm;
                if (string.Equals(stringValue, "Parus", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Parus;
                if (string.Equals(stringValue, "Qianfan", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Qianfan;
                if (string.Equals(stringValue, "QZSS", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.QZSS;
                if (string.Equals(stringValue, "Radar Calibration", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.RadarCalibration;
                if (string.Equals(stringValue, "Raduga", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Raduga;
                if (string.Equals(stringValue, "Russian LEO Navigation", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.RussianLEONavigation;
                if (string.Equals(stringValue, "Satellite-Based Augmentation System",
                        StringComparison.OrdinalIgnoreCase))
                    return SatCategory.SatelliteBasedAugmentationSystem;
                if (string.Equals(stringValue, "Search & rescue", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.SearchAndRescue;
                if (string.Equals(stringValue, "Space & Earth Science", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.SpaceAndEarthScience;
                if (string.Equals(stringValue, "Starlink", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Starlink;
                if (string.Equals(stringValue, "Strela", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Strela;
                if (string.Equals(stringValue, "Tracking and Data Relay Satellite System",
                        StringComparison.OrdinalIgnoreCase))
                    return SatCategory.TrackingAndDataRelaySatelliteSystem;
                if (string.Equals(stringValue, "Tselina", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Tselina;
                if (string.Equals(stringValue, "Tsikada", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Tsikada;
                if (string.Equals(stringValue, "Tsiklon", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Tsiklon;
                if (string.Equals(stringValue, "TV", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.TV;
                if (string.Equals(stringValue, "Weather", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Weather;
                if (string.Equals(stringValue, "Westford Needles", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.WestfordNeedles;
                if (string.Equals(stringValue, "XM and Sirius", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.XMAndSirius;
                if (string.Equals(stringValue, "Yaogan", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.Yaogan;
                if (string.Equals(stringValue, "All", StringComparison.OrdinalIgnoreCase))
                    return SatCategory.All;
            }

            // If we can't parse it as an int or match a string, throw an exception
            throw new JsonException($"Unable to convert {reader.TokenType} to SatCategory.");
        }

        public override void Write(Utf8JsonWriter writer, SatCategory value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case SatCategory.All:
                    writer.WriteStringValue("All");
                    break;
                case SatCategory.AmateurRadio:
                    writer.WriteStringValue("Amateur radio");
                    break;
                case SatCategory.BeidouNavigationSystem:
                    writer.WriteStringValue("Beidou Navigation System");
                    break;
                case SatCategory.Brightest:
                    writer.WriteStringValue("Brightest");
                    break;
                case SatCategory.Celestis:
                    writer.WriteStringValue("Celestis");
                    break;
                case SatCategory.ChineseSpaceStation:
                    writer.WriteStringValue("Chinese Space Station");
                    break;
                case SatCategory.CubeStats:
                    writer.WriteStringValue("CubeSats");
                    break;
                case SatCategory.DisasterMonitoring:
                    writer.WriteStringValue("Disaster monitoring");
                    break;
                case SatCategory.EarthResources:
                    writer.WriteStringValue("Earth resources");
                    break;
                case SatCategory.Education:
                    writer.WriteStringValue("Education");
                    break;
                case SatCategory.Engineering:
                    writer.WriteStringValue("Engineering");
                    break;
                case SatCategory.Experimental:
                    writer.WriteStringValue("Experimental");
                    break;
                case SatCategory.Flock:
                    writer.WriteStringValue("Flock");
                    break;
                case SatCategory.Galileo:
                    writer.WriteStringValue("Galileo");
                    break;
                case SatCategory.Geodetic:
                    writer.WriteStringValue("Geodetic");
                    break;
                case SatCategory.Geostationary:
                    writer.WriteStringValue("Geostationary");
                    break;
                case SatCategory.GlobalPositioningSystemConstellation:
                    writer.WriteStringValue("Global Positioning System (GPS) Constellation");
                    break;
                case SatCategory.GlobalPositioningSystemOperational:
                    writer.WriteStringValue("Global Positioning System (GPS) Operational");
                    break;
                case SatCategory.Globalstart:
                    writer.WriteStringValue("Globalstar");
                    break;
                case SatCategory.GlonassCosntellation:
                    writer.WriteStringValue("Glonass Constellation");
                    break;
                case SatCategory.GlonassOperational:
                    writer.WriteStringValue("Glonass Operational");
                    break;
                case SatCategory.GOES:
                    writer.WriteStringValue("GOES");
                    break;
                case SatCategory.Gontes:
                    writer.WriteStringValue("Gonets");
                    break;
                case SatCategory.Gorizont:
                    writer.WriteStringValue("Gorizont");
                    break;
                case SatCategory.Instelsat:
                    writer.WriteStringValue("Intelsat");
                    break;
                case SatCategory.Iridium:
                    writer.WriteStringValue("Iridium");
                    break;
                case SatCategory.IRNSS:
                    writer.WriteStringValue("IRNSS");
                    break;
                case SatCategory.ISS:
                    writer.WriteStringValue("ISS");
                    break;
                case SatCategory.Kuiper:
                    writer.WriteStringValue("Kuiper");
                    break;
                case SatCategory.Lemur:
                    writer.WriteStringValue("Lemur");
                    break;
                case SatCategory.Military:
                    writer.WriteStringValue("Military");
                    break;
                case SatCategory.Molniya:
                    writer.WriteStringValue("Molniya");
                    break;
                case SatCategory.NavyNavigationSatelliteSystem:
                    writer.WriteStringValue("Navy Navigation Satellite System");
                    break;
                case SatCategory.NOAA:
                    writer.WriteStringValue("NOAA");
                    break;
                case SatCategory.O3BNetworks:
                    writer.WriteStringValue("O3B Networks");
                    break;
                case SatCategory.OneWeb:
                    writer.WriteStringValue("OneWeb");
                    break;
                case SatCategory.Orbcomm:
                    writer.WriteStringValue("Orbcomm");
                    break;
                case SatCategory.Parus:
                    writer.WriteStringValue("Parus");
                    break;
                case SatCategory.Qianfan:
                    writer.WriteStringValue("Qianfan");
                    break;
                case SatCategory.QZSS:
                    writer.WriteStringValue("QZSS");
                    break;
                case SatCategory.RadarCalibration:
                    writer.WriteStringValue("Radar Calibration");
                    break;
                case SatCategory.Raduga:
                    writer.WriteStringValue("Raduga");
                    break;
                case SatCategory.RussianLEONavigation:
                    writer.WriteStringValue("Russian LEO Navigation");
                    break;
                case SatCategory.SatelliteBasedAugmentationSystem:
                    writer.WriteStringValue("Satellite-Based Augmentation System");
                    break;
                case SatCategory.SearchAndRescue:
                    writer.WriteStringValue("Search & rescue");
                    break;
                case SatCategory.SpaceAndEarthScience:
                    writer.WriteStringValue("Space & Earth Science");
                    break;
                case SatCategory.Starlink:
                    writer.WriteStringValue("Starlink");
                    break;
                case SatCategory.Strela:
                    writer.WriteStringValue("Strela");
                    break;
                case SatCategory.TrackingAndDataRelaySatelliteSystem:
                    writer.WriteStringValue("Tracking and Data Relay Satellite System");
                    break;
                case SatCategory.Tselina:
                    writer.WriteStringValue("Tselina");
                    break;
                case SatCategory.Tsikada:
                    writer.WriteStringValue("Tsikada");
                    break;
                case SatCategory.Tsiklon:
                    writer.WriteStringValue("Tsiklon");
                    break;
                case SatCategory.TV:
                    writer.WriteStringValue("TV");
                    break;
                case SatCategory.Weather:
                    writer.WriteStringValue("Weather");
                    break;
                case SatCategory.WestfordNeedles:
                    writer.WriteStringValue("Westford Needles");
                    break;
                case SatCategory.XMAndSirius:
                    writer.WriteStringValue("XM and Sirius");
                    break;
                case SatCategory.Yaogan:
                    writer.WriteStringValue("Yaogan");
                    break;
                default:
                    throw new JsonException($"Unknown SatCategory value: {value}");
            }
        }
    }
}