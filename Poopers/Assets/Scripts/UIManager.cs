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
    public Image YouPoopedImage;
    static bool created = false;

    void Awake()
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
        GameManager.Instance.OnScoreChanged += ChanfgeScore;
        GameManager.Instance.OnGameOver += OnGameOver;
        RestartButton.onClick.AddListener(RestartGame);
        RestartButton.image.sprite = RestartButtonSprite;
        RestartButton.image.color = new Color(RestartButton.image.color.r, RestartButton.image.color.g, RestartButton.image.color.b, 0);
        YouPoopedImage.color = new Color(YouPoopedImage.color.r, YouPoopedImage.color.g, YouPoopedImage.color.b, 0);
    }

    private void OnGameOver(object sender, EventArgs e)
    {
        GameOverText.text = "GAME OVER";
        RestartButton.image.sprite = RestartButtonSprite;
        RestartButton.image.color = new Color(RestartButton.image.color.r, RestartButton.image.color.g, RestartButton.image.color.b, 255);
        YouPoopedImage.color = new Color(YouPoopedImage.color.r, YouPoopedImage.color.g, YouPoopedImage.color.b, 255);
    }

    private void ChanfgeScore(object sender, ScoreChanged e)
    {
        ScoreText.text = e.Score.ToString();
    }

    private void RestartGame()
    {
        RestartButton.image.sprite = null;
        GameOverText.text = string.Empty;
        RestartButton.image.color = new Color(RestartButton.image.color.r, RestartButton.image.color.g, RestartButton.image.color.b, 0);
        YouPoopedImage.color = new Color(YouPoopedImage.color.r, YouPoopedImage.color.g, YouPoopedImage.color.b, 0);
        ScoreText.text = "0";
        GameManager.Instance.OnRestartGame.Invoke(this, new EventArgs());
    }
}
