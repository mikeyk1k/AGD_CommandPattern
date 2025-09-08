using Command.Actions;
using Command.Main;

namespace Command.Commands
{
    public class AttackStanceCommand : UnitCommand
    {
        private bool willHitTarget;

        public AttackStanceCommand(CommandData commandData)
        {
            this.commandData = commandData;
            willHitTarget = WillHitTarget();
        }

        public override void Execute()
        {
            IAction action = GameService.Instance.ActionService.GetActionByType(CommandType.AttackStance);
            action.PerformAction(actorUnit, targetUnit, willHitTarget);
        }
        public override void Undo()
        {
            if (!willHitTarget) return;

            targetUnit.CurrentPower = (int)(targetUnit.CurrentPower / 1.2f);
            actorUnit.Owner.ResetCurrentActivePlayer();
        }

        public override bool WillHitTarget() => true;
    }
}