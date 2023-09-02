using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset data;

    public static GameManager Instance;

    public Dictionary<string, string> Cards = new();

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

        string[] lines = data.text.Split("\r\n");

        for(int i = 0; i < lines.Length; i++)
        {
            string[] datas = lines[i].Split(',');
            Cards.Add(datas[0], datas[1]);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ToPlayer()
    {
        SceneManager.LoadScene("Player");
    }
}
