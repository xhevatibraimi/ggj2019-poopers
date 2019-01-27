using System;

namespace Assets.Scripts
{
    public class ScoreChanged : EventArgs
    {
        public int Score { get; set; }
    }
}
