using Command.Actions;
using Command.Main;
using UnityEngine;

namespace Command.Commands
{
    public class CleanseCommand : UnitCommand
    {
        private bool willHitTarget;
        private const float hitChance = 0.2f;

        public CleanseCommand(CommandData commandData)
        {
            this.commandData = commandData;
            willHitTarget = WillHitTarget();
        }

        public override void Execute()
        {
            IAction action = GameService.Instance.ActionService.GetActionByType(CommandType.Cleanse);
            action.PerformAction(actorUnit, targetUnit, willHitTarget);
        }

        public override bool WillHitTarget() => Random.Range(0f, 1f) < hitChance;
    }
}