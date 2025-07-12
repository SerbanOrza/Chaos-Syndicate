using System;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float damage = 1;
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Destructible"))
        {
            Destructible d = other.gameObject.GetComponent<Destructible>();
            if (d != null)
                d.impact(damage);
        }

        Destroy(gameObject);
    }
}
