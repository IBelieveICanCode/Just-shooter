namespace TestShooter.Shooting
{
    public interface ICanShootable 
    {
        public IGunable CurrentGun { get; }
        public void UseCurrentWeapon();
    }
}
