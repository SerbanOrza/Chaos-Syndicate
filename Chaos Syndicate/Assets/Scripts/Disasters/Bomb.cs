using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Disaster disaster;
    public GameObject partsObject;
    public Rigidbody rb;
    public GameObject[] particles;
    public float expRadiusSmall,expRadiusBig,expForce;
    public float t=3;
    void Awake()
    {
        if(rb==null)
            rb=gameObject.GetComponent<Rigidbody>();

        DisasterData data=ScriptableObject.CreateInstance<DisasterData>();
        data.initialize(false,expRadiusSmall,expRadiusBig,expForce);

        disaster=new BombDisaster(gameObject,partsObject,rb,particles,data);
    }
    void Start()
    {
        
    }

    void Update()
    {
        t-=Time.deltaTime;
        if(t<0)
            disaster.impact();
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
