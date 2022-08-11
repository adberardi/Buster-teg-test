using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class RecoScript : MonoBehaviour, ITrackableEventHandler
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
            if(Controller.controlCharacter.onGoingGame)
            {
                if (Controller.controlCharacter.startRigth)
                    //SpawnerStart.current.CreateObjectStart();
                    SpawnerStart.current.SetCurrentPosition(currentPosition);

                if (Controller.controlCharacter.startLeft)
                    //SpawnerEnd.current.CreateObjectEnd();
                    SpawnerEnd.current.SetCurrentPosition(currentPosition);
            }
            else
            {
                Controller.controlCharacter.ShowCounterToStartGame();
            }
        }
        else
        {
            if (Controller.controlCharacter.onGoingGame)
            {
                if (Controller.controlCharacter.startRigth)
                {
                    SpawnerStart.current.start = false;
                    currentPosition = SpawnerStart.current.GetCharacterPosition();
                }

                else
                {
                    SpawnerEnd.current.start = false;
                    currentPosition = SpawnerEnd.current.GetCharacterPosition();
                }
            }

        }
    }
}
