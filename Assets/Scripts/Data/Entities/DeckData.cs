using System.Collections.Generic;

namespace Yaw.Data
{
    public class DeckData
    {
        public List<CardData> cards;
        public List<RuneDefinition> runes;

        public DeckData(DeckDefinition definition)
        {
            cards = new List<CardData>();
            runes = new List<RuneDefinition>();
            foreach (var card in definition.cards)
            {
                //Cria a carta e adiciona à lista
                cards.Add(new CardData(card));

                //Adiciona as runas possíveis se ainda não estiverem na lista
                foreach (var rune in card.Runes)
                {
                    if (runes.Contains(rune))
                    {
                        continue;
                    }
                    runes.Add(rune);
                }
            }

            //Embaralha as cartas e tudo pronto
            cards.Shuffle();
        }
    }
}