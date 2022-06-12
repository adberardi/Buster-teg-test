using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class RecoScript : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;

    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            //muovo la barca
            // Debug.Log("muovo");
            // Spawn.current.start = true;
            //float r = Random.Range(0, 1f);
            Spawn.current.StartGenerator(0);
        }
        else
        {
            // rimetto la barca al suo posto
            Spawn.current.start = false;
            //Spawn.current.DesactivatedCharacter();
            //  Debug.Log("fermo");
        }
    }
}
