using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Complete();

public abstract class Command{

    protected IAction action;
    public Status status { get; protected set; }

    public Command(IAction action)
    {
        this.action = action;
    }

    public abstract void Execute(Complete complete);
    public abstract void Undo(Complete complete);

    public enum Status
    {
        complete,
        processing
    }

    public enum Type
    {
        none
    }
}
