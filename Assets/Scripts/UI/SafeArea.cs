using UnityEngine;

namespace Assets.Scripts.UI
{
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    class SafeArea : MonoBehaviour
    {
        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = (RectTransform)transform;
        }

        private void OnEnable()
        {
            rectTransform.anchoredPosition = new Vector2(Screen.safeArea.min.x, Screen.safeArea.min.y);
            rectTransform.sizeDelta = Screen.safeArea.size;
        }
    }
}
