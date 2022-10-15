using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class RecoMultiplicationScript : MonoBehaviour, ITrackableEventHandler
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
            MultiplicationController.current.ShowCounterToStartGame();
/*            if (!MultiplicationController.current.onGoingGame)
            {
                MultiplicationController.current.ShowCounterToStartGame();
            }
*/
        }
    }
}
