using Command.Actions;
using Command.Main;

namespace Command.Commands
{
    public class ThirdEyeCommand : UnitCommand
    {
        private bool willHitTarget;

        public ThirdEyeCommand(CommandData commandData)
        {
            this.commandData = commandData;
            willHitTarget = WillHitTarget();
        }

        public override void Execute()
        {
            IAction action = GameService.Instance.ActionService.GetActionByType(CommandType.ThirdEye);
            action.PerformAction(actorUnit, targetUnit, willHitTarget);
        }

        public override bool WillHitTarget() => true;
    }
}