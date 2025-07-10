using System;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleStructure : MonoBehaviour,Destructible
{
    private bool destroyed=false;
    public GameObject parts;
    public DestructibleStructure[] destroyNext;
    public Groups groups;
    public float hp=100.0f;
    public float resistance = 0.2f;
    public float amplifyDamageToParts = 0.75f;
    void Start()
    {
        destroyed=false;
        parts.SetActive(false);
    }

    public void impact(float damage)
    {
        if(destroyed)
            return;
        substractDamage(damage);
        if (hp > 0.0f)
            return;
        impact();
    }
    public void impact() //full damage
    {
        if(destroyed)
            return;
        destroyed=true;
        //remove from groups
        List<DestructibleStructure> obj_to_break=new List<DestructibleStructure>();
        if(groups)
            obj_to_break=groups.removeObjFromGroups(this);
        //destroy
        parts.SetActive(true);
        //damage next parts
        float furtherDamage = hp * amplifyDamageToParts;
        foreach(DestructibleStructure d in destroyNext)
            try
            {
                d.impact(furtherDamage);
            }
            catch (Exception e){}
        //destroy connected objects from groups
        foreach(DestructibleStructure d in obj_to_break)
            try
            {
                d.impact();
            }
            catch (Exception e){}

        foreach (Transform p in parts.transform)
            MyUtil.instance.addToTrash(p.gameObject, 25);//not always a timer. change this to a custom destroyer that check if it is a des. str.
        Destroy(gameObject);
    }

    public void substractDamage(float damage)
    {
        if (hp <= 0)
            return;
        hp -= damage * (1.0f - resistance);
        if (hp <= 0)
            hp = 0;
    }

}
