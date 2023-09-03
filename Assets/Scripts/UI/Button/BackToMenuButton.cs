using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BackToMenuButton : MonoBehaviour
{
    private void Awake()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(()=>
        {
            GameManager.Instance.BackToMenu();
        });
    }
}
