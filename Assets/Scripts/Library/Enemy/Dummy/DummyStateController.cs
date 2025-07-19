using System.Collections;
using UnityEngine;
using static ConfigurationManager;

public class DummyStateController : DamageableEntityStateController<Dummy>
{

    private Dummy dummy;
    public DummyStateController(Dummy dummy) : base(dummy)
    {
        this.dummy = dummy;
    }

    protected override void OnDamaged()
    {
        damaged = true;
        dummy.StartCoroutine(DamagedDelay());
    }
    protected override void OnDeath()
    {
        Debug.Log("Dummy is dead");
    }

    public override int UpdateState()
    {
        return PlayerState.IDLE;
    }

    private IEnumerator DamagedDelay()
    {
        if (!dummy.Dead)
        {
            yield return new WaitForSeconds(ConfigurationManager.Instance.enemyConfig.DAMAGED_STATE_DURATION);
            damaged = false;
            invokeDamageDelayOver();
        }
    }
}
