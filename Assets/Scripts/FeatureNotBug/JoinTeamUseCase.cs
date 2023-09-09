namespace FeatureNotBug;

public sealed class JoinTeamUseCase : IJoinTeamUseCase {
    readonly ITeamRepository _teamRepository;

    public JoinTeamUseCase(ITeamRepository teamRepository) {
        _teamRepository = teamRepository;
    }

    public void Execute(ulong playerId, uint teamId) {

        // _teamRepository.Add(team);
    }
}
