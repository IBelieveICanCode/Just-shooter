namespace TestShooter.Shooting
{
    public interface ICanAttackable 
    {
        public IWeaponable CurrentGun { get; }
        public void UseCurrentWeapon();
    }
}
