using System.Linq;
using UnityEngine;

class DesignerSceneBootstrap : MonoBehaviour
{
    public CardDatabase conditionDatabase;
    public CardDatabase actionDatabase;

    private void Start()
    {
        if (GameManager.Instance == null)
        {
            return;
        }

        conditionDatabase.Initialize(GameManager.Instance.ConditionCards.Select(x => new Card(x.Key, x.Value)));
        actionDatabase.Initialize(GameManager.Instance.ActionCards.Select(x => new Card(x.Key, x.Value)));
    }
}
