using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class CardDisplay : MonoBehaviour
{
    public Text card1IDText;
    public Text card1DepictionText;

    public Text card2IDText;
    public Text card2DepictionText;

    public Text card3IDText;
    public Text card3DepictionText;

    public event Action OnDrawCards;
    public event Action OnUpdateUI;

private CardDatabase cardDatabase;
    private List<Card> drawnCards = new List<Card>();

    private void Start()
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

        // 調用初始化卡片資料庫的方法
        cardDatabase.InitializeCardDatabase();

        // 調用抽卡的方法
        DrawRandomCards(3);

        // 更新UI
        UpdateUI();
    }

    private void DrawRandomCards(int count)
    {
        // 從卡片資料庫中隨機抽取指定數量的卡片
        drawnCards = cardDatabase.GetRandomCards(count);
    }

    private void UpdateUI()
    {
        if (drawnCards.Count >= 3)
        {
            card1IDText.text = "ID: " + drawnCards[0].ID.ToString();
            card1DepictionText.text = "Depiction: " + drawnCards[0].CardString;

            card2IDText.text = "ID: " + drawnCards[1].ID.ToString();
            card2DepictionText.text = "Depiction: " + drawnCards[1].CardString;

            card3IDText.text = "ID: " + drawnCards[2].ID.ToString();
            card3DepictionText.text = "Depiction: " + drawnCards[2].CardString;
        }
        else
        {
            Debug.LogWarning("Not enough drawn cards to update UI.");
        }
    }
}
