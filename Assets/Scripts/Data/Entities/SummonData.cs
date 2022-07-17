using System;
using UnityEngine;

namespace Yaw.Data
{
    /// <summary>
    /// Dados básicos de um Summon
    /// </summary>
    [Serializable]
    public struct SummonData
    {
        public int Health;
        public int Attack;
        public int Speed;
        [NonSerialized]
        public int Team;
        public bool IsFromLeft => Team % 2 == 0;
        public GameObject Prefab;
        public Sprite Avatar;
    }
}
