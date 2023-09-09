using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

namespace FeatureNotBug.UI; 

[UxmlElement]
public sealed partial class RadialProgress : VisualElement {
    Color _trackColor = Color.clear;
    Color _progressColor = Color.white;
    float _lineWidth = 8f;
    LineCap _lineCap = LineCap.Butt;
    ArcDirection _direction = ArcDirection.Clockwise;
    float _progress = 100f;
    float _startAngle;

    public RadialProgress() {
        generateVisualContent += OnGenerateVisualContent;
    }

    [UxmlAttribute]
    public Color TrackColor {
        get => _trackColor;
        set {
            _trackColor = value;
            MarkDirtyRepaint();
        }
    }

    [UxmlAttribute]
    [CreateProperty]
    public Color ProgressColor {
        get => _progressColor;
        set {
            _progressColor = value;
            MarkDirtyRepaint();
        }
    }

    [UxmlAttribute]
    public float LineWidth {
        get => _lineWidth;
        set {
            _lineWidth = value;
            MarkDirtyRepaint();
        }
    }

    [UxmlAttribute]
    public LineCap LineCap {
        get => _lineCap;
        set {
            _lineCap = value;
            MarkDirtyRepaint();
        }
    }

    [UxmlAttribute]
    [CreateProperty]
    public float Progress {
        get => _progress;
        set {
            _progress = value;
            MarkDirtyRepaint();
        }
    }

    [UxmlAttribute]
    public ArcDirection Direction {
        get => _direction;
        set {
            _direction = value;
            MarkDirtyRepaint();
        }
    }

    [UxmlAttribute]
    public float StartAngle {
        get => _startAngle;
        set {
            _startAngle = value;
            MarkDirtyRepaint();
        }
    }

    static void OnGenerateVisualContent(MeshGenerationContext ctx) {
        var e = (RadialProgress)ctx.visualElement;
        var (width, height) = (e.contentRect.width, e.contentRect.height);
        var painter = ctx.painter2D;
        painter.lineWidth = e.LineWidth;
        painter.lineCap = e.LineCap;
        var center = new Vector2(0.5f * width, 0.5f * height);
        var radius = Mathf.Min(0.5f * (width - e.LineWidth), 0.5f * (height - e.LineWidth));
        var theta = e.Progress * 0.01f * 360f;
        if (e.TrackColor != Color.clear) {
            painter.strokeColor = e.TrackColor;
            painter.BeginPath();
            painter.Arc(center, radius, e.StartAngle + theta, e.StartAngle, e.Direction);
            painter.Stroke();
        }

        painter.strokeColor = e.ProgressColor;
        painter.BeginPath();
        painter.Arc(center, radius, e.StartAngle, e.StartAngle + theta, e.Direction);
        painter.Stroke();
    }
}