namespace FeatureNotBug;

public interface IJoinTeamUseCase {
    void Execute(ulong playerId, uint teamId);
}
