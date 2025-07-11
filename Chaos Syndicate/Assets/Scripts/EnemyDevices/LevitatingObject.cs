using UnityEngine;

public class LevitatingObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody rb;
    public Transform position;
    public float t = 1;
    private float t2 = 0; //timer between accepting 2 collisions
    public bool desactivated = false;
    
    private float springStrength = 25f;
    private float dampingStrength = 2f;
    private float responsiveness = 1;

    void Start()
    {
        if(rb == null)
            rb=GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (desactivated)
            return;
        if (t2 > 0)
            t2 -= Time.deltaTime;
        if (t <= 0)
            ApplyLevitationForce(position.position, springStrength, dampingStrength, responsiveness);
        else
            t -= Time.deltaTime;
    }

    // Update is called once per frame
    public void ApplyLevitationForce(Vector3 targetPosition, float springStrength, float dampingStrength, float responsiveness)
    {
        if (rb == null) return;

        Vector3 displacement = targetPosition - rb.position;
        Vector3 relativeVelocity = rb.linearVelocity;

        // Spring force pulls toward target
        Vector3 v = displacement;
        if (v.magnitude > 1)
            v = displacement.normalized;
        float distanceFactor = Mathf.Clamp01(displacement.magnitude / 2f); // Smooth fade below 2 units
        float scaledSpring = springStrength * distanceFactor * responsiveness;
        float scaledDamping = dampingStrength * distanceFactor * responsiveness;
        
        Vector3 springForce = v * (scaledSpring * rb.mass);
        Vector3 dampingForce = -relativeVelocity * (scaledDamping * rb.mass);

        Vector3 totalForce = springForce + dampingForce;

        rb.AddForce(totalForce, ForceMode.Force);
    }

    public void setVariables(Transform position, float springStrength, float dampingStrength, float responsiveness)
    {
        this.position = position;
        this.springStrength = springStrength;
        this.dampingStrength = dampingStrength;
        this.responsiveness = responsiveness;
    }

    public void desactivateThisObj()
    {
        desactivated = true;
        t2 = 2;
        t = 0;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponentInParent<LevitatingObject>() != null)
            return;
        if (t2 > 0)
            return;
        t2 = 2;
        t = 3;
    }
    
}
