using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Text id;
    public Text description;

    public void SetCard(Card card)
    {
        id.text = $"ID: {card.Id}";
        description.text = $"Depiction: {card.Description}";
    }
}
