public class EnemyHitbox : DamagingHitbox{
    protected new void Awake(){
        base.Awake();
        Damage = baseDamage * EnemyConfig.GLOBAL_DAMAGE_MULTIPLIER;
    }
}