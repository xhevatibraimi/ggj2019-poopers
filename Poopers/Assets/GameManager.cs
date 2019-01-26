using UnityEngine;

namespace Assets
{
    public class GameManager
    {
        private static readonly GameManager _instance = new GameManager();
        public static GameManager Instance { get { return _instance; } }

        public int Score { get; set; }

        public void AddScore(int score)
        {
            Instance.Score += score;
            Debug.Log("score " + Instance.Score);
        }

        public void EndGame()
        {
            Debug.Log("game ended");
        }
    }
}
