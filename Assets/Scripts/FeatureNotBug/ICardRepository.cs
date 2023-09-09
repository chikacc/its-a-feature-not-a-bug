namespace FeatureNotBug;

public interface ICardRepository {
    int Count { get; }
    void Add(Card card);
    Card this[uint id] { get; }
}
