namespace FeatureNotBug;

public interface ISubmitChatUseCase {
    void Execute(ulong playerId, string message);
}
