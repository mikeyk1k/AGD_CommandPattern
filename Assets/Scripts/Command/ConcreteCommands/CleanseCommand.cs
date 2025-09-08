using Command.Actions;
using Command.Main;
using UnityEngine;

namespace Command.Commands
{
    public class CleanseCommand : UnitCommand
    {
        private bool willHitTarget;
        private const float hitChance = 0.2f;
        private int previousPower;

        public CleanseCommand(CommandData commandData)
        {
            this.commandData = commandData;
            willHitTarget = WillHitTarget();
        }

        public override void Execute()
        {
            previousPower = targetUnit.CurrentPower;
            IAction action = GameService.Instance.ActionService.GetActionByType(CommandType.Cleanse);
            action.PerformAction(actorUnit, targetUnit, willHitTarget);
        }

        public override void Undo()
        {
            // No undo functionality for CleanseCommand as it resets stats.
            if(willHitTarget)
                targetUnit.CurrentPower = previousPower;
            actorUnit.Owner.ResetCurrentActivePlayer();
        }

        public override bool WillHitTarget() => Random.Range(0f, 1f) < hitChance;
    }
}