using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHandlerRight: SpellHandler
{
    public override void OnAwake()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        instantFire = playerControl.instantFireR;
        constantFire = playerControl.constantFireR;
        stats = FindObjectOfType<PlayerStats>();
    }
}
