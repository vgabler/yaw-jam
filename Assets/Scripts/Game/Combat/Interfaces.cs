namespace Yaw.Game
{
    public interface ITakesDamage : ITeamEntity
    {
        void TakeDamage(int damage, ITeamEntity attacker);
    }
    public interface IAttacker : ITeamEntity
    {
        int Damage { get; set; }
        float Range { get; set; }

        void Attack();
    }

    public interface ITeamEntity
    {
        int Team { get; set; }
    }
}