using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
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
        public async Task<TLEModel> GetTLE(int id)
        {
            var response = await MakeGetWebRequest(settings.ApiUrl + id);
            return JsonConvert.DeserializeObject<TLEModel>(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // public async Task<TLECollectionResponse> GetTLECollection(
        //     
        //     )
        // {
        //     var response = await MakeGetWebRequest(settings.ApiUrl);
        //     return response;
        // }

        /// <summary>
        /// Return propagation result with satellite position and velocity using SGP4 or SDP4 algorithms
        /// </summary>
        /// <param name="id">Target satellite id for which propagation is calculated</param>
        /// <param name="date">Target date and time</param>
        public async Task<TLEPropagateResponse> Propagate(int id, DateTime date)
        {
            var response = await MakeGetWebRequest(settings.ApiUrl + id + $"/propagate?date={date.ToLongDateString()}");
            return JsonConvert.DeserializeObject<TLEPropagateResponse>(response);
        }
        
        [ItemCanBeNull]
        private async Task<string> MakeGetWebRequest(string url)
        {
            var www = UnityWebRequest.Get(url);

            try
            {
                await www.SendWebRequest();
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
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
    }
}