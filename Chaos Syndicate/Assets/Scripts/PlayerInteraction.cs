using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class PlayerInteraction : MonoBehaviour
{
    public GameObject testObj;
    public Transform target;
    public CinematicCameraFollow cm;
    void Start()
    {
        
    }

    void Update()
    {
        if(IsLeftMouseButtonDown())
        {
            Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);// it creates the ray with direction 
            RaycastHit hit;
            //Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 20);

           if(Physics.Raycast(ray,out hit,1000)) //max distance allowed is 1000
            {
                // position is hit.point
                Vector3 pos=new Vector3(Random.Range(100f,201f),Random.Range(100f, 201f),Random.Range(100f, 201f));
                GameObject x=Instantiate(testObj,pos,Quaternion.identity);
                Meteorite m=x.GetComponent<Meteorite>();
                cm.target=x.transform;
                if(m!=null)
                    m.launch(5,new Vector3(0,0,0));//hit.point);
            }
        }
    }
    bool IsLeftMouseButtonDown()
    {
        #if ENABLE_INPUT_SYSTEM
            return Mouse.current != null ? Mouse.current.leftButton.wasPressedThisFrame  : false;
        #else
            return Input.GetMouseButtonDown(0);
        #endif
    }
}
