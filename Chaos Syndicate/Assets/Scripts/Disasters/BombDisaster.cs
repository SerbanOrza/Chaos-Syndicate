using UnityEngine;

public class BombDisaster:Disaster
{
    public BombDisaster(GameObject thisObj,GameObject partsObject,Rigidbody rb,GameObject[] particles,DisasterData data)
        :base(thisObj,partsObject,rb,particles,data)
    {
    }
    // public override void launch(float time,Vector3 targetPos)
    // {
    // }
    /*
        the main function when the object hits something
    */
    public override void impact()
    {
        if(destroyed)
            return;
        destroyed=true;
        //remove tails
        foreach(GameObject g in particles)
            MyUtil.instance.addToTrash(g,15);
        //create explosion
        MyUtil.instance.applyExplosionForce(thisObj.transform.position,data.expRadiusSmall,data.expRadiusBig,data.expForce,data.damage);
        GameObject.Destroy(thisObj);
    }
}
