using UnityEngine;

public abstract class Disaster:Destructible
{
    protected GameObject thisObj;
    protected GameObject partsObject;
    protected Rigidbody rb;
    protected bool destroyed;
    public Disaster(GameObject thisObj,GameObject partsObject,Rigidbody rb)
    {
        this.thisObj=thisObj;
        this.partsObject=partsObject;
        this.rb=rb;
        this.destroyed=false;

        if(this.partsObject!=null)
            this.partsObject.SetActive(false);
    }
    public virtual void impact()
    {
        Debug.Log("impact with disaster");
    }
    public virtual void launch()
    {
        Debug.Log("launch");
    }
}
