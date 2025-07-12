using UnityEngine;

public class ChaosController : MonoBehaviour
{
    public static ChaosController instance;
    public float totalChaos=0;
    private float chaosToAdd = 0;
    public ParticleSystem chaosParticles;
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

    // Update is called once per frame
    void Update()
    {
        if (chaosToAdd > 0)
        {
            chaosToAdd--;
            totalChaos++;
        }
    }
    
    public void addChaos(float chaos, Vector3 position)
    {
        chaosToAdd+=chaos;
        ParticleSystem p = Instantiate(chaosParticles.gameObject, position, Quaternion.identity).GetComponent<ParticleSystem>();
        p.gameObject.SetActive(true);
        p.Play();
        Destroy(p, 3);
    }
}
