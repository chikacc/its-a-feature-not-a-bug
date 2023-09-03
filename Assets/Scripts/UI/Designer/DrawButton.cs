using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
class DrawButton : MonoBehaviour
{
    public CardDatabase database;
    public CardCollection collection;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(Draw);
    }
    private void OnDisable()
    {
        button.onClick.RemoveListener(Draw);
    }

    public void Draw()
    {
        collection.Refill(database.GetRandom(3));
    }
}
