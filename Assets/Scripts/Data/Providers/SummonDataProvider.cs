using UnityEngine;

namespace Yaw.Data
{
    /// <summary>
    /// Gerencia os dados do summon
    /// </summary>
    public class SummonDataProvider : MonoBehaviour, ISingleDataProvider<SummonData>
    {
        public SummonData Data { get; set; }

        public void Set(SummonData value)
        {
            Data = value;
        }
    }
}