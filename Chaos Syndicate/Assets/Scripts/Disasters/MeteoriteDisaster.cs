using UnityEngine;

public class MeteoriteDisaster:Disaster
{
    public MeteoriteDisaster(GameObject thisObj,GameObject partsObject,Rigidbody rb,GameObject[] particles,DisasterData data)
        :base(thisObj,partsObject,rb,particles,data)
    {
    }
    public override void launch(float time,Vector3 targetPos)
    {
        Vector3 launchPos=thisObj.transform.position;
        if(time<1f)
            time=1f;
        //prepare the force
        Vector3 force;
        force.x=(targetPos.x-launchPos.x)/time;
        force.y=(targetPos.y-launchPos.y)/time+9.8F*time/2;
        force.z=(targetPos.z-launchPos.z)/time;

        rb.AddForce(force*rb.mass,ForceMode.Impulse);
        initialSpeed=force/rb.mass;

        //add random rotation force
        Vector3 randomTorque=new Vector3(
            Random.Range(-1f,1f),
            Random.Range(-1f,1f),
            Random.Range(-1f,1f)).normalized*Random.Range(70f,400);

        rb.AddTorque(randomTorque,ForceMode.Impulse);
    }
    /*
        the main function when the object hits something
    */
    public override void impact()
    {
        if(destroyed==true)
            return;
        destroyed=true;
        breakIntoParts();
        //remove tails
        foreach(GameObject g in particles)
            MyUtil.instance.addToTrash(g,15);
        //create explosion
        float velocityMagnitude=rb.linearVelocity.magnitude;
        float kineticEnergy=0.5f*rb.mass*velocityMagnitude*velocityMagnitude;
        float explosionForce=kineticEnergy*0.35f;//0.35 is a multiplier
        MyUtil.instance.applyExplosionForce(thisObj.transform.position,data.expRadiusSmall,data.expRadiusBig,explosionForce,data.damage);
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
