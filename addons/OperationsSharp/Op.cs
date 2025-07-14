using Godot;
using System;

namespace Operations;

/// <summary>
/// Provides convient methods for quickly creating pooled operations.
/// </summary>
public static partial class Op
{

    public static ActionOperation Action(Action action)
    {
        ActionOperation operation = Pools.Obtain<ActionOperation>();
        operation.Action = action;
        return operation;
    }

    public static AddOperation Add(Operator @operator, Operation operation)
    {
        AddOperation add = Pools.Obtain<AddOperation>();
        add.Operator = @operator;
        add.Operation = operation;
        return add;
    }
    
    public static AlwaysFailOperation AlwaysFail(params Operation[] children)
    {
        AlwaysFailOperation operation = Pools.Obtain<AlwaysFailOperation>();
        operation.Children.AddRange(children);
        return operation;
    }

    public static AlwaysRunningOperation AlwaysRunning(params Operation[] children)
    {
        AlwaysRunningOperation operation = Pools.Obtain<AlwaysRunningOperation>();
        operation.Children.AddRange(children);
        return operation;
    }

    public static AlwaysSucceedOperation AlwaysSucceed(params Operation[] children)
    {
        AlwaysSucceedOperation operation = Pools.Obtain<AlwaysSucceedOperation>();
        operation.Children.AddRange(children);
        return operation;
    }

    public static AudioFadeOperation AudioFade(bool @out, float duration)
    {
        AudioFadeOperation operation = Pools.Obtain<AudioFadeOperation>();
        operation.Reverse = @out;
        operation.Duration = duration;
        return operation;
    }

    public static DeferOperation Defer(Operation operation)
    {
        DeferOperation op = Pools.Obtain<DeferOperation>();
        op.Operation = operation;
        return op;
    }

    public static FuncOperation Funco(Func<Operation.Status> func)
    {
        FuncOperation op = Pools.Obtain<FuncOperation>();
        op.Func = func;
        return op;
    }

    public static PressedOperation Pressed(StringName action)
    {
        PressedOperation operation = Pools.Obtain<PressedOperation>();
        operation.Action = action;
        return operation;
    }

    public static JustPressedOperation JustPressed(StringName action)
    {
        JustPressedOperation operation = Pools.Obtain<JustPressedOperation>();
        operation.Action = action;
        return operation;
    }

    public static InvertOperation Invert(Operation child)
    {
        InvertOperation operation = Pools.Obtain<InvertOperation>();
        operation.Children.Add(child);
        return operation;
    }

    public static ManualOperation Manual() => Pools.Obtain<ManualOperation>();

    public static PrintOperation Print(params object[] what)
    {
        PrintOperation operation = Pools.Obtain<PrintOperation>();
        operation.What = what;
        return operation;
    }

    public static ProcessModeOperation ProcessMode(Operation target, Node.ProcessModeEnum mode)
    {
        ProcessModeOperation operation = Pools.Obtain<ProcessModeOperation>();
        operation.TargetOperation = target;
        operation.Set = mode;
        return operation;
    }

    public static SoundOperation Sound(string path, string bus = "Sound")
    {
        SoundOperation sound = Pools.Obtain<SoundOperation>();
        sound.Path = path;
        sound.Bus = bus;
        return sound;
    }

    public static Sound2DOperation Sound2D(string path, Vector2 position, string bus = "Sound")
    {
        Sound2DOperation sound = Pools.Obtain<Sound2DOperation>();
        sound.Path = path;
        sound.Bus = bus;
        sound.Position = position;
        return sound;
    }

    public static Sound3DOperation Sound3D(string path, Vector3 position, string bus = "Sound")
    {
        Sound3DOperation sound = Pools.Obtain<Sound3DOperation>();
        sound.Path = path;
        sound.Bus = bus;
        sound.Position = position;
        return sound;
    }

    public static ValidOperation Valid() => Pools.Obtain<ValidOperation>();

    public static TimeOperation Wait(float duration)
    {
        TimeOperation operation = Pools.Obtain<TimeOperation>();
        operation.Duration = duration;
        return operation;
    }
}

/// <summary>
/// Provides convient methods for quickly creating pooled operations.
/// </summary>
public static partial class Op
{

    public static NAnimationOperation Animation(string animation, double customBlend = -1, float customSpeed = 1, bool fromEnd = false)
    {
        NAnimationOperation operation = Pools.Obtain<NAnimationOperation>();
        operation.Animation = animation;
        operation.CustomBlend = customBlend;
        operation.CustomSpeed = customSpeed;
        operation.FromEnd = fromEnd;
        return operation;
    }

    public static NAnimationOperation AnimationBackwards(string animation, double customBlend = -1, float customSpeed = 1)
    {
        return Animation(animation, customBlend, customSpeed * -1, true);
    }

    public static NFreeOperation Free() => Pools.Obtain<NFreeOperation>();

    public static NMethodOperation NodeMethod(Callable method, Variant from, Variant to, float duration, Tween.TransitionType transType = Tween.TransitionType.Linear, Tween.EaseType easeType = Tween.EaseType.InOut)
    {
        NMethodOperation operation = Pools.Obtain<NMethodOperation>();
        operation.Method = method;
        operation.From = from;
        operation.To = to;
        operation.Duration = duration;
        operation.TransType = transType;
        operation.EaseType = easeType;
        return operation;
    }

    public static NModulateOperation NodeModulate(Color color, float duration, bool self = false, Tween.TransitionType transType = Tween.TransitionType.Linear, Tween.EaseType easeType = Tween.EaseType.InOut)
    {
        NModulateOperation operation = Pools.Obtain<NModulateOperation>();
        operation.Color = color;
        operation.Duration = duration;
        operation.Self = self;
        operation.TransType = transType;
        operation.EaseType = easeType;
        return operation;
    }

    public static NMove2DOperation NodeMove2D(Vector2 position, float duration, bool relative = true, bool global = false, Tween.TransitionType transType = Tween.TransitionType.Linear, Tween.EaseType easeType = Tween.EaseType.InOut)
    {
        return NodeRelative<NMove2DOperation>(position, duration, relative, global, transType, easeType);
    }

    public static NMove3DOperation NodeMove3D(Vector3 position, float duration, bool relative = true, bool global = false, Tween.TransitionType transType = Tween.TransitionType.Linear, Tween.EaseType easeType = Tween.EaseType.InOut)
    {
        return NodeRelative<NMove3DOperation>(position, duration, relative, global, transType, easeType);
    }

    public static NParticleOperation Particles2D(string path, Vector2 position = default)
    {
        NParticleOperation operation = Pools.Obtain<NParticleOperation>();
        operation.Path = path;
        operation.Position = position;
        return operation;
    }

    public static NParticleOperation Particles3D(string path, Vector3 position = default)
    {
        NParticleOperation operation = Pools.Obtain<NParticleOperation>();
        operation.Path = path;
        operation.Position = position;
        return operation;
    }

    public static NPropertyOperation NodeProperty(StringName property, Variant delta, float duration, Tween.TransitionType transType = Tween.TransitionType.Linear, Tween.EaseType easeType = Tween.EaseType.InOut)
    {
        NPropertyOperation operation = Pools.Obtain<NPropertyOperation>();
        operation.Property = property;
        operation.Delta = delta;
        operation.Duration = duration;
        operation.TransType = transType;
        operation.EaseType = easeType;
        return operation;
    }

    public static NReadyOperation NodeReady() => Pools.Obtain<NReadyOperation>();

    public static T NodeRelative<T>(Variant value, float duration, bool relative = true, bool global = false, Tween.TransitionType transType = Tween.TransitionType.Linear, Tween.EaseType easeType = Tween.EaseType.InOut) where T : NRelativeOperation
    {
        T operation = Pools.Obtain<T>();
        operation.Value = value;
        operation.Duration = duration;
        operation.Relative = relative;
        operation.Global = global;
        operation.TransType = transType;
        operation.EaseType = easeType;
        return operation;
    }

    public static T NodeRelative<T>(StringName property, Variant value, float duration, bool relative = true, bool global = false, Tween.TransitionType transType = Tween.TransitionType.Linear, Tween.EaseType easeType = Tween.EaseType.InOut) where T : NRelativeOperation
    {
        T operation = Pools.Obtain<T>();
        operation.Property = property;
        operation.Value = value;
        operation.Duration = duration;
        operation.Relative = relative;
        operation.Global = global;
        operation.TransType = transType;
        operation.EaseType = easeType;
        return operation;
    }

    public static NRotate2DOperation NodeRotate2D(float rotationDegrees, float duration, bool relative = true, bool global = false, Tween.TransitionType transType = Tween.TransitionType.Linear, Tween.EaseType easeType = Tween.EaseType.InOut)
    {
        return NodeRelative<NRotate2DOperation>(rotationDegrees, duration, relative, global, transType, easeType);
    }

    public static NRotate3DOperation NodeRotate3D(Vector3 rotationDegrees, float duration, bool relative = true, bool global = false, Tween.TransitionType transType = Tween.TransitionType.Linear, Tween.EaseType easeType = Tween.EaseType.InOut)
    {
        return NodeRelative<NRotate3DOperation>(rotationDegrees, duration, relative, global, transType, easeType);
    }

    public static NScale2DOperation NodeScale2D(Vector2 scale, float duration, bool relative = true, bool global = false, Tween.TransitionType transType = Tween.TransitionType.Linear, Tween.EaseType easeType = Tween.EaseType.InOut)
    {
        return NodeRelative<NScale2DOperation>(scale, duration, relative, global, transType, easeType);
    }

    public static NScale3DOperation NodeScale3D(Vector3 scale, float duration, bool relative = true, bool global = false, Tween.TransitionType transType = Tween.TransitionType.Linear, Tween.EaseType easeType = Tween.EaseType.InOut)
    {
        return NodeRelative<NScale3DOperation>(scale, duration, relative, global, transType, easeType);
    }

    public static NSetOperation NodeSet(StringName property, Variant value)
    {
        NSetOperation operation = Pools.Obtain<NSetOperation>();
        operation.Property = property;
        operation.Value = value;
        return operation;
    }
    
    public static NSignalOperation NodeSignal(StringName signalName, params Variant[] args)
    {
        NSignalOperation operation = Pools.Obtain<NSignalOperation>();
        operation.SignalName = signalName;
        operation.Args = args;
        return operation;
    }

    public static NTransform3DOperation NodeTransform3D(Transform3D transform, float duration, bool relative = true, bool global = false, Tween.TransitionType transType = Tween.TransitionType.Linear, Tween.EaseType easeType = Tween.EaseType.InOut)
    {
        return NodeRelative<NTransform3DOperation>(transform, duration, relative, global, transType, easeType);
    }

    public static NTransform2DOperation NodeTransform2D(Transform2D transform, float duration, bool relative = true, bool global = false, Tween.TransitionType transType = Tween.TransitionType.Linear, Tween.EaseType easeType = Tween.EaseType.InOut)
    {
        return NodeRelative<NTransform2DOperation>(transform, duration, relative, global, transType, easeType);
    }

    public static NVisibleOperation NodeVisible(bool visible)
    {
        NVisibleOperation operation = Pools.Obtain<NVisibleOperation>();
        operation.Visible = visible;
        return operation;
    }
}

/// <summary>
/// Provides convient methods for quickly creating pooled operations.
/// </summary>
public static partial class Op
{

    public static GuardSelectorOperation GuardSelector(params Operation[] children)
    {
        GuardSelectorOperation selector = Pools.Obtain<GuardSelectorOperation>();
        selector.Children.AddRange(children);
        return selector;
    }

    public static IndexedOperation Indexed(int starting = 0, params Operation[] children)
    {
        IndexedOperation operation = Pools.Obtain<IndexedOperation>();
        operation.Index = starting;
        operation.Children.AddRange(children);
        return operation;
    }

    public static ParallelOperation Parallel(ParallelPolicy policy)
    {
        ParallelOperation parallel = Pools.Obtain<ParallelOperation>();
        parallel.Policy = policy;
        return parallel;
    }

    public static ParallelOperation Parallel(params Operation[] children)
    {
        ParallelOperation parallel = Pools.Obtain<ParallelOperation>();
        parallel.Children.AddRange(children);
        return parallel;
    }

    public static ParallelOperation Parallel(ParallelPolicy policy, params Operation[] children)
    {
        ParallelOperation parallel = Pools.Obtain<ParallelOperation>();
        parallel.Policy = policy;
        parallel.Children.AddRange(children);
        return parallel;
    }

    public static RandomOperation Random(float probability, params Operation[] children)
    {
        RandomOperation random = Pools.Obtain<RandomOperation>();
        random.Probability = probability;
        random.Children.AddRange(children);
        return random;
    }

    public static RandomSelectorOperation RandomSelector(params Operation[] children)
    {
        RandomSelectorOperation selector = Pools.Obtain<RandomSelectorOperation>();
        selector.Children.AddRange(children);
        return selector;
    }

    public static RandomSequenceOperation RandomSequence(params Operation[] children)
    {
        RandomSequenceOperation sequence = Pools.Obtain<RandomSequenceOperation>();
        sequence.Children.AddRange(children);
        return sequence;
    }

    public static RepeatOperation Repeat(Operation child, int limit = 0)
    {
        RepeatOperation operation = Pools.Obtain<RepeatOperation>();
        operation.Limit = limit;
        operation.Children.Add(child);
        return operation;
    }

    public static SelectorOperation Selector(params Operation[] children)
    {
        SelectorOperation selector = Pools.Obtain<SelectorOperation>();
        selector.Children.AddRange(children);
        return selector;
    }

    public static SequenceOperation Sequence(SequencePolicy policy)
    {
        SequenceOperation sequence = Pools.Obtain<SequenceOperation>();
        sequence.Policy = policy;
        return sequence;
    }
    
    public static SequenceOperation Sequence(params Operation[] children)
    {
        SequenceOperation sequence = Pools.Obtain<SequenceOperation>();
        sequence.Children.AddRange(children);
        return sequence;
    }

    public static SequenceOperation Sequence(SequencePolicy policy, params Operation[] children)
    {
        SequenceOperation sequence = Pools.Obtain<SequenceOperation>();
        sequence.Policy = policy;
        sequence.Children.AddRange(children);
        return sequence;
    }

    public static TimeScaleOperation TimeScale(Operation child, float scale)
    {
        TimeScaleOperation operation = Pools.Obtain<TimeScaleOperation>();
        operation.Scale = scale;
        operation.Children.Add(child);
        return operation;
    }

    public static UntilFailOperation UntilFail(Operation child)
    {
        UntilFailOperation operation = Pools.Obtain<UntilFailOperation>();
        operation.Children.Add(child);
        return operation;
    }

    public static UntilSucceedOperation UntilSucceed(Operation child)
    {
        UntilSucceedOperation operation = Pools.Obtain<UntilSucceedOperation>();
        operation.Children.Add(child);
        return operation;
    }
}

/// <summary>
/// Provides convient methods for creating transition animations for Control nodes.
/// </summary>
public static partial class Op
{

    /// <summary>
    /// Modulates the targets alpha from 0 to 1.
    /// </summary>
    public static Operation ControlFadeIn(params object[] targets)
    {
        ParallelOperation visible = Parallel();
        Array.ForEach(targets, t => visible.Children.Add(NodeVisible(true).SetTarget(t)));
        ParallelOperation para = Parallel();
        foreach (Control target in targets)
        {
            target.SetThreadSafe("modulate", new Color(1, 1, 1, 0));
            para.Children.Add(NodeModulate(new Color(1, 1, 1, 1), .15f).SetTarget(target));
        }
        return Sequence(visible, para);
    }

    /// <summary>
    /// Modulates the targets alpha from 1 to 0.
    /// </summary>
    public static Operation ControlFadeOut(params object[] targets)
    {
        ParallelOperation visible = Parallel();
        Array.ForEach(targets, t => visible.Children.Add(NodeVisible(false).SetTarget(t)));
        ParallelOperation para = Parallel();
        foreach (Control target in targets)
            para.Children.Add(NodeModulate(new Color(1, 1, 1, 0), .15f).SetTarget(target));
        return Sequence(para, visible);
    }

    public static Operation ControlScaleIn(params object[] targets) => ControlScaleIn(Tween.TransitionType.Linear, Tween.EaseType.InOut, targets);
    /// <summary>
    /// Interpolates the targets scale from .5 to 1.
    /// </summary>
    public static Operation ControlScaleIn(Tween.TransitionType transType, Tween.EaseType easeType, params object[] targets)
    {
        ParallelOperation visible = Parallel();
        Array.ForEach(targets, t => visible.Children.Add(NodeVisible(true).SetTarget(t)));
        ParallelOperation para = Parallel();
        foreach (Control target in targets)
        {
            target.SetThreadSafe("pivot_offset", target.Size / 2);
            target.SetThreadSafe("scale", new Vector2(.5f, .5f));
            para.Children.Add(NodeScale2D(new Vector2(1.0f, 1.0f), .15f, false, false, transType, easeType).SetTarget(target));
        }
        return Sequence(visible, para);
    }

    public static Operation ControlScaleOut(params object[] targets) => ControlScaleOut(Tween.TransitionType.Linear, Tween.EaseType.InOut, targets);
    /// <summary>
    /// Interpolates the targets scale from 1 to .5.
    /// </summary>
    public static Operation ControlScaleOut(Tween.TransitionType transType, Tween.EaseType easeType, params object[] targets)
    {
        ParallelOperation visible = Parallel();
        Array.ForEach(targets, t => visible.Children.Add(NodeVisible(false).SetTarget(t)));
        ParallelOperation para = Parallel();
        foreach (Control target in targets)
            para.Children.Add(NodeScale2D(new Vector2(.5f, .5f), .15f, false, false, transType, easeType).SetTarget(target));
        return Sequence(para, visible);
    }


    public static Operation ControlScaleFadeIn(params object[] targets) => ControlScaleFadeIn(Tween.TransitionType.Linear, Tween.EaseType.InOut, 1.0f, targets);
    public static Operation ControlScaleFadeIn(float alpha, params object[] targets) => ControlScaleFadeIn(Tween.TransitionType.Linear, Tween.EaseType.InOut, alpha, targets);
    public static Operation ControlScaleFadeIn(Tween.TransitionType transType, Tween.EaseType easeType, params object[] targets) => ControlScaleFadeIn(transType, easeType, 1.0f, targets);
    /// <summary>
    /// Modulates the targets alpha from 0 to alpha and interpolates the targets scale from .5 to 1.
    /// </summary>
    public static Operation ControlScaleFadeIn(Tween.TransitionType transType, Tween.EaseType easeType, float alpha, params object[] targets)
    {
        ParallelOperation visible = Parallel();
        Array.ForEach(targets, t => visible.Children.Add(NodeVisible(true).SetTarget(t)));
        ParallelOperation para = Parallel();
        foreach (Control target in targets)
        {
            target.SetThreadSafe("pivot_offset", target.Size / 2);
            target.SetThreadSafe("scale", new Vector2(.5f, .5f));
            target.SetThreadSafe("modulate", new Color(1, 1, 1, 0));
            para.Children.Add(NodeScale2D(new Vector2(1.0f, 1.0f), .15f, false, false, transType, easeType).SetTarget(target));
            para.Children.Add(NodeModulate(new Color(1, 1, 1, alpha), .15f, false).SetTarget(target));
        }
        return Sequence(visible, para);
    }

    public static Operation ControlScaleFadeOut(params object[] targets) => ControlScaleFadeOut(Tween.TransitionType.Linear, Tween.EaseType.InOut, 0, targets);
    public static Operation ControlScaleFadeOut(float alpha, params object[] targets) => ControlScaleFadeOut(Tween.TransitionType.Linear, Tween.EaseType.InOut, alpha, targets);
    public static Operation ControlScaleFadeOut(Tween.TransitionType transType, Tween.EaseType easeType, params object[] targets) => ControlScaleFadeOut(transType, easeType, 0, targets);
    /// <summary>
    /// Modulates the targets alpha  from alpha to 0 and interpolates the targets scale from 1 to .5.
    /// </summary>
    public static Operation ControlScaleFadeOut(Tween.TransitionType transType, Tween.EaseType easeType, float alpha, params object[] targets)
    {
        ParallelOperation visible = Parallel();
        Array.ForEach(targets, t => visible.Children.Add(NodeVisible(false).SetTarget(t)));
        ParallelOperation para = Parallel();
        foreach (Control target in targets)
        {
            target.PivotOffset = target.Size / 2;
            para.Children.Add(NodeScale2D(new Vector2(.5f, .5f), .15f, false, false, transType, easeType).SetTarget(target));
            para.Children.Add(NodeModulate(new Color(1, 1, 1, alpha), .15f, false).SetTarget(target));
        }
        return Sequence(para, visible);
    }
}