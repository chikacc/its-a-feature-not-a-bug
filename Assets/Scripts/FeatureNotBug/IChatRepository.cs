namespace FeatureNotBug;

public interface IChatRepository {
    int Count { get; }
    void Add(Chat chat);
}
