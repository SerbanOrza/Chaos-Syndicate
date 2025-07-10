using System;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleStructure : MonoBehaviour,Destructible
{
    private bool destroyed=false;
    public GameObject parts;
    public DestructibleStructure[] destroyNext;
    public Groups groups;
    void Start()
    {
        destroyed=false;
        parts.SetActive(false);
    }
    public void impact()
    {
        if(destroyed)
            return;
        destroyed=true;
        //remove from groups
        List<DestructibleStructure> obj_to_break=new List<DestructibleStructure>();
        if(groups!=null)
            obj_to_break=groups.removeObjFromGroups(this);
        //destroy
        parts.SetActive(true);
        //destroy other parts
        foreach(DestructibleStructure d in destroyNext)
            try
            {
                d.impact();
            }
            catch (Exception e){}
        //destroy other objects
        foreach(DestructibleStructure d in obj_to_break)
            try
            {
                d.impact();
            }
            catch (Exception e){}

        foreach(Transform p in parts.transform)
            MyUtil.instance.addToTrash(p.gameObject,15);
        Destroy(gameObject);
    }
}
