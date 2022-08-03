using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    AudioSource soundBtn;
    bool statusSound;
    // Start is called before the first frame update
    void Start()
    {
        soundBtn = Controller.controlCharacter.GetAudioClip();
        statusSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MuteSound()
    {
        soundBtn.mute = !soundBtn.mute;
    }
}
