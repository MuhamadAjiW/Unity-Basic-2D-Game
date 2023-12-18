using System;

public interface IDamageableEntity{
    event Action OnDeath;
    public bool IsDead();
    public float GetHealth();
    public float InflictDamage(float damage);
}
