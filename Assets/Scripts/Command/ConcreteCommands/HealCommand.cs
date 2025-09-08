using Command.Actions;
using Command.Main;

namespace Command.Commands
{
    public class HealCommand : UnitCommand
    {
        private bool willHitTarget;
        private int healingAmount;

        public HealCommand(CommandData commandData)
        {
            this.commandData = commandData;
            willHitTarget = WillHitTarget();
        }

        public override void Execute()
        {
            IAction action = GameService.Instance.ActionService.GetActionByType(CommandType.Heal);
            healingAmount = targetUnit.CurrentHealth + actorUnit.CurrentPower > targetUnit.CurrentMaxHealth 
                ? targetUnit.CurrentMaxHealth - targetUnit.CurrentHealth 
                : actorUnit.CurrentPower;
            action.PerformAction(actorUnit, targetUnit, willHitTarget);
        }

        public override void Undo()
        {
            if (!willHitTarget) return;
            targetUnit.TakeDamage(healingAmount);
            actorUnit.Owner.ResetCurrentActivePlayer();
        }
        public override bool WillHitTarget() => true;
    }
}