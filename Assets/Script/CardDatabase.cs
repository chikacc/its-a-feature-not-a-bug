using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardDatabase : MonoBehaviour
{
   


    // 在Awake方法中初始化卡片資料庫
    private void Awake()
    {
        InitializeCardDatabase(); // 調用初始化方法
    }

    // 初始化卡片資料庫的方法，添加預設卡片
    // 初始化卡片資料庫的方法，添加預設卡片
    public void InitializeCardDatabase()
    {
        cards.Add(new Card(1, "Card1"));
        cards.Add(new Card(2, "Card2"));
        cards.Add(new Card(3, "Card3"));
        cards.Add(new Card(4, "Card4"));
        cards.Add(new Card(5, "Card5"));
        cards.Add(new Card(6, "Card6"));
        cards.Add(new Card(7, "Card7"));
        cards.Add(new Card(8, "Card8"));
        cards.Add(new Card(9, "Card9"));
        cards.Add(new Card(10, "Card10"));
    }
    public List<Card> cards = new();

    internal List<Card> GetRandomCards(int v)
    {
        throw new NotImplementedException();
    }
}
