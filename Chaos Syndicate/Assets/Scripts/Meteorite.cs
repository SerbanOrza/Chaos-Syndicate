using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public Disaster disaster;
    public GameObject partsObject;
    public Rigidbody rb;
    void Awake()
    {
        //prepare the meteorite
        if(rb==null)
            rb=gameObject.GetComponent<Rigidbody>();
        disaster=new MeteoriteDisaster(gameObject,partsObject,rb);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision other)
    {
        disaster.impact();
    }
}
