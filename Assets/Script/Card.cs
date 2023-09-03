using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card : MonoBehaviour
{
    public int ID;
    public string CardString;

    // 礚把计篶ㄧ计匡
    public Card()
    {
        ID = 0;
        CardString = "";
    }

    // Τ把计篶ㄧ计ノ﹍て
    public Card(int id, string cardString)
    {
        ID = id;
        CardString = cardString;
    }
}
