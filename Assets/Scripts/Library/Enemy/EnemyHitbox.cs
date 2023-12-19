public class EnemyHitbox : DamagingHitbox{
    protected new void Awake(){
        base.Awake();
        Damage = baseDamage * EnemyConfig.ENEMY_GLOBAL_DAMAGE_MULTIPLIER;
    }
}