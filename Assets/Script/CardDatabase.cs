using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public List<Card> cards = new();
    private object cardDatabase;

    public List<Card> GetRandomCards(int count)
    {
        List<Card> randomCards = new();
        List<Card> availableCards = new(cards);

        if (count > availableCards.Count)
        {
            Debug.LogWarning("Not enough available cards.");
            count = availableCards.Count;
        }

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, availableCards.Count);
            randomCards.Add(availableCards[randomIndex]);
            availableCards.RemoveAt(randomIndex);
        }

        return randomCards;
    }


    // 在Awake方法中初始化卡片資料庫
    private void Awake()
    {
        cardDatabase = GetComponent<CardDatabase>();
        if (cardDatabase == null)
        {
            Debug.LogError("CardDatabase component is missing!");
        }
        else
        {
            Debug.Log("CardDatabase found!");
        }
    }




    // 初始化卡片資料庫的方法，添加預設卡片
    private void InitializeCardDatabase()
    {
        // 添加預設卡片到資料庫中
        cards.Add(new Card(1,"Card1"));
        cards.Add(new Card(2,"Card2"));
        cards.Add(new Card(3,"Card3"));
        cards.Add(new Card(4,"Card4"));
        cards.Add(new Card(5,"Card5"));
        cards.Add(new Card(6,"Card6"));
        cards.Add(new Card(7,"Card7"));
        cards.Add(new Card(8,"Card8"));
        cards.Add(new Card(9,"Card9"));
        cards.Add(new Card(10,"Card10"));
        // 添加更多卡片，根據需要
    }
}
