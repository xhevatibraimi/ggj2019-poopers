using Assets;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text ScoreText;

    void Start()
    {
        GameManager.Instance.ScoreChanged += ChanfgeScore;
    }

    private void ChanfgeScore(object sender, ScoreChanged e)
    {
        ScoreText.text = e.Score.ToString();
    }
}
