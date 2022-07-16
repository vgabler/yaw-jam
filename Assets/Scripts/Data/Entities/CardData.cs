using System.Collections.Generic;
using UnityEngine;

namespace Yaw.Data
{
    public class CardData
    {
        public SummonData SummonData { get; private set; }
        public string Name { get; private set; }
        public RuneDefinition[] Combination { get; private set; }

        /// <summary>
        /// Cria uma combinação de runas
        /// </summary>
        public CardData(CardDefinition definition)
        {
            SummonData = definition.SummonData;
            Name = definition.name;

            //Sempre vão ser 6 slots
            Combination = new RuneDefinition[Constants.RUNE_SLOTS];

            //Seleciona alguns dos slots (ou todos, dependendo de quantas runas)
            var takenSlots = GetRandomIndexes(Mathf.Min(definition.Runes.Count, Combination.Length), Combination.Length);

            //Distribuir aleatoriamente as runas nos slots
            var runes = new List<RuneDefinition>(definition.Runes);

            foreach (var s in takenSlots)
            {
                var runeIndex = Random.Range(0, runes.Count);

                Combination[s] = runes[runeIndex];
                runes.RemoveAt(runeIndex);
            }
        }

        /// <summary>
        /// Retorna verdadeiro se a combinação é a mesma dessa carta
        /// </summary>
        public bool VerifyCombination(RuneDefinition[] combination)
        {
            if (combination.Length != Combination.Length)
            {
                Debug.LogError("Combinações de tamanho diferente!");
                return false;
            }

            for (int i = 0; i < Combination.Length; i++)
            {
                if (Combination[i] != combination[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Gera uma array de indices aleatórios dentro do range definido
        /// </summary>
        static int[] GetRandomIndexes(int limit, int range)
        {
            var result = new int[limit];
            var all = new int[range];
            //Cria uma array com todos os indices
            for (int i = 0; i < all.Length; ++i)
                all[i] = i;

            //Shuffle
            all.Shuffle();

            //Popula a array de resultado só até alcançar o limite
            for (int i = 0; i < limit; ++i)
                result[i] = all[i];

            return result;
        }
    }
}