namespace Yaw.Game
{
    public interface IScoreController
    {
        public int Score { get; }

        public void AddScore(int score);

        public void ResetScore();
    }
}