using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI idText;
    [SerializeField]
    private TextMeshProUGUI depictionText;
    [SerializeField]
    private Button button;

    public void Show(string id, string depiction)
    {
        idText.text = id;
        depictionText.text = depiction;
        button.interactable = false;
    }
}
