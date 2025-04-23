using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public Disaster disaster;
    public GameObject partsObject;
    public Rigidbody rb;
    public GameObject[] particles;
    void Awake()
    {
        //prepare the meteorite
        if(rb==null)
            rb=gameObject.GetComponent<Rigidbody>();
        disaster=new MeteoriteDisaster(gameObject,partsObject,rb,particles);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void launch(float time,Vector3 targetPos)
    {
        disaster.launch(time,targetPos);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        disaster.impact();
    }
}
