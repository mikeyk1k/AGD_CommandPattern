using Command.Actions;
using Command.Main;

namespace Command.Commands
{
    public class MeditateCommand : UnitCommand
    {
        private bool willHitTarget;
        private int previousMaxHealth;

        public MeditateCommand(CommandData commandData)
        {
            this.commandData = commandData;
            willHitTarget = WillHitTarget();
        }

        public override void Execute()
        {
            previousMaxHealth = targetUnit.CurrentMaxHealth;
            IAction action = GameService.Instance.ActionService.GetActionByType(CommandType.Meditate);
            action.PerformAction(actorUnit, targetUnit, willHitTarget);
        }

        public override void Undo()
        {
            if (willHitTarget)
            {
                int healthToDecrease = targetUnit.CurrentMaxHealth - previousMaxHealth;
                targetUnit.CurrentMaxHealth = previousMaxHealth;
                targetUnit.TakeDamage(healthToDecrease);
            }
            actorUnit.Owner.ResetCurrentActivePlayer();
        }
        public override bool WillHitTarget() => true;
    }
}