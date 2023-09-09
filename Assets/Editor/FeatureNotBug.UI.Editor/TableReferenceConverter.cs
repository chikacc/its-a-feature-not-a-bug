using System;
using UnityEditor.UIElements;
using UnityEngine.Localization.Tables;

namespace FeatureNotBug.UI.Editor.FeatureNotBug.UI.Editor;

public sealed class TableReferenceConverter : UxmlAttributeConverter<TableReference> {
    public override TableReference FromString(string value) {
        return Guid.TryParse(value, out var guid) ? guid : value;
    }

    public override string ToString(TableReference value) {
        return !string.IsNullOrEmpty(value.TableCollectionName)
            ? value.TableCollectionName
            : value.TableCollectionNameGuid.ToString();
    }
}
