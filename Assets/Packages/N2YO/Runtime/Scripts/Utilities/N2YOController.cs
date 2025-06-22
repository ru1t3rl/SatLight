using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using N2YO.Runtime.Domain;
using Newtonsoft.Json;
using SatLight.Models.Responses;
using Ru1t3rl.Utilities;
using UnityEngine;
using UnityEngine.Networking;
using Logger = Ru1t3rl.Utilities.Logger;

namespace SatLight.Utilities
{
    public class N2YOController : UnitySingleton<N2YOController>
    {
        [SerializeField] private N2YOSettings settings;

        /// <summary>
        /// Retrieve the Two Line Elements (TLE) for a satellite identified by NORAD id
        /// </summary>
        /// <param name="id">NORAD id</param>
        public async Task<TLEReponse> GetTLE(int id)
        {
            var response = await MakeGetWebRequest(settings.ApiUrl + $"tle/{id}");
            return JsonConvert.DeserializeObject<TLEReponse>(response);
        }

        /// <summary>
        /// Retrieve the future positions of any satellite as groundtrack (latitude, longitude) to display orbits on maps.
        /// Also return the satellite's azimuth and elevation with respect to the observer location.
        /// Each element in the response array is one second of calculation.
        /// First element is calculated for current UTC time.
        /// </summary>
        /// <param name="id">NORAD id</param>
        /// <param name="observerLat">Observer's latitude (decimal degrees format)</param>
        /// <param name="observerLng">Observer's longitude (decimal degrees format)</param>
        /// <param name="observerAlt">Observer's altitude above sea level in meters</param>
        /// <param name="seconds">Number of future positions to return. Each second is a position. Limit 300 seconds</param>
        public async Task<PositionResponse> GetSatellitePositions(
            int id,
            double observerLat,
            double observerLng,
            double observerAlt,
            int seconds)
        {
            var response = await MakeGetWebRequest(settings.ApiUrl +
                                                   $"positions/{id}/{observerLat}/{observerLng}/{observerAlt}/{seconds}/");
            return JsonConvert.DeserializeObject<PositionResponse>(response);
        }

        /// <summary>
        /// Get predicted visual passes for any satellite relative to a location on Earth.
        /// A "visual pass" is a pass that should be optically visible on the entire (or partial) duration of crossing the sky.
        /// For that to happen, the satellite must be above the horizon, illuminated by Sun (not in Earth shadow),
        /// and the sky dark enough to allow visual satellite observation.
        /// </summary>
        /// <param name="id">NORAD id</param>
        /// <param name="observerLat">Observer's latitude (decimal degrees format)</param>
        /// <param name="observerLng">Observer's longitude (decimal degrees format)</param>
        /// <param name="observerAlt">Observer's altitude above sea level in meters</param>
        /// <param name="days">Number of days of prediction (max 10)</param>
        /// <param name="minVisibility">Minimum number of seconds the satellite should be considered optically visible during the pass to be returned as result</param>
        public async Task<VisualPassesResponse> GetVisualPasses(
            int id,
            float observerLat,
            float observerLng,
            float observerAlt,
            int days,
            int minVisibility
        )
        {
            var response = await MakeGetWebRequest(settings.ApiUrl +
                                                   $"visualpasses/{id}/{observerLat}/{observerLng}/{observerAlt}/{days}/{minVisibility}");
            return JsonConvert.DeserializeObject<VisualPassesResponse>(response);
        }

        /// <summary>
        /// The "radio passes" are similar to "visual passes",
        /// the only difference being the requirement for the objects to be optically visible for observers.
        /// This function is useful mainly for predicting satellite passes to be used for radio communications.
        /// The quality of the pass depends essentially on the highest elevation value during the pass,
        /// which is one of the input parameters.
        /// </summary>
        /// <param name="id">NORAD id</param>
        /// <param name="observerLat">Observer's latitude (decimal degrees format)</param>
        /// <param name="observerLng">Observer's longitude (decimal degrees format)</param>
        /// <param name="observerAlt">Observer's altitude above sea level in meters</param>
        /// <param name="days">Number of days of prediction (max 10)</param>
        /// <param name="minElevation">The minimum elevation acceptable for the highest altitude point of the pass (degrees)</param>
        public async Task<RadioPassesResponse> GetRadioPasses(
            int id,
            float observerLat,
            float observerLng,
            float observerAlt,
            int days,
            int minElevation
        )
        {
            var response = await MakeGetWebRequest(settings.ApiUrl +
                                                   $"radiopasses/{id}/{observerLat}/{observerLng}/{observerAlt}/{days}/{minElevation}");
            return JsonConvert.DeserializeObject<RadioPassesResponse>(response);
        }

        /// <summary>
        /// The "above" function will return all objects within a given search radius above observer's location.
        /// The radius (θ), expressed in degrees, is measured relative to the point in the sky directly above an observer (azimuth).
        ///
        /// Please use this function responsible as there is a lot of CPU needed in order to calculate exact positions for all satellites in the sky.
        /// The function will return altitude, latitude and longitude of satellites footprints to be displayed on a map,
        /// and some minimal information to identify the object.
        /// </summary>
        /// <param name="observerLat">Observer's latitude (decimal degrees format)</param>
        /// <param name="observerLng">Observer's longitude (decimal degrees format)</param>
        /// <param name="observerAlt">Observer's altitude above sea level in meters</param>
        /// <param name="searchRadius">Search radius (0-90)</param>
        /// <param name="categoryId">Category id (see table). Use 0 for all categories</param>
        public async Task<AboveResponse> WhatsAbove(
            double observerLat,
            double observerLng,
            double observerAlt,
            int searchRadius,
            int categoryId
        )
        {
            var response = await MakeGetWebRequest(settings.ApiUrl +
                                                   $"above/{observerLat}/{observerLng}/{observerAlt}/{searchRadius}/{categoryId}");

            return JsonConvert.DeserializeObject<AboveResponse>(response);
        }

        [ItemCanBeNull]
        private async Task<string> MakeGetWebRequest(string url)
        {
            var www = UnityWebRequest.Get(AppendAPIKey(url));

            try
            {
                var request = www.SendWebRequest();
                while (!request.isDone)
                {
                    await Task.Yield();
                    Logger.Log($"Waiting for request to complete... ({request.progress}%)");
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
            }

            if (www.result != UnityWebRequest.Result.Success)
            {
                Logger.LogError(www.error);
                return null;
            }

            if (www.downloadHandler.text.Contains("null"))
            {
                Logger.LogError($"Received null response. ({url})");
                return null;
            }

            if (www.downloadHandler.text.Contains("error"))
            {
                Logger.LogError($"Received error response ({url}).\r\n{www.downloadHandler.text}");
                return null;
            } 

            return www.downloadHandler.text;
        }

        private string AppendAPIKey(string url)
        {
            return url += $"&apiKey={settings.ApiKey}";
        }
    }
}