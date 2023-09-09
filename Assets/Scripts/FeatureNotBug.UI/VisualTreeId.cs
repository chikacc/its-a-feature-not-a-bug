using UnitGenerator;
using UnityEngine.Localization.Tables;

namespace FeatureNotBug.UI; 

[UnitOf(typeof(string), UnitGenerateOptions.ImplicitOperator)]
public partial struct VisualTreeId {
    public static implicit operator TableEntryReference(VisualTreeId id) => (string)id;
}