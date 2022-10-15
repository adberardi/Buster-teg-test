using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMultiplication : MonoBehaviour
{
    AudioSource soundBtn;
    Text buttonTextSound;
    // Start is called before the first frame update
    void Start()
    {
        buttonTextSound = MultiplicationController.current.GetSoundButton();
    }



    // Update is called once per frame
    void Update()
    {

    }

    // Mutes the sound in the game and change the button's text
    public void MuteSound()
    {
        soundBtn = MultiplicationController.current.GetAudioClip();
        soundBtn.mute = !soundBtn.mute;
        if (soundBtn.mute)
        {
            buttonTextSound.text = "Activar Música";
        }
        else
        {
            buttonTextSound.text = "Desactivar Música";
        }
    }
}
