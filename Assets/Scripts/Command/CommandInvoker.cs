using Command.Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command.Commands
{
    public class CommandInvoker
    {
        private Stack<ICommand> commandRegistry = new Stack<ICommand>();
        private bool RegistryEmpty => commandRegistry.Count == 0;
        private bool CommandBelongsToActivePlayer()
        {
            return (commandRegistry.Peek() as UnitCommand).commandData.ActorPlayerID == GameService.Instance.PlayerService.ActivePlayerID;
        }

        public void ProcessCommand(ICommand commandToProcess)
        {
            ExecuteCommand(commandToProcess);
            RegisterCommand(commandToProcess);
        }

        public void ExecuteCommand(ICommand command) => command.Execute();
        public void RegisterCommand(ICommand command) => commandRegistry.Push(command);
        public void UndoLastCommand()
        {
            if (RegistryEmpty) return;
            if(!CommandBelongsToActivePlayer()) return;
            ICommand lastCommand = commandRegistry.Pop();
            lastCommand.Undo();
        }
    }
}