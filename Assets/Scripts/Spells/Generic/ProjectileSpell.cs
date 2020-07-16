using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileSpell : Spell
{
    public void Cast(GameObject projectile, float forceModifier)
    {
        GameObject firedProjectile = Instantiate(projectile, castPoint.transform.position, castPoint.transform.rotation);
        Rigidbody2D rb2d = firedProjectile.GetComponent<Rigidbody2D>();
        float theta = Mathf.Deg2Rad * crosshair.angleToPlayer;
        float x = castPoint.transform.position.x;
        float y = castPoint.transform.position.y;
        Vector2 forceAngle = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));

        rb2d.AddForce(forceAngle * forceModifier, ForceMode2D.Impulse);
    }

    public override void Init()
    {
        throw new System.NotImplementedException();
    }

}
