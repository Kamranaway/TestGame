using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpell : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float forceModifier = 2f;
    [SerializeField] float projectileLife = 2f;
    GameObject castPoint;
    Crosshair crosshair;

    /*public void Init()
    {
        this.castPoint = GameObject.Find("Char_Center");
        this.crosshair = FindObjectOfType<Crosshair>();
    }*/

    private void Start()
    {
        this.castPoint = GameObject.Find("Char_Center");
        this.crosshair = FindObjectOfType<Crosshair>();
    }

    public void Cast()
    {
        GameObject firedProjectile = Instantiate(projectile, castPoint.transform.position, castPoint.transform.rotation);
        Rigidbody2D rb2d = firedProjectile.GetComponent<Rigidbody2D>();
        Destroyer destroyer = firedProjectile.GetComponent<Destroyer>();
        destroyer.projectile = firedProjectile;
        destroyer.projectileLife = this.projectileLife;
        float theta = Mathf.Deg2Rad * crosshair.angleToPlayer;
        float x = castPoint.transform.position.x;
        float y = castPoint.transform.position.y;
        Vector2 forceAngle = new Vector2(Mathf.Cos(theta) * forceModifier, Mathf.Sin(theta) * forceModifier);

        rb2d.AddForce(forceAngle, ForceMode2D.Impulse);
    }
}
