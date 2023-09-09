using UnitGenerator;

namespace FeatureNotBug; 

[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator)]
public partial struct ReviewPhaseId {
    public static readonly ReviewPhaseId Empty = string.Empty;
}