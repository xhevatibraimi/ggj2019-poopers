using Assets;
using System;
using UnityEngine;

public class ObstacleSoundController : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameManager.Instance.OnGameOver += OnGameOver;
    }

    private void OnGameOver(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsDead)
            audioSource.Play();
    }
}
