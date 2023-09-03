using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardCollectionDisplay : MonoBehaviour
{
    public CardDatabase database;
    public CardCollection collection;
    public CardDisplay[] cards;

    public CardIndexEvent IndexClicked = new();

    private void OnEnable()
    {
        collection.OnCardsChanged += UpdateCards;
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].Clicked.AddListener(HandleCardClicked);
        }
    }

    private void OnDisable()
    {
        collection.OnCardsChanged -= UpdateCards;
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].Clicked.RemoveListener(HandleCardClicked);
        }
    }

    private void UpdateCards(IReadOnlyList<Card> rawCards)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (i >= rawCards.Count)
            {
                cards[i].gameObject.SetActive(false);
                continue;
            }

            cards[i].SetCard(rawCards[i]);
        }
    }

    private void HandleCardClicked(CardDisplay card)
    {
        var index = Array.IndexOf(cards, card);
        IndexClicked?.Invoke(index);
    }

    public class CardIndexEvent : UnityEvent<int> { }
}
