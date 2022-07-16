using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yaw.Data
{
    /// <summary>
    /// Dados básicos de um Summon
    /// </summary>
    [System.Serializable]
    public struct SummonData
    {
        public int Health;
        public int Attack;
        public int Speed;
        public int Team;
        public bool IsFromLeft => Team % 2 == 0;
    }
}
