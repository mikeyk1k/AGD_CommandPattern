using Command.Actions;
using Command.Main;

namespace Command.Commands
{
    public class ThirdEyeCommand : UnitCommand
    {
        private bool willHitTarget;
        private int previousHealth;

        public ThirdEyeCommand(CommandData commandData)
        {
            this.commandData = commandData;
            willHitTarget = WillHitTarget();
        }

        public override void Execute()
        {
            previousHealth = targetUnit.CurrentPower;
            IAction action = GameService.Instance.ActionService.GetActionByType(CommandType.ThirdEye);
            action.PerformAction(actorUnit, targetUnit, willHitTarget);
        }

        public override void Undo()
        {
            if(!targetUnit.IsAlive())
                targetUnit.Revive();
            int healthToRevert = (int)(previousHealth * 0.25f);
            targetUnit.CurrentPower -= healthToRevert;
            targetUnit.RestoreHealth(healthToRevert);
            actorUnit.Owner.ResetCurrentActivePlayer();
        }
        public override bool WillHitTarget() => true;
    }
}