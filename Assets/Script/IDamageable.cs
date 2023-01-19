public interface IDamageable
{
    /// <summary>
    /// Take base amount of damage
    /// </summary>
    public void TakeDamage();

    /// <summary>
    /// Take set amount of damage
    /// </summary>
    /// <param name="amount">how much damage</param>
    public void TakeDamage(int amount);
}
