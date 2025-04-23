using UnityEngine;

public class MyUtil : MonoBehaviour
{
    public static MyUtil instance;
    public Transform trashPoint;
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
}
