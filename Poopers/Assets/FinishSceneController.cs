using System;
using Assets;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishSceneController : MonoBehaviour
{
    public Button ResetGameButton;

    void Start()
    {
        ResetGameButton.onClick.AddListener(ResetGame);
        var players = GameObject.FindGameObjectsWithTag(Tags.Player);
        foreach (var p in players)
        {
            p.SetActive(false);
        }
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(Scenes.Intro);
    }
}
