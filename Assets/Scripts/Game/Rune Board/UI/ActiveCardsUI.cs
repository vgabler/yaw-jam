using UnityEngine;
namespace Yaw.Game
{
    /// <summary>
    /// Gerencia a visualização das cartas ativas
    /// </summary>
    public class ActiveCardsUI : MonoBehaviour
    {
        CardUI[] cards;
        public DeckController controller;
        void Start()
        {
            cards = GetComponentsInChildren<CardUI>(true);
            //TODO reatividade
            controller.OnCardsUpdated += UpdateUI;
            UpdateUI();
        }

        private void OnDestroy()
        {
            controller.OnCardsUpdated -= UpdateUI;
        }

        void UpdateUI()
        {
            var activeCards = controller.ActiveCards;
            if (activeCards == null)
            {
                return;
            }
            //Atualiza o UI
            for (int i = 0; i < cards.Length; i++)
            {
                //Se tiver carta ativa para esse indice, atualiza
                if (i < activeCards.Count)
                {
                    cards[i].gameObject.SetActive(true);
                    cards[i].SetUp(activeCards[i]);
                }
                //Se não, desabilita o card
                else
                {
                    cards[i].gameObject.SetActive(false);
                }
            }
        }
    }
}