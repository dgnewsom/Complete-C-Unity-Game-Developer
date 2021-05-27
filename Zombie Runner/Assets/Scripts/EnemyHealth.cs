using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthScript
{
    internal override void DeathBehaviour()
    {
        base.DeathBehaviour();
        Destroy(gameObject, 1f);
    }

}
