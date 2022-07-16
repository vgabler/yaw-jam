using UnityEngine;
using Yaw.Data;

namespace Yaw.Game
{
    /// <summary>
    /// Instancia e inicializa o Summon
    /// </summary>
    public class Summonner : MonoBehaviour
    {
        public GameObject prefab;
        public Transform summonLocation;

        public void Summon(SummonData data)
        {
            var obj = Instantiate(prefab, summonLocation.position, Quaternion.identity, transform);

            var dataProvider = obj.GetComponentInChildren<ISingleDataProvider<SummonData>>();
            dataProvider.Set(data);
        }
    }
}