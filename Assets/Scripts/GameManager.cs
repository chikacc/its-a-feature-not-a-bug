using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset conditionCardsData;
    [SerializeField]
    private TextAsset actionCardsData;

    public static GameManager Instance;

    public Dictionary<string, string> ConditionCards { get; private set; } = new();

    public Dictionary<string, string> ActionCards { get; private set; } = new();

#if UNITY_EDITOR
    [InitializeOnEnterPlayMode]
    private static void OnEnterPlayModeInEditor(EnterPlayModeOptions options)
    {
        Instance = null;
    } 
#endif

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
            return;
        }

        string[] lines = conditionCardsData.text.Split("\r\n");

        for(int i = 0; i < lines.Length; i++)
        {
            string[] datas = lines[i].Split(',');
            ConditionCards.Add(datas[0], datas[1]);
        }

        lines = actionCardsData.text.Split("\r\n");

        for (int i = 0; i < lines.Length; i++)
        {
            string[] datas = lines[i].Split(',');
            ActionCards.Add(datas[0], datas[1]);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ToPlayer()
    {
        SceneManager.LoadScene("Player");
    }

    public void ToDesigner()
    {
        SceneManager.LoadScene("Designer");
    }
}
