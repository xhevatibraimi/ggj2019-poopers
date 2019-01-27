using Assets.Scripts;
using UnityEngine;

namespace Assets
{
    public class GameManager
    {
        public System.EventHandler<ScoreChanged> ScoreChanged = new System.EventHandler<ScoreChanged>((o, e) => { });
        private static readonly GameManager _instance = new GameManager();
        public static GameManager Instance { get { return _instance; } }

        public int Score { get; set; }

        public void AddScore(int score)
        {
            Instance.Score += score;
            ScoreChanged.Invoke(Instance, new ScoreChanged { Score = Instance.Score });
        }

        public void EndGame()
        {
            Debug.Log("game ended");
        }
    }
}
