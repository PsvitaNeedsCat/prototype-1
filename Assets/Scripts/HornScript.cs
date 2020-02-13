using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornScript : MonoBehaviour
{
    public AudioClip[] hornSounds;
    AudioSource m_audioSource;

    public enum Sounds
    { 
        SQUEAK = 0,
        CLASSIC
    }

    public Sounds hornIndex = Sounds.SQUEAK;

    private void Awake()
    {
        // Set audio source
        m_audioSource = GetComponent<AudioSource>();

        // Set audio clip
        m_audioSource.clip = hornSounds[(int)hornIndex];
    }

    public void PlayHorn()
    {
        if (!m_audioSource.isPlaying)
            m_audioSource.Play();
    }

    public void StopHorn()
    {
        if (m_audioSource.isPlaying)
            m_audioSource.Stop();
    }

    public void ChangeHorn(Sounds _newSound)
    {
        hornIndex = _newSound;

        // If horn is playing, swap sound
        if (m_audioSource.isPlaying)
        {
            m_audioSource.Stop();
            m_audioSource.clip = hornSounds[(int)_newSound];
            m_audioSource.Play();
        }
        else
        {
            m_audioSource.clip = hornSounds[(int)_newSound];
        }
    }
}
