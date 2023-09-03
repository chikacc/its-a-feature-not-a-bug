using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public Card[] cards;

    public void Initialize(IEnumerable<Card> collection) => cards = collection.ToArray();
    public Card Find(string id) => Array.Find(cards, x => x.Id == id);

    public IEnumerable<Card> GetRandom(int count) => cards.GetRandom(count);
}
