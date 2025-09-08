using Command.Actions;
using Command.Main;

namespace Command.Commands
{
    public class HealCommand : UnitCommand
    {
        private bool willHitTarget;

        public HealCommand(CommandData commandData)
        {
            this.commandData = commandData;
            willHitTarget = WillHitTarget();
        }

        public override void Execute()
        {
            IAction action = GameService.Instance.ActionService.GetActionByType(CommandType.Heal);
            action.PerformAction(actorUnit, targetUnit, willHitTarget);
        }

        public override bool WillHitTarget() => true;
    }
}