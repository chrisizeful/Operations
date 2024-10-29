using Godot;

namespace Operations;

/// <summary>
/// Interpolates the color of a the target <see cref="CanvasItem"/> over time (i.e. an <see cref="Node2D"/> or <see cref="Control"/>).
/// </summary>
[Operation("NodeModulate")]
public class NModulateOperation : TimeOperation
{

    /// <summary>
    /// The target color.
    /// </summary>
    public Color Color;
    /// <summary>
    /// Whether to set the <see cref="CanvasItem.PropertyName.SelfModulate"/> or the <see cref="CanvasItem.PropertyName.Modulate"/>.
    /// </summary>
    public bool Self;
    
    public Tween.TransitionType TransType = Tween.TransitionType.Linear;
    public Tween.EaseType EaseType = Tween.EaseType.InOut;

    private Color _start;
    private Color _end;

    public override void Start()
    {
        base.Start();
        _start = (Self ? Node.Get(CanvasItem.PropertyName.SelfModulate) : Node.Get(CanvasItem.PropertyName.Modulate)).AsColor();
        _end = new Color();
    }

    public override Status Act(double delta)
    {
        Status status = base.Act(delta);
        _end.R = Tween.InterpolateValue(_start.R, Color.R - _start.R, Percent, 1, TransType, EaseType).AsSingle();
        _end.G = Tween.InterpolateValue(_start.G, Color.G - _start.G, Percent, 1, TransType, EaseType).AsSingle();
        _end.B = Tween.InterpolateValue(_start.B, Color.B - _start.B, Percent, 1, TransType, EaseType).AsSingle();
        _end.A = Tween.InterpolateValue(_start.A, Color.A - _start.A, Percent, 1, TransType, EaseType).AsSingle();
        Node.Set(Self ? CanvasItem.PropertyName.SelfModulate : CanvasItem.PropertyName.Modulate, _end);
        return status;
    }
}