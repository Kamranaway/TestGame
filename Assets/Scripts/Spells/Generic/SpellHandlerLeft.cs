using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHandlerLeft: SpellHandler
{
    public override void OnAwake()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        instantFire = playerControl.instantFireL;
        constantFire = playerControl.constantFireL;
    }
}
