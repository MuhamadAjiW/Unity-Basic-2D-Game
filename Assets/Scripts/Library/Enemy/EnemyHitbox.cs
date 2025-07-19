public class EnemyHitbox : DamagingHitbox
{
    protected void Awake()
    {
        Damage *= EnemyConfig.GLOBAL_DAMAGE_MULTIPLIER;
    }
}