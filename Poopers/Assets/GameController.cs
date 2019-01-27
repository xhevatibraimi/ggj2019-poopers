using Assets.Scripts;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Button ManButton;
    public Button WomanButton;
    public Text NameText;
    public static string PlayerName = string.Empty;
    public static bool IsMale = false;

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
        ManButton.onClick.AddListener(PlayWithMan);
        ManButton.onClick.AddListener(PlayWithWoman);
    }

    private void PlayWithMan()
    {
        IsMale = true;
        StartGame();
    }

    private void PlayWithWoman()
    {
        StartGame();
    }

    private void StartGame()
    {
        PlayerName = NameText.text;
        GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene(Scenes.InTheMall);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
