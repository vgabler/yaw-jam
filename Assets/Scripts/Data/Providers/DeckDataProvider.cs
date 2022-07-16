using UnityEngine;
using Yaw.Game;

namespace Yaw.Data
{
    /// <summary>
    /// Gerencia os dados do deck
    /// </summary>
    public class DeckDataProvider : MonoBehaviour, ISingleDataProvider<DeckData>
    {
        //Esse valor vai definir qual dos decks vai pegar lá no StageData
        public int team;

        public DeckData Data { get; set; }

        private void Awake()
        {
            //TODO isso seria resolvido por Dependency Injection
            //Não é bom usar o ServiceLocator.Get no Awake;
            //Mas nesse caso eu tenho certeza que o serviço está registrado
            var gameController = ServiceLocator.Get<IGameStateController>();

            var stageData = (gameController.State as GameState).Data;
            var def = stageData.Definition.TeamDecks[team];
            Set(new DeckData(def));
        }

        public void Set(DeckData value)
        {
            Data = value;
        }
    }
}