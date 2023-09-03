using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;

public class CardDisplay : MonoBehaviour
{
    public Text card1IDText;
    public Text card1DepictionText;

    public Text card2IDText;
    public Text card2DepictionText;

    public Text card3IDText;
    public Text card3DepictionText;

    private CardDatabase cardDatabase;
    private List<Card> drawnCards = new List<Card>();

    public Action OnDrawCards { get; internal set; }
    public Action OnUpdateUI { get; internal set; }

    private void Start()
    {
        cardDatabase = GetComponent<CardDatabase>();
        DrawRandomCards();
        UpdateUI();
    }

    private void DrawRandomCards()
    {
        // 從卡片資料庫中隨機抽取三張不重覆的卡片
        drawnCards = cardDatabase.GetRandomCards(3);
    }

    private void UpdateUI()
    {
        if (drawnCards.Count >= 3)
        {
            // 設置第一張卡片信息
            card1IDText.text = "ID: " + drawnCards[0].ID.ToString();
            card1DepictionText.text = "Depiction: " + drawnCards[0].CardString;

            // 設置第二張卡片信息
            card2IDText.text = "ID: " + drawnCards[1].ID.ToString();
            card2DepictionText.text = "Depiction: " + drawnCards[1].CardString;

            // 設置第三張卡片信息
            card3IDText.text = "ID: " + drawnCards[2].ID.ToString();
            card3DepictionText.text = "Depiction: " + drawnCards[2].CardString;
        }
        else
        {
            // 如果沒有足夠的卡片，您可以顯示一個錯誤訊息或者不做任何操作。
        }
    }
}
