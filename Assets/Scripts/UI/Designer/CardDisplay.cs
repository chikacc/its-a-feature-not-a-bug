using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Text id;
    public Text description;

    public void SetCard(Card card)
    {
        id.text = card.Id;
        description.text = card.Description;
    }
}
