using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomNumberGenerator : MonoBehaviour
{
    public Text[] textElements; // ノ陪ボ睹计Textじ皚
    private List<int> availableNumbers; // ノ计
    private System.Random random = new System.Random();

    private void Start()
    {
        // ﹍てノ计
        availableNumbers = new List<int>();
        for (int i = 1; i <= 10; i++)
        {
            availableNumbers.Add(i);
        }

        // ネΘぃ狡睹计だ皌倒Textじ
        for (int i = 0; i < textElements.Length; i++)
        {
            int randomIndex = random.Next(0, availableNumbers.Count);
            int randomNumber = availableNumbers[randomIndex];
            textElements[i].text = randomNumber.ToString();

            // 眖ノ计い埃ㄏノ计
            availableNumbers.RemoveAt(randomIndex);
        }
    }
}
