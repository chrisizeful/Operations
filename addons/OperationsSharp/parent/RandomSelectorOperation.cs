using System;
using System.Collections.Generic;

namespace Operations;

/// <summary>
/// Randomly sets an order to run its children in. Will fail if all children fail in order,
/// or succeed if one succeeds in the process.
/// </summary>
[Operation("RandomSelector")]
public class RandomSelectorOperation : Operation
{

    /// <summary>
    /// The <see cref="Random"/> object to use, or null.
    /// </summary>
    public Random Rand;

    private readonly List<int> _sequence = new();
    private int _index = 0;

    public override void Start()
    {
        base.Start();
        _index = 0;
        SetSequence();
    }

    private void SetSequence()
    {
        for (int i = 0; i < Children.Count; i++)
            if (!_sequence.Contains(i))
                _sequence.Add(i);
        Shuffle();
    }

    private void Shuffle()
    {
        Rand ??= new Random();
        var count = _sequence.Count;
        var last = count - 1;
        for (var i = 0; i < last; i++)
        {
            var r = Rand.Next(i, count);
            (_sequence[r], _sequence[i]) = (_sequence[i], _sequence[r]);
        }
    }

    public override void ChildFail()
    {
        SetSequence();
        _index++;
    }

    public override Status Act(double delta)
    {
        if (_index >= _sequence.Count)
            return Status.Failed;
        Children[_sequence[_index]].Run(delta);
        return Status.Running;
    }
}
