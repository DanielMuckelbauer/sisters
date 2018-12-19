namespace Code.Classes.CombatController
{
    public interface ICombatController
    {
        void ReceiveHit();

        int CurrentLife { get; set; }
    }
}