using UnityEngine;

public class MyUtil : MonoBehaviour
{
    public static MyUtil instance;
    public Transform trashPoint;
    public Transform allGroupsPoint;
    public LayerMask destructibleLayerMask;
    void Awake()
    {
        if(instance==null)
            instance=this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void addToTrash(GameObject g,float t)
    {
        Destroy(g,t);
        g.transform.SetParent(trashPoint);
    }
    public void addGroupToParent(GameObject g)
    {
        g.transform.SetParent(allGroupsPoint);
    }
    public void applyExplosionForce(Vector3 expPos,float expRadiusSmall,float expRadiusBig,float expForce,float damage=0)
    {
        //there are 2 waves.
        //First one is for detecting what to break.
        //The second one is for just applying a force (they are not destroyed)
        //Obs: in wave 1 wr do not apply any force yet

        //first wave
        Collider[] colliders=Physics.OverlapSphere(expPos,expRadiusSmall,destructibleLayerMask);
        foreach(Collider near in colliders)
        {
            Rigidbody r=near.GetComponentInParent<Rigidbody>();
            if(r!=null)
            {
                //if obj is a struct, break it
                Destructible d=near.GetComponentInParent<Destructible>();
                if(d!=null)
                {
                    Debug.Log("detected a destructible");
                    d.impact(damage);
                }
            }
        }
        //second wave
        colliders=Physics.OverlapSphere(expPos,expRadiusBig);
        foreach(Collider near in colliders)
        {
            Rigidbody r=near.GetComponentInParent<Rigidbody>();
            if(r!=null)
            {
                r.AddExplosionForce(expForce,expPos,expRadiusBig);
            }
        }
    }
}
