using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandProcess {

    private static CommandProcess instance;
    public static CommandProcess Instance
    {
        get
        {
            if (instance == null) instance = new CommandProcess();
            return instance;
        }
    }

    private List<Command> commands = new List<Command>();

    public void ExecuteCommand(Command command)
    {
        if (hasCommandPrcessing) return;

        commands.Add(command);
        command.Execute(null);
    }

    public void UndoCommand()
    {
        if (commands.Count == 0) return;

        Command command = commands[commands.Count - 1];
        if (command.status == Command.Status.complete)
        {
            commands[commands.Count - 1].Undo(() =>
            {
                commands.RemoveAt(commands.Count - 1);
            });
        }
    }

    public bool hasCommandPrcessing
    {
        get
        {
            if (commands.Find((Command obj) => obj.status == Command.Status.processing) != null)
            {
                return true;
            }
            return false;
        }
    }

    public int CommandCount
    {
        get { return commands.Count; }
    }
}
