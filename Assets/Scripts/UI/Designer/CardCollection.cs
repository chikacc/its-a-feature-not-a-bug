using UnityEngine;
using System.Collections.Generic;
using System;

public class CardCollection : MonoBehaviour
{
    public List<Card> cards;
    public event Action<IReadOnlyList<Card>> OnCardsChanged;

    public void Refill(IEnumerable<Card> collection)
    {
        cards ??= new List<Card>();
        if (cards.Count > 0) cards.Clear();
        cards.AddRange(collection);
        OnCardsChanged?.Invoke(cards);
    }

    public void UpdateCard(int index, Card newCard)
    {
        cards[index] = newCard;
        OnCardsChanged?.Invoke(cards);
    }

    public IReadOnlyList<Card> GetCards()
    {
        return cards;
    }
}
