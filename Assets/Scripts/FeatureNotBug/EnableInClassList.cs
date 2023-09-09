using UnityEngine.UIElements;

namespace FeatureNotBug; 

[UxmlObject]
public sealed partial class EnableInClassList : CustomBinding {
    [UxmlAttribute]
    public string ClassName { get; set; } = string.Empty;

    [UxmlAttribute]
    public bool Enable { get; set; }

    protected override BindingResult Update(in BindingContext context) {
        var element = context.targetElement;
        element.EnableInClassList(ClassName, Enable);
        return new BindingResult(BindingStatus.Success);
    }
}