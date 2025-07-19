using System;

public interface IDamagingEntity
{
    float Damage { get; set; }
    event Action OnDamage;
}
