using System.Collections.Generic;
using UnityEngine;
using Yaw.Storage;

namespace Yaw.Data
{
    /// <summary>
    /// Gerencia os dados das fases
    /// </summary>
    public class StageDataProvider : MonoBehaviour, IDataProvider<StageData>
    {
        ILocalStorage localStorage;
        Dictionary<string, StageData> stages = new Dictionary<string, StageData>();

        private void Awake()
        {
            ServiceLocator.Register<IDataProvider<StageData>>(this);
        }

        private void Start()
        {
            localStorage = ServiceLocator.Get<ILocalStorage>();
            Initialize();
        }

        void Initialize()
        {
            var definitions = Resources.LoadAll<StageDefinition>("Stages");

            foreach (var def in definitions)
            {
                //TODO deveria ser um ID, mais seguro; se mudar o name vai atrapalhar quem já tinha save
                var key = def.name;

                var data = localStorage.Get<StageData>(key);
                //Adiciona a definição
                data.Definition = def;
                stages.Add(def.name, data);
            }

            var i = 0;
            //TODO temp; vertical slice, enquanto não tem fases suficientes para preencher a lista
            while (stages.Count < 24)
            {
                i++;
                stages.Add(
                    $"placeholder{i}",
                    new StageData { Locked = true, Completed = false }
                );
            }
        }

        public List<StageData> GetAll() => new List<StageData>(stages.Values);

        public void Set(StageData value)
        {
            var key = value.Definition.name;
            //Atualiza o valor na lista, depois salva no storage
            if (stages.ContainsKey(key) == false)
            {
                Debug.LogError("Tentando salvar fase não carregada?");
                return;
            }

            stages[key] = value;
            localStorage.Set(key, value);
        }
    }
}