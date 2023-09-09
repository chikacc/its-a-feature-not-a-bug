using System.Reflection;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.UIElements;

namespace FeatureNotBug; 

[UxmlElement]
public sealed partial class LocalizedTextElement : TextElement {
    public LocalizedTextElement() : this(string.Empty, string.Empty) { }

    public LocalizedTextElement(TableReference tableReference, TableEntryReference tableEntryReference) {
        StringReference = new LocalizedString(tableReference, tableEntryReference);
        StringReference.StringChanged += OnStringChanged;
        UpdateElement();
    }

    public LocalizedString StringReference { get; }

    [UxmlAttribute]
    public TableReference TableReference {
        get => StringReference.TableReference;
        set {
            StringReference.TableReference = value;
            UpdateStringReference();
            UpdateElement();
        }
    }

    [UxmlAttribute]
    public TableEntryReference TableEntryReference {
        get => StringReference.TableEntryReference;
        set {
            StringReference.TableEntryReference = value;
            UpdateStringReference();
            UpdateElement();
        }
    }

    [UxmlAttribute]
    public FallbackBehavior FallbackBehavior {
        get => StringReference.FallbackState;
        set {
            StringReference.FallbackState = value;
            UpdateStringReference();
            UpdateElement();
        }
    }

    void UpdateStringReference() {
        var propertyInfo = PropertyInfoContainer.Get(typeof(TableReference), "SharedTableData",
            BindingFlags.Instance | BindingFlags.NonPublic)!;
        if (propertyInfo.GetValue(StringReference.TableReference) is not SharedTableData sharedTableData) return;
        if (StringReference.TableEntryReference.ResolveKeyName(sharedTableData) is not { } keyName) return;
        StringReference.TableEntryReference = keyName;
    }

    public void UpdateElement() {
        StringReference.RefreshString();
    }

    void OnStringChanged(string value) {
        text = value;
    }
}