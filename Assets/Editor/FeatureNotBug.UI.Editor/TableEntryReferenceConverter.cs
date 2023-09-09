using UnityEditor.UIElements;
using UnityEngine.Localization.Tables;

namespace FeatureNotBug.UI.Editor.FeatureNotBug.UI.Editor;

public sealed class TableEntryReferenceConverter : UxmlAttributeConverter<TableEntryReference> {
    public override TableEntryReference FromString(string value) {
        return long.TryParse(value, out var key) ? key : value;
    }

    public override string ToString(TableEntryReference value) {
        return !string.IsNullOrEmpty(value.Key) ? value.Key : value.KeyId.ToString();
    }
}
