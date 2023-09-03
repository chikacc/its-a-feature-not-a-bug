using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardCollectionDisplay : MonoBehaviour
{
    public CardCollection collection;
    public CardDisplay[] cards;

    private void OnEnable()
    {
        collection.OnCardsChanged += UpdateUI;
    }

    private void OnDisable()
    {
        collection.OnCardsChanged -= UpdateUI;
    }

    private void UpdateUI(IReadOnlyList<Card> rawCards)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (i > rawCards.Count)
            {
                cards[i].gameObject.SetActive(false);
                continue;
            }

            cards[i].SetCard(rawCards[i]);
        }
    }
}
