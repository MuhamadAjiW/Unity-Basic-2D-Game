using System;

public interface IDamagingEntity{
    event Action OnDamage;
    public float GetDamage();
}
