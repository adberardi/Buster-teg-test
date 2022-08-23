using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SumaRecoScript : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;
    Vector3 currentPosition;

    void Start()
    {
        currentPosition = new Vector3(0,0,0);
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    // Method that indicates when the App Detectes the image target.
    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            if(SumaController.controlCharacter.onGoingGame)
            {
                //SumaSpawnerStart.current.CreateObjectStart();
                SumaSpawnerStart.current.SetCurrentPosition(currentPosition);
            }
            else
            {
                //SumaController.controlCharacter.ShowCounterToStartGame();
                SumaController.controlCharacter.CreateObject();
            }
        }
        else
        {
            if (SumaController.controlCharacter.onGoingGame)
            {
                SumaSpawnerStart.current.start = false;
                currentPosition = SumaSpawnerStart.current.GetCharacterPosition();
            }

        }
    }
}
