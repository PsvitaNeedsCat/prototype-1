using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornScript : MonoBehaviour
{
    public AudioSource[] hornSounds;


    public enum Sounds
    { 
        SQUEAK = 0
    }

    public Sounds hornIndex = Sounds.SQUEAK;

    public void PlayHorn()
    {
        if (!hornSounds[(int)hornIndex].isPlaying)
            hornSounds[(int)hornIndex].Play();
    }

    public void StopHorn()
    {
        if (hornSounds[(int)hornIndex].isPlaying)
            hornSounds[(int)hornIndex].Stop();
    }

    public void ChangeHorn(Sounds _newSound)
    {
        int oldSound = (int)hornIndex;
        hornIndex = _newSound;

        // If horn is playing, swap sound
        if (hornSounds[oldSound].isPlaying)
        {
            hornSounds[oldSound].Stop();
            hornSounds[(int)hornIndex].Play();
        }
    }
}
