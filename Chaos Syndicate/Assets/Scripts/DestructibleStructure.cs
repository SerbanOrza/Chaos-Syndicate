using UnityEngine;

public class DestructibleStructure : MonoBehaviour,Destructible
{
    public GameObject parts;
    public DestructibleStructure[] destroyNext;
    void Start()
    {
        parts.SetActive(false);
    }
    public void impact()
    {
        //destroy
        parts.SetActive(true);
        //destroy other parts
        foreach(DestructibleStructure d in destroyNext)
            d.impact();
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision other)
    {
    }
}
