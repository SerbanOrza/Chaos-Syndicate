using System.Collections.Generic;
using UnityEngine;

public class ObjectsShield : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<LevitatingObject> objects;
    public Transform positions;
    
    public Rigidbody cubeRb;
    public Transform targetPosition;
    public float springStrength = 25f;
    public float dampingStrength = 2f;
    public float responsiveness = 1;
    void Start()
    {
        Debug.Log("started");
        int i = 0;
        foreach(LevitatingObject g in objects)
        {
            g.setVariables(positions.GetChild(i), springStrength, dampingStrength, responsiveness);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ApplyLevitationForce(cubeRb, targetPosition.position, springStrength, dampingStrength);
        // int i = 0;
        // foreach(LevitatingObject g in objects)
        // {
        //     g.ApplyLevitationForce(rb, positions.GetChild(i).position, springStrength, dampingStrength,responsiveness);
        //     i++;
        // }
    }
    public static void ApplyLevitationForce(Rigidbody rb, Vector3 targetPosition, float springStrength, float dampingStrength, float responsiveness)
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
    // public static void ApplyLevitationForce(Rigidbody rb, Vector3 currentPosition, Vector3 targetPosition, float mass, float strength, float damping)
    // {
    //     if (rb == null) return;
    //
    //     Vector3 displacement = targetPosition - currentPosition;
    //     Vector3 velocity = rb.linearVelocity;
    //
    //     // Force to move toward target
    //     Vector3 springForce = displacement * strength;
    //
    //     // Damping to reduce oscillations
    //     Vector3 dampingForce = -velocity * damping;
    //
    //     // Compensate for gravity so the object can float
    //     Vector3 antiGravity = -Physics.gravity * mass;
    //
    //     // Combine all forces
    //     Vector3 totalForce = springForce + dampingForce + antiGravity;
    //
    //     rb.AddForce(totalForce, ForceMode.Force);
    // }
}
