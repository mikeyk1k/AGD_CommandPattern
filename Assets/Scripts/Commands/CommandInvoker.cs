using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker
{
    private Stack<ICommand> commandRegistry = new Stack<ICommand>();

    public void ProcessCommand(ICommand commandToProcess)
    {
        ExecuteCommand(commandToProcess);
        RegisterCommand(commandToProcess);
    }

    public void ExecuteCommand(ICommand command) => command.Execute();
    public void RegisterCommand(ICommand command) => commandRegistry.Push(command);
}