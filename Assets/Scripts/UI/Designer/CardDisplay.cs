using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Button button;
    public Text id;
    public Text description;

    public CardEvent Clicked = new();

    private void OnEnable()
    {
        button.onClick.AddListener(HandleClicked);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(HandleClicked);
    }

    public void SetCard(Card card)
    {
        id.text = card.Id;
        description.text = card.Description;
    }

    private void HandleClicked()
    {
        Clicked?.Invoke(this);
    }

    public class CardEvent : UnityEvent<CardDisplay> { }
}
