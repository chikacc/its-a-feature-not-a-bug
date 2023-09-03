using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomCardGenerator : MonoBehaviour
{
    public Text[] textFields; 
    public Dictionary<int, string> cardData; 
    private List<int> usedCardIDs = new List<int>(); 

    void Start()
    {
        
        GenerateRandomCards();
    }

    void GenerateRandomCards()
    {
       
        if (textFields.Length != 6)
        {
            Debug.LogError("Text数组长度必须为6！");
            return;
        }

        
        List<int> randomCardIDs = new List<int>();
        while (randomCardIDs.Count < 6)
        {
            int randomID = Random.Range(1, 11); 
            if (!usedCardIDs.Contains(randomID))
            {
                randomCardIDs.Add(randomID);
                usedCardIDs.Add(randomID);
            }
        }

        
        for (int i = 0; i < textFields.Length; i++)
        {
            
            string randomResult = cardData[randomCardIDs[i]];

            
            textFields[i].text = randomResult;
        }
    }
}
