namespace FeatureNotBug;

public sealed class SubmitChatUseCase : ISubmitChatUseCase {
    readonly IChatRepository _chatRepository;

    public SubmitChatUseCase(IChatRepository chatRepository) {
        _chatRepository = chatRepository;
    }

    public void Execute(ulong playerId, string message) {
        var chat = new Chat {
            Id = (uint)_chatRepository.Count,
            PlayerId = playerId,
            Message = message
        };
        _chatRepository.Add(chat);
    }
}
