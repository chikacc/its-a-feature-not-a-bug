using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JudgerUI : MonoBehaviour
{
    [SerializeField]
    private GameObject inputUI;
    [SerializeField]
    private GameObject errorUI;
    [SerializeField]
    private TMP_InputField inputField;

    private PlayerCard selectCard;

    public void OnCardClick(PlayerCard playerCard)
    {
        selectCard = playerCard;
        inputField.text = string.Empty;
        inputUI.SetActive(true);
    }

    public void OnConfirmInput()
    {
        if (!GameManager.Instance.ConditionCards.TryGetValue(inputField.text, out var des))
        {
            errorUI.SetActive(true);
            return;
        }

        selectCard.Show(inputField.text, des);
        inputUI.SetActive(false);
    }

    public void BackToMenu()
    {
        GameManager.Instance.BackToMenu();
    }
}
