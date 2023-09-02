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
        string des;

        switch (selectCard.Type)
        {
            case PlayerCard.CardType.Action:
                {
                    if (!GameManager.Instance.ActionCards.TryGetValue(inputField.text, out des))
                    {
                        errorUI.SetActive(true);
                        return;
                    }
                }
                break;
            default:
                {
                    if (!GameManager.Instance.ConditionCards.TryGetValue(inputField.text, out des))
                    {
                        errorUI.SetActive(true);
                        return;
                    }
                }
                break;
        }

        selectCard.Show(inputField.text, des);
        inputUI.SetActive(false);
    }

    public void BackToMenu()
    {
        GameManager.Instance.BackToMenu();
    }
}
