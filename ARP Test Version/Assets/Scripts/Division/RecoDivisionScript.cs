using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class RecoDivisionScript : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;
    Vector3 currentPosition;

    void Start()
    {
        currentPosition = new Vector3(0, 0, 0);
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

            if (DivisionController.current.onGoingGame)
            {
                DivisionController.current.SetBoatPosition(currentPosition);
            }
            else
            {
                DivisionController.current.ShowCounterToStartGame();
            }
        }
        else
        {
            currentPosition = DivisionController.current.GetBoatPosition();
        }
    }
}
