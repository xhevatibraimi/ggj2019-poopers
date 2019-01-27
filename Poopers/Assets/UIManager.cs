using Assets;
using Assets.Scripts;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text ScoreText;
    public Text GameOverText;
    public Button RestartButton;
    public Sprite RestartButtonSprite;

    void Start()
    {
        GameManager.Instance.OnScoreChanged += ChanfgeScore;
        GameManager.Instance.OnGameOver += OnGameOver;
        RestartButton.onClick.AddListener(RestartGame);
        RestartButton.image.sprite = RestartButtonSprite;
        RestartButton.image.color = new Color(GameOverText.color.r, GameOverText.color.g, GameOverText.color.b, 0);
    }

    private void OnGameOver(object sender, EventArgs e)
    {
        GameOverText.text = "GAME OVER";
        RestartButton.image.sprite = RestartButtonSprite;
        RestartButton.image.color = new Color(GameOverText.color.r, GameOverText.color.g, GameOverText.color.b, 255);
    }

    private void ChanfgeScore(object sender, ScoreChanged e)
    {
        ScoreText.text = e.Score.ToString();
    }

    private void RestartGame()
    {
        RestartButton.image.sprite = null;
        GameOverText.text = string.Empty;
        RestartButton.image.color = new Color(GameOverText.color.r, GameOverText.color.g, GameOverText.color.b, 0);
        ScoreText.text = "0";
        GameManager.Instance.OnRestartGame.Invoke(this, new EventArgs());
    }
}
