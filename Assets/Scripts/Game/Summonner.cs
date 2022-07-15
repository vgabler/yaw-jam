using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaw.Data;

namespace Yaw.Game
{
    public class Summonner : MonoBehaviour
    {
        public SummonData data;
        public Summon prefab;
        public Transform summonLocation;

        public void Summon()
        {
            var entity = Instantiate(prefab, summonLocation.position, Quaternion.identity, transform);
            entity.SetUp(data);
        }
    }
}