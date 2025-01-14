﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class MusicFadeIn : MonoBehaviour
{
    private AudioSource musicSource;
    public float overSeconds = 2.0f;
    public float finalVolume = 1.0f;

    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();
        
    }

    private void Update()
    {
        if (musicSource.volume < finalVolume) { musicSource.volume += Time.deltaTime / overSeconds; }
    }
}
