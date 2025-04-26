using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public Disaster disaster;
    public GameObject partsObject;
    public Rigidbody rb;
    public GameObject[] particles;
    public float expRadiusSmall,expRadiusBig;
    void Awake()
    {
        //prepare the meteorite
        float scale=Random.Range(0.5f, 2.0f);
        transform.localScale=new Vector3(scale,scale,scale);
        if(rb==null)
            rb=gameObject.GetComponent<Rigidbody>();

        DisasterData data=ScriptableObject.CreateInstance<DisasterData>();
        data.initialize(false,expRadiusSmall,expRadiusBig);

        disaster=new MeteoriteDisaster(gameObject,partsObject,rb,particles,data);
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
