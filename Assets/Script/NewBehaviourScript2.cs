using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDrawButton : MonoBehaviour
{
    public Button drawButton; // 透過Inspector設置，拖曳Button元件到這裡
    public CardDisplay cardDisplay; // 透過Inspector設置，拖曳CardDisplay腳本所在物件到這裡

    private void Start()
    {
        drawButton.onClick.AddListener(DrawCard);
        cardDisplay.OnDrawCards += DrawCard;
        cardDisplay.OnUpdateUI += UpdateUI;
    }

    private void DrawCard()
    {
        // Your code here
    }

    private void UpdateUI()
    {
        // Your code here
    }
}
