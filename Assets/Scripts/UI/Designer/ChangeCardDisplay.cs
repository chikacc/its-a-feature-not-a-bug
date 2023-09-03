using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ChangeCardDisplay))]
class ChangeCardDisplay : MonoBehaviour
{
    public CardDatabase database;
    public CardCollection collection;
    public int index;
    public InputField id;
    public Button button;

    private void OnEnable()
    {
        button.onClick.AddListener(HandleClicked);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(HandleClicked);
    }

    public void SetIndex(int value)
    {
        index = value;
    }

    private void HandleClicked()
    {
        var card = database.Find(id.text);
        collection.UpdateCard(index, card);
    }
}
