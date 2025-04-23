using UnityEngine;

public class MeteoriteDisaster:Disaster
{
    public MeteoriteDisaster(GameObject thisObj,GameObject partsObject,Rigidbody rb)
        :base(thisObj,partsObject,rb)
    {
    }
    public override void launch()
    {}
    /*
        the main function when the object hits something
    */
    public override void impact()
    {
        if(destroyed==true)
            return;
        destroyed=true;
        breakIntoParts();
        //create explosion
        GameObject.Destroy(thisObj);
    }
    private void breakIntoParts()
    {
        partsObject.SetActive(true);
        foreach(Transform t in partsObject.transform)
        {
            Rigidbody rbb=t.gameObject.GetComponent<Rigidbody>();
            //update with the parent velocity
            if(rbb)
            {
                rbb.linearVelocity=rb.linearVelocity;
                rbb.angularVelocity=rb.angularVelocity;
            }
        }
        MyUtil.instance.addToTrash(partsObject,20);
    }
}
