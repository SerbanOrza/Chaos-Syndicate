using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class PlayerInteraction : MonoBehaviour
{
    public GameObject testObj;
    void Start()
    {
        
    }

    void Update()
    {
        if(IsLeftMouseButtonDown())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);// it creates the ray with direction 
            RaycastHit hit;
            //Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 20);

            if(Physics.Raycast(ray, out hit,1000)) //max distance allowed is 1000
            {
                // position is hit.point
                Instantiate(testObj,hit.point,Quaternion.identity);
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
