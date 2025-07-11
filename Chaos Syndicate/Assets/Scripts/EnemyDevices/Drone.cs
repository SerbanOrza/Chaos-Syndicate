using System;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public Transform target;
    public Rigidbody rb;
    public Transform cannon;
    public GameObject projectilePrefab;
    public Transform shootPos;
    public float projectileForce = 30f;   // Launch force
    public float shootCooldown = 0.1f;      // Time between shots

    private float lastShootTime = -Mathf.Infinity;
    public float flyForce = 15f;        // How strong the drone moves toward target
    public float maxSpeed = 10f;        // Maximum movement speed
    public float turnSpeed = 3f;        // How fast the drone turns to face target
    public float cannonAimSpeed = 6f;   // How fast the cannon rotates
    private float tCrash = 0, t2=0;
    
    void FixedUpdate()
    {
        if (!target || !rb) return;
        if (tCrash <= 0)
            FlyTowardTarget();
        RotateTowardsTarget();
        AimCannon();
    }
    void Update()
    {
        if (t2 > 0)
            t2 -= Time.deltaTime;
        if (tCrash > 0)
            tCrash -= Time.deltaTime;
        if (!target || !rb) return;
        
        TryShoot();
    }

    void FlyTowardTarget()
    {
        Vector3 toTarget = target.position - transform.position;
        Vector3 direction = toTarget.normalized;

        Vector3 gravityComp = -Physics.gravity;

        Vector3 desiredVelocity = direction * maxSpeed;
        Vector3 velocityError = desiredVelocity - rb.linearVelocity;

        Vector3 desiredAccel = velocityError * 1.5f + gravityComp;
        desiredAccel = Vector3.ClampMagnitude(desiredAccel, flyForce); // avoid extreme forces

        rb.AddForce(desiredAccel * rb.mass, ForceMode.Force);
    }
    // void FlyTowardTarget2()
    // {
    //     Vector3 toTarget = target.position - transform.position;
    //     Vector3 direction = toTarget.normalized;
    //
    //     // --- Vertical lift to stay airborne ---
    //     Vector3 gravityComp = -Physics.gravity * rb.mass;  // Counteract gravity
    //     Vector3 verticalLift = Vector3.up * flyForce * rb.mass; // Extra lift for flying
    //
    //     // --- Horizontal movement towards target ---
    //     Vector3 horizontalDir = new Vector3(direction.x, 0f, direction.z).normalized;
    //     Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    //
    //     float horizontalSpeedError = maxSpeed - horizontalVelocity.magnitude;
    //     Vector3 horizontalForce = horizontalDir * (horizontalSpeedError * flyForce);
    //
    //     // Combine
    //     Vector3 totalForce = gravityComp + verticalLift + horizontalForce;
    //
    //     // Apply
    //     rb.AddForce(totalForce, ForceMode.Force);
    // }

    void RotateTowardsTarget()
    {
        Vector3 targetDirection = (target.position - transform.position).normalized;
        Vector3 flatDirection = new Vector3(targetDirection.x, 0f, targetDirection.z);

        if (flatDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(flatDirection, Vector3.up);
            Quaternion smoothedRotation = Quaternion.Slerp(rb.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(smoothedRotation);
        }
    }

    void AimCannon()
    {
        if (!cannon) return;

        Vector3 aimDir = (target.position - cannon.position).normalized;
        Quaternion desiredRotation = Quaternion.LookRotation(aimDir, Vector3.up);
        cannon.rotation = Quaternion.Slerp(cannon.rotation, desiredRotation, cannonAimSpeed * Time.deltaTime);
    }
    void TryShoot()
    {
        if (Time.time - lastShootTime < shootCooldown) return;
        if (!projectilePrefab || !shootPos) return;

        lastShootTime = Time.time;

        GameObject proj = Instantiate(projectilePrefab, shootPos.position, shootPos.rotation);
    
        Rigidbody projRb = proj.GetComponent<Rigidbody>();
        if (projRb)
        {
            projRb.linearVelocity = rb.linearVelocity; // inherit drone velocity
            projRb.AddForce(shootPos.forward * projectileForce, ForceMode.VelocityChange);
        }
        Destroy(proj,10);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if (t2 > 0)
            return;
        t2 = 1f;
        tCrash = 0.1f;
    }
}