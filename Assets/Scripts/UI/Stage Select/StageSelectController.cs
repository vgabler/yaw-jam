using System.Collections.Generic;
using UnityEngine;
using Yaw.Data;
using Yaw.Game;

namespace Yaw.StageSelect
{
    /// <summary>
    /// Atualiza a tela de stage select, e chama o serviço de iniciar jogo ao selecionar uma fase
    /// </summary>
    public class StageSelectController : MonoBehaviour
    {
        //Campos para atribuir pelo inspector
        public StageSelectEntry prefab;
        public Transform entriesContainer;

        //Dependências
        IDataProvider<StageData> dataProvider;
        IGameStateController gameController;

        //Campos
        List<StageSelectEntry> entries = new List<StageSelectEntry>();

        private void Start()
        {
            dataProvider = ServiceLocator.Get<IDataProvider<StageData>>();
            gameController = ServiceLocator.Get<IGameStateController>();

            UpdateData();
        }

        /// <summary>
        /// Cria novas entradas de acordo com a necessidade, e atualiza os dados
        /// </summary>
        void UpdateData()
        {
            //TODO seria melhor reativo
            var stages = dataProvider.GetAll();
            for (int i = 0; i < stages.Count; i++)
            {
                var entry = GetEntry(i);
                entry.SetUp(stages[i], i);
            }

            //Desabilita as entradas extras
            for (int i = stages.Count; i < entries.Count; i++)
            {
                entries[i].gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Retorna uma entry para o indice. Pooling básico
        /// </summary>
        StageSelectEntry GetEntry(int index)
        {
            StageSelectEntry entry;
            //Se já existe uma entry, usa-a
            if (entries.Count > index)
            {
                entry = entries[index];
            }
            //Se não, cria nova
            else
            {
                entry = Instantiate(prefab, entriesContainer);
                entry.OnPicked += OnPicked;
                entries.Add(entry);
            }

            //Ativa
            entry.gameObject.SetActive(true);
            return entry;
        }

        /// <summary>
        /// Evento chamado pelas entries, quando selecionadas
        /// </summary>
        private void OnPicked(StageData obj)
        {
            gameController.StartGame(obj);
        }

        private void OnDestroy()
        {
            //Remove o link do evento
            foreach (var e in entries)
            {
                e.OnPicked -= OnPicked;
            }
        }
    }
}