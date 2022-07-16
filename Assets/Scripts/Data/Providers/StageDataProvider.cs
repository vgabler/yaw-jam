using System.Collections.Generic;
using UnityEngine;

namespace Yaw.Data
{
    /// <summary>
    /// Gerencia os dados das fases
    /// </summary>
    public class StageDataProvider : MonoBehaviour, IDataProvider<StageData>
    {
        List<StageData> stages;

        public List<StageData> GetAll() => stages;
        private void Awake()
        {
            Initialize();
            ServiceLocator.Register<IDataProvider<StageData>>(this);
        }

        void Initialize()
        {
            stages = new List<StageData>();
            var definitions = Resources.LoadAll<StageDefinition>("Stages");

            foreach (var def in definitions)
            {
                //TODO carregar os dados de um database (playerprefs?)
                stages.Add(new StageData
                {
                    Definition = def,
                    Locked = false,
                    Completed = false,
                });
            }

            //TODO temp; vertical slice, enquanto não tem fases suficientes para preencher a lista
            while (stages.Count < 24)
            {
                stages.Add(new StageData
                {
                    Locked = true,
                    Completed = false
                });
            }
        }
    }
}