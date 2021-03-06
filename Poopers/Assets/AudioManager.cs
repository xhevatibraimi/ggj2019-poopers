﻿using Assets;
using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private bool created = false;

    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        GameManager.Instance.OnSound += PlaySound;
    }

    private void PlaySound(object sender, AudioInfo audioInfo)
    {
        //audioSource.clip = AudioClip.Create()
        //audioSource.Play()
    }
}
