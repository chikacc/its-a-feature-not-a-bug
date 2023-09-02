using System.Collections.Generic;
using System;

public static class GetRandomResult
{
    public static string[] GetWithNonRepet(Dictionary<string, string> source, int count)
    {
        List<string> list = new ();

        foreach (var s in source)
        {
            list.Add(s.Key);
        }

        if (count >= source.Count)
        {
            return list.ToArray();
        }

        int index;
        int[] indexList = new int[list.Count];

        for (int i = 0; i < list.Count; i++)
        {
            indexList[i] = i;
        }

        Random rnd = new();
        
        for (int i = 0; i < list.Count; i++)
        {
            var tmp = indexList[i];
            index = rnd.Next(0, list.Count);
            indexList[i] = indexList[index];
            indexList[index] = tmp;
        }

        string[] result = new string[count];

        index = indexList[rnd.Next(0, list.Count)];

        for (int i = 0; i < count; i++)
        {
            index += i;
            if(index >= list.Count)
            {
                index -= list.Count;
            }
            result[i] = list[index];
        }
        
        return result;
    }
}
