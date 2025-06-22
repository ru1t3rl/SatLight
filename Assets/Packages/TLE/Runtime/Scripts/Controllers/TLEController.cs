using System;
using System.Text.Json;
using System.Threading.Tasks;
using JetBrains.Annotations;
using OneOf;
using OneOf.Types;
using Ru1t3rl.Utilities;
using TLE.Runtime.Domain;
using TLE.Runtime.Domain.Responses;
using UnityEngine;
using UnityEngine.Networking;
using Logger = Ru1t3rl.Utilities.Logger;

namespace TLE.Runtime.Controllers
{
    public class TLEController : UnitySingleton<TLEController>
    {
        [SerializeField] private TLESettings settings;

        /// <summary>
        /// Return single TleModel for requested satellite id
        /// </summary>
        /// <param name="id">Satellite Id (i.e. 43638)</param>
        public async Task<OneOf<TLEModel, None>> GetTLE(int id)
        {
            var response = await MakeGetWebRequest(settings.ApiUrl + id);
            
            
            return response.Match<OneOf<TLEModel, None>>(
                model => JsonSerializer.Deserialize<TLEModel>(model),
                none => none
            );
        }

        /// <summary>
        /// Return propagation result with satellite position and velocity using SGP4 or SDP4 algorithms
        /// </summary>
        /// <param name="id">Target satellite id for which propagation is calculated</param>
        /// <param name="date">Target date and time</param>
        public async Task<OneOf<TLEPropagateResponse, None>> Propagate(int id, DateTime date)
        {
            var response = await MakeGetWebRequest(settings.ApiUrl + id + $"/propagate?date={date.ToLongDateString()}");
            return response.Match<OneOf<TLEPropagateResponse, None>>(
                model => JsonSerializer.Deserialize<TLEPropagateResponse>(model),
                none => none
            );
        }

        [ItemCanBeNull]
        private async Task<OneOf<string, None>> MakeGetWebRequest(string url)
        {
            var www = UnityWebRequest.Get(url);
            www.SetRequestHeader("Accept", "application/json");
            www.SetRequestHeader("User-Agent", "UnityClient/1.0");

            try
            {
                var request = www.SendWebRequest();
                while (!request.isDone)
                {
                    await Task.Yield();
                    Logger.Log($"Waiting for request to complete... ({request.progress * 100f}%)");
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
            }

            if (www.result != UnityWebRequest.Result.Success)
            {
                Logger.LogError(www.error);
                return new None();
            }

            if (www.downloadHandler.text.Contains("null"))
            {
                Logger.LogError($"Received null response. ({url})");
                return new None();
            }

            if (www.downloadHandler.text.Contains("error"))
            {
                Logger.LogError($"Received error response ({url}).\r\n{www.downloadHandler.text}");
                return new None();
            }

            return www.downloadHandler.text;
        }
    }
}