using System;

public interface IDamageableEntity{
    float Health {get; set;}
    bool Damageable {get;}
    bool Dead {get;}

    event Action OnDeath;
    event Action OnDamaged;
    
    float InflictDamage(float damage);
}
