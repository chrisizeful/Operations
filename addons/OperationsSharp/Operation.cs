using System.Collections.Generic;
using Godot;
using System;
using System.Linq;

namespace Operations;

/// <summary>
/// The base class for all operations.
/// </summary>
public class Operation : IPoolable
{

    /// <summary>
    /// An optional name, can be used for debugging purposes.
    /// </summary>
#nullable enable
    public string? Name;
#nullable disable
    /// <summary>
    /// The operation that controls/parents this one.
    /// </summary>
    public Operation Control;
    /// <summary>
    /// The operation that guards this one (i.e. this operation will not process unless this guard succeeds).
    /// </summary>
    public Operation Guard;
    /// <summary>
    /// Children this operation is responsible for processing.
    /// </summary>
    public List<Operation> Children = new();
    /// <summary>
    /// The object to operate on.
    /// </summary>
    public object Target;
    /// <summary>
    /// A cached validator used to validate <see cref="Target"/>.
    /// Automatically set when <see cref="SetTarget"/> is called.
    /// </summary>
    public Validator TargetValidator;
    /// <summary>
    /// The Node to operate on, provided for convenience.
    /// </summary>
    public Node Node => (Node) Target;
    /// <summary>
    /// The current <see cref="Status"/> of this operation.
    /// </summary>
    public Status Current = Status.Fresh;
    /// <summary>
    /// How this operation behaves when the <see cref="SceneTree"/> is paused.
    /// </summary>
    public Node.ProcessModeEnum ProcessMode = Node.ProcessModeEnum.Pausable;
    /// <summary>
    /// What happens if the <see cref="Target"/> is invalidated (i.e. freed from memory).
    /// </summary>
    public InvalidPolicy Invalid = InvalidPolicy.Success;

#nullable enable
    /// <summary>
    /// Sets the name of this operation.
    /// </summary>
    /// <returns>This operation for chaining.</returns>
    public Operation SetName(string? name)
    {
        Name = name;
        return this;
    }
#nullable disable

    /// <summary>
    /// Sets the operation that will "guard" this one. If the guard does not have a target,
    /// it's target will be set to this operations target.
    /// </summary>
    /// <param name="guard">The operation that needs to succeed.</param>
    /// <returns>This operation for chaining.</returns>
    public Operation SetGuard(Operation guard)
    {
        Guard = guard;
        if (guard.Target == null)
            guard.SetTarget(Target);
        return this;
    }

    /// <summary>
    /// Sets the object to target and validator to use. Will also set the target of the <see cref="Guard"/> and children if
    /// they are null or <paramref name="force"/> is true.
    /// </summary>
    /// <param name="target">The object to target.</param>
    /// <param name="force">Whether to set the target on the guard and children even if it is not null.</param>
    /// <returns>This operation for chaining.</returns>
    public Operation SetTarget(object target, bool force = false)
    {
        Target = target;
        Type type = target.GetType();
        TargetValidator = Operator.TargetValidators.FirstOrDefault(e => e.Key.IsAssignableFrom(type)).Value;
        // Set target for guard
        if (Guard != null && (Guard.Target == null || force))
            Guard.SetTarget(Target, force);
        // Set target for children
        foreach (Operation child in Children)
            if (Target != null && (child.Target == null || force))
                child.SetTarget(target, force);
        return this;
    }

    /// <summary>
    /// Sets how this operation behaves when the <see cref="SceneTree"/> is paused.
    /// </summary>
    /// <param name="mode">The mode to use.</param>
    /// <returns>This operation for chaining.</returns>
    public Operation SetProcessMode(Node.ProcessModeEnum mode)
    {
        // Inherit target process mode
        if (mode == Node.ProcessModeEnum.Inherit && Target != null)
        {
            if (Target is Node node) mode = node.ProcessMode;
            else mode = Node.ProcessModeEnum.Always;
        }
        ProcessMode = mode;
        // Set mode for guard
        Guard?.SetProcessMode(mode);
        // Set mode for children
        foreach (Operation child in Children)
            if (Target != null)
                child.SetProcessMode(mode);
        return this;
    }

    /// <summary>
    /// Sets how this operation behaves when the <see cref="Target"/> is invalidated (i.e. freed from memory).
    /// </summary>
    /// <param name="invalid">The policy to use.</param>
    /// <returns>This operation for chaining.</returns>
    public Operation SetInvalidPolicy(InvalidPolicy invalid)
    {
        Invalid = invalid;
        Guard?.SetInvalidPolicy(invalid);
        foreach (Operation child in Children)
            child.SetInvalidPolicy(invalid);
        return this;
    }

    /// <summary>
    /// A convenience method to add a child Operation.
    /// </summary>
    /// <param name="child">The child to add.</param>
    /// <returns>This operation for chaining.</returns>
    public Operation AddChild(Operation child)
    {
        Children.Add(child);
        return this;
    }

    /// <summary>
    /// Sets the operation back to its initial state so it can be run again.
    /// </summary>
    public virtual void Restart()
    {
        // Cancel but reset status
        Cancel();
        Current = Status.Fresh;
        Guard?.Restart();
        // Restart guard/children
        foreach (Operation child in Children)
            child.Restart();
    }
    
    /// <summary>
    /// A mostly unused method that can be used for debug or other purposes.
    /// </summary>
    /// <returns>This operation for chaining.</returns>
    public virtual Operation Display()
    {
        foreach (Operation child in Children)
            child.Display();
        return this;
    }

    /// <summary>
    /// Called once when operation is first run (i.e. <see cref="Current"/> is <see cref="Status.Fresh"/>).
    /// </summary>
    public virtual void Start()
    {
        foreach (Operation child in Children)
            child.Control = this;
    }

    /// <summary>
    /// Called when the operation succeeds, fails, or is cancelled.
    /// </summary>
    public virtual void End() {}

    /// <summary>
    /// Called when when <see cref="Current"/> is <see cref="Status.Running"/>.
    /// </summary>
    public virtual void Running()
    {
        Current = Status.Running;
        Control?.ChildRunning();
    }

    /// <summary>
    /// Called when <see cref="Current"/> is <see cref="Status.Succeeded"/>.
    /// </summary>
    public virtual void Success()
    {
        if (Current == Status.Succeeded)
            return;
        Current = Status.Succeeded;
        Control?.ChildSuccess();
        End();
    }

    /// <summary>
    /// Called when <see cref="Current"/> is <see cref="Status.Failed"/>.
    /// </summary>
    public virtual void Fail()
    {
        if (Current == Status.Failed)
            return;
        Current = Status.Failed;
        Control?.ChildFail();
        End();
    }

    /// <summary>
    /// Called when <see cref="Current"/> is <see cref="Status.Cancelled"/>.
    /// </summary>
    public virtual void Cancel()
    {
        if (Current is not Status.Fresh || Current is not Status.Running)
            return;
        Current = Status.Cancelled;
        foreach (Operation child in Children)
            child.Cancel();
        End();
    }

    /// <summary>
    /// Called when a child operation succeeds.
    /// </summary>
    public virtual void ChildSuccess() => Success();

    /// <summary>
    /// Called when a child operation fails.
    /// </summary>
    public virtual void ChildFail() => Fail();

    /// <summary>
    /// Called when a child operation is running.
    /// </summary>
    public virtual void ChildRunning() {}

    /// <summary>
    /// Checks whether or not the <see cref="Guard"/> succeeds. If no guard is present,
    /// returns true.
    /// </summary>
    /// <returns>If the <see cref="Guard" succeeded./></returns>
    public bool CheckGuard()
    {
        // No guard to check
        if (Guard == null)
            return true;
        // Check the guard of the guard recursively
        if (!Guard.CheckGuard())
            return false;
        // Use the tree's guard evaluator task to check the guard of this task
        Guard.Run(0);
        return Guard.Current == Status.Succeeded;
    }

    /// <summary>
    /// Runs this operation if <see cref="Current"/> is one of <see cref="Status.Fresh"/> or <see cref="Status.Running"/>.
    /// This method is responsible for updating the <see cref="Current"/> status and calling the relevant methods (i.e. <see cref="Fail"/>)
    /// </summary>
    /// <param name="delta">Delta time between frames.</param>
    public virtual void Run(double delta)
    {
        // Return if cancelled, failed, or succeeded
        if (Current is not Status.Running and not Status.Fresh)
            return;
        // Fail if no validator or target is not valid
        if (TargetValidator == null || !TargetValidator.Invoke(this))
        {
            if (Invalid == InvalidPolicy.Success) Success();
            else Fail();
            return;
        }
        // Check if operation is fresh
        if (Current is Status.Fresh)
            Start();
        // Act
        Status result = Act(delta);
        switch (result)
        {
            case Status.Succeeded:
                Success();
                break;
            case Status.Failed:
                Fail();
                break;
            case Status.Running:
                Running();
                break;
        }
    }
    
    /// <summary>
    /// The core logic of an operationw which determines its status.
    /// </summary>
    /// <param name="delta">Delta time between frames.</param>
    /// <returns>The current status.</returns>
    public virtual Status Act(double delta) => Status.Running;

    /// <summary>
    /// Resets the operation to a blank state so it can be reused.
    /// </summary>
    public virtual void Reset()
    {
        Restart();
        Control = null;
        Target = null;
        ProcessMode = Node.ProcessModeEnum.Pausable;
        // Free guard
        if (Guard != null)
            Pools.Free(Guard);
        // Free children
        foreach (Operation child in Children) 
            Pools.Free(child);
        Children.Clear();
    }

    /// <summary>
    /// Used to determine if the target of an operation is valid or not.
    /// </summary>
    /// <param name="target">The target of an operation.</param>
    /// <returns></returns>
    public delegate bool Validator(Operation operaiton);
    
    /// <summary>
    /// An enum for determining the status after an operation is run.
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// Has never run or has been reset.
        /// </summary>
        Fresh,
        /// <summary>
        /// Is in the middle of operating.
        /// </summary>
        Running,
        /// <summary>
        /// Ran and returned a failure result.
        /// </summary>
        Failed,
        /// <summary>
        /// Ran and returned a success result.
        /// </summary>
        Succeeded,
        /// <summary>
        /// Operation was terminated before returning a success or failure.
        /// </summary>
        Cancelled
    }

    /// <summary>
    /// Policy for what happens if the <see cref="Target"/> is invalidated (i.e. freed from memory).
    /// </summary>
    public enum InvalidPolicy
    {
        /// <summary>
        /// Ignore the fact the target is invalided and return a success status.
        /// </summary>
        Success,
        /// <summary>
        /// Fail when the target is invalidated.
        /// </summary>
        Fail
    }
}

/// <summary>
/// An attribute for defining a shortname of a class. This name is what
/// is used in an operation file. If none is specified, a fully qualified
/// name must be used.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited=false)]
public class OperationAttribute : Attribute
{

    public string Shorthand { get; }

    public OperationAttribute(string shorthand) => Shorthand = shorthand;
}