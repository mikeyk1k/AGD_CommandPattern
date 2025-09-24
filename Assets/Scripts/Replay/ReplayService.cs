using Command.Commands;
using Command.Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command.Replay
{
    public class ReplayService
    {
        private Stack<ICommand> replayCommandStack;

        public ReplayState ReplayState { get; private set; }

        public ReplayService() => SetReplayState(ReplayState.DEACTIVE);

        public void SetReplayState(ReplayState stateToSet) => ReplayState = stateToSet;

        public void SetReplayCommands(Stack<ICommand> commandsToSet) => replayCommandStack = new Stack<ICommand>(commandsToSet);

        public IEnumerator ExecuteNext()
        {
            yield return new WaitForSeconds(1f);
            if (replayCommandStack.Count > 0)
            {
                ICommand commandToExecute = replayCommandStack.Pop();
                GameService.Instance.ProcessUnitCommand(commandToExecute);
            }
        }
    }
    public enum ReplayState
    {
        ACTIVE,
        DEACTIVE
    }
}