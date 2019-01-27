using System;

namespace Assets.Scripts
{
    public class ScoreChanged : EventArgs
    {
        public float Score { get; set; }
    }
}
