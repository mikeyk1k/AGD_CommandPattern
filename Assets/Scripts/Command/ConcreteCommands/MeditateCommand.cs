using Command.Actions;
using Command.Main;

namespace Command.Commands
{
    public class MeditateCommand : UnitCommand
    {
        private bool willHitTarget;

        public MeditateCommand(CommandData commandData)
        {
            this.commandData = commandData;
            willHitTarget = WillHitTarget();
        }

        public override void Execute()
        {
            IAction action = GameService.Instance.ActionService.GetActionByType(CommandType.Meditate);
            action.PerformAction(actorUnit, targetUnit, willHitTarget);
        }

        public override void Undo()
        {
            if (willHitTarget)
            {
                int healthToDecrease = (int)(targetUnit.CurrentMaxHealth / 1.2f * 0.2f);
                targetUnit.CurrentMaxHealth -= healthToDecrease;
                targetUnit.TakeDamage(healthToDecrease);
            }
            actorUnit.Owner.ResetCurrentActivePlayer();
        }
        public override bool WillHitTarget() => true;
    }
}