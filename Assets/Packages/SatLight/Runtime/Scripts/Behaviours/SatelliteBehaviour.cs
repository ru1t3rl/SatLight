using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SatLight.Models;
using UnityEngine;

public class SatelliteBehaviour : MonoBehaviour
{
    [SerializeField]
    private SatAbove satInfo;

    [SerializeField]
    private bool updateLocation;

    [SerializeField]
    private float updateTimeout;

    private Coroutine _informationUpdateRoutine = null;
    private WaitForSeconds _timeout = null;

    private IEnumerator UpdateInfo()
    {
        if (!updateLocation)
        {
            yield return _timeout;
            yield break;
        }
        
        _timeout ??= new WaitForSeconds(updateTimeout);

        // TODO: Make request for new location or other information

        yield return _timeout;
    }

    public void SetSatelliteIno(SatAbove satInfo)
    {
        this.satInfo = satInfo;

        if (_informationUpdateRoutine != null)
        {
            StopCoroutine(_informationUpdateRoutine);
            _informationUpdateRoutine = StartCoroutine(UpdateInfo());
        } 
    }
}