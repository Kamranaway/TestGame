using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/LightCharge")]
public class LightCharge: ChargeSpell
{
    [SerializeField] GameObject projectile;
    [SerializeField] float forceModifier = 2f;

    public override void Cast()
    {
        GameObject firedProjectile = Instantiate(projectile, castPoint.transform.position, castPoint.transform.rotation);
        Rigidbody2D rb2d = firedProjectile.GetComponent<Rigidbody2D>();
        Destroyer destroyer = firedProjectile.GetComponent<Destroyer>();
        destroyer.projectile = firedProjectile;
        destroyer.projectileLife = duration;
        float theta = Mathf.Deg2Rad * crosshair.angleToPlayer;
        float x = castPoint.transform.position.x;
        float y = castPoint.transform.position.y;
        Vector2 forceAngle = new Vector2(Mathf.Cos(theta) * forceModifier, Mathf.Sin(theta) * forceModifier);

        rb2d.AddForce(forceAngle, ForceMode2D.Impulse);
    }

    public override void Init()
    {
        this.castPoint = GameObject.Find("Char_Center");
        this.crosshair = FindObjectOfType<Crosshair>();
    }
}

