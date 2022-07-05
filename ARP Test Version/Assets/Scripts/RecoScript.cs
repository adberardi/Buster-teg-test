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
            if(Controller.controlCharacter.GetOnGoingGame())
            {
                if(Controller.controlCharacter.startRigth)
                    SpawnerStart.current.CreateObjectStart();

                if (Controller.controlCharacter.startLeft)
                    SpawnerEnd.current.CreateObjectEnd();
            }
        }
        else
        {
            SpawnerStart.current.start = false;
            SpawnerEnd.current.start = false;
        }
    }
}
