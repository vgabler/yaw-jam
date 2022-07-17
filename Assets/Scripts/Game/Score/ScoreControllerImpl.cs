using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yaw.Game
{
    public class ScoreControllerImpl : MonoBehaviour, IScoreController
    {
        public int Score { get; set; }
        private void Awake()
        {
            ServiceLocator.Register<IScoreController>(this);
        }

        public void AddScore(int score)
        {
            Score += score;
        }

        public void ResetScore()
        {
            Score = 0;
        }
    }
}