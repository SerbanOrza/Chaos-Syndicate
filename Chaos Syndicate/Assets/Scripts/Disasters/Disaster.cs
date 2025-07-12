using UnityEngine;

public abstract class Disaster:Destructible
{
    protected GameObject thisObj;
    protected GameObject partsObject;
    protected Rigidbody rb;
    protected GameObject[] particles;//fire tails

    protected bool destroyed;
    protected DisasterData data;
    protected Vector3 initialSpeed;
    public Disaster(GameObject thisObj,GameObject partsObject,Rigidbody rb,GameObject[] particles,DisasterData data)
    {
        this.thisObj=thisObj;
        this.partsObject=partsObject;
        this.rb=rb;
        this.particles=particles;
        this.data=data;
        this.destroyed=false;

        if(this.partsObject!=null)
            this.partsObject.SetActive(false);
    }
    public virtual void impact()
    {
        Debug.Log("impact with disaster");
    }
    public virtual void impact(float damage)
    {
        impact();
    }
    public virtual void launch(float time,Vector3 targetPos)
    {
        Debug.Log("launch");
    }
}
