using System;

[Serializable]
public struct Card
{
    public string Id;
    public string Description;

    public Card(string id, string description)
    {
        Id = id;
        Description = description;
    }
}
