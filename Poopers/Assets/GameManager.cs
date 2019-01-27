using System;
using Assets.Scripts;

namespace Assets
{
    public class GameManager
    {
        public EventHandler<ScoreChanged> OnScoreChanged = new System.EventHandler<ScoreChanged>((o, e) => { });
        public EventHandler OnGameOver = new System.EventHandler((o, e) => { });
        public EventHandler OnRestartGame = new EventHandler((o, e) => { });
        public EventHandler<AudioInfo> OnSound = new EventHandler<AudioInfo>((o, e) => { });

        public GameManager()
        {
            OnRestartGame += OnRestartGameImplementation;
        }

        private static readonly GameManager _instance = new GameManager();
        public static GameManager Instance { get { return _instance; } }

        public int Score { get; set; }
        public string NextSceneName { get; set; }
        public bool IsDead { get; internal set; }

        public void AddScore(int score)
        {
            Instance.Score += score;
            if (Instance.Score == 10)
            {
                Instance.Score = 0;
                Instance.ChangeScene(Instance.NextSceneName);
            }
            Instance.OnScoreChanged.Invoke(Instance, new ScoreChanged { Score = Instance.Score });
        }

        private void ChangeScene(string nextSceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
        }

        private void OnRestartGameImplementation(object sender, EventArgs e)
        {
            Instance.Score = 0;
            Instance.IsDead = false;
        }

        public void EndGame()
        {
            Instance.OnGameOver.Invoke(Instance, new EventArgs());
            Instance.IsDead = true;
        }
    }
}
