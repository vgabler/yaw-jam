using System.Collections.Generic;
using UnityEngine;
using Yaw.Data;

namespace Yaw.Game
{
    /// <summary>
    /// Versão AI de controle da combinaçãod e runas e summon
    /// </summary>
    public class RuneBoardControllerAI : MonoBehaviour
    {

        #region Campos
        //Contagem de tempo para tomada de decisão
        float timer;
        RuneDefinition[] combination;
        AiSettings settings;
        #endregion

        #region Dependências
        public DeckController deckController;
        public GameObject iDataProviderDeck;
        IGameStateController gameStateController;
        #endregion

        /// <summary>
        /// Busca as dependências
        /// </summary>
        void Start()
        {
            //Busca as dependências
            var dataProvider = iDataProviderDeck.GetComponent<ISingleDataProvider<DeckData>>();
            gameStateController = ServiceLocator.Get<IGameStateController>();

            //Busca as configurações
            var stageData = (gameStateController.State as GameState).Data;
            settings = stageData.Definition.AiSettings;

            //Prepara a lista de runas e combinação
            combination = new RuneDefinition[Constants.RUNE_SLOTS];

            //No começo, o timer é o de espera para iniciar
            timer = settings.StartDelay;
        }

        private void Update()
        {
            //Se não estiver rodando, ignora
            if (!(gameStateController.State is GameStateRunning))
            {
                return;
            }

            timer -= Time.deltaTime;

            if (timer > 0)
            {
                return;
            }

            //Quando o timer terminar, passa para colocar runas ou summon
            Act();
        }

        /// <summary>
        /// Coloca uma runa de cada vez das necessárias para a carta
        /// </summary>
        void Act()
        {
            var card = deckController.ActiveCards[0];

            //Se a combinação for igual, passa para o summon
            var allSet = true;

            //Verifica qual a runa que está faltando, e coloca-a
            for (int i = 0; i < card.Combination.Length; i++)
            {
                if (combination[i] == card.Combination[i])
                {
                    continue;
                }

                //Atribui a runa certa na combinação
                combination[i] = card.Combination[i];
                //Colocar / remover runas gasta tempo
                allSet = false;
                break;
            }

            //Se já terminou a combinação, vai summonar
            if (allSet)
            {
                timer = settings.SummonDelay;
                deckController.TrySummon(combination);
            }
            //Se não, vai para colocar outra runa
            else
            {
                timer = settings.RunePlacementDelay;
            }
        }
    }
}