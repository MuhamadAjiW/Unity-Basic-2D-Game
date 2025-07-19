using System;

public interface IDamageableEntity
{
    float MaxHealth { get; set; }
    float Health { get; set; }
    bool Damageable { get; }
    bool Dead { get; }

    event Action OnDeath;
    event Action OnDamaged;
    event Action OnHeal;

    float InflictDamage(float damage);
    float InflictHeal(float heal);
}
