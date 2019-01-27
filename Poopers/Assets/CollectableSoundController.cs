using Assets;
using System;
using UnityEngine;

public class CollectableSoundController : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameManager.Instance.OnScoreChanged += OnScoreChanged;
    }

    private void OnScoreChanged(object sender, EventArgs e)
    {
        audioSource.Play();
    }
}
