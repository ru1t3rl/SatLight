using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using SatLight.Runtime.Domain;
using SatLight.Runtime.Domain.Common;
using UnityEngine;

namespace SatLight.Runtime.Behaviours
{
    [RequireComponent(typeof(SatelliteData))]
    public abstract class SatelliteDataUpdaterBase : MonoBehaviour
    {
        [Range(1, 300), SerializeField, Tooltip("Update rate in seconds!")]
        protected int updateRate;

        protected SatelliteData _data;
        private CancellationTokenSource _cancellationTokenSource = null;

        [CanBeNull, SerializeField,
         Tooltip("The user settings are used to share the user location between the satellite objects.")]
        protected UserSettings userSettings;

        protected virtual void Awake()
        {
            _data = GetComponent<SatelliteData>();
        }

        protected virtual void OnEnable()
        {
            if (_cancellationTokenSource == null)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                _ = UpdateLoop(_cancellationTokenSource.Token);
            }
        }

        protected virtual void OnDisable()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
            }
        }


        private async Task UpdateLoop(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(updateRate, cancellationToken);
                if (_data.SatInfo == null)
                {
                    continue;
                }
                
                await UpdateInfo(cancellationToken);
            }
        }
        
        protected abstract Task UpdateInfo(CancellationToken cancellationToken);
    }
}