using UnityEngine;

public class DestructibleStructure : MonoBehaviour,Destructible
{
    private bool destroyed=false;
    public GameObject parts;
    public DestructibleStructure[] destroyNext;
    void Start()
    {
        destroyed=false;
        parts.SetActive(false);
    }
    public void impact()
    {
        if(destroyed)
            return;
        destroyed=true;
        //destroy
        parts.SetActive(true);
        //destroy other parts
        foreach(DestructibleStructure d in destroyNext)
            d.impact();
        foreach(Transform p in parts.transform)
            MyUtil.instance.addToTrash(p.gameObject,15);
        Destroy(gameObject);
    }
}
