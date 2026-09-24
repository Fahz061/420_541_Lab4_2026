
using UnityEngine;

public class GunComponent : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletMaxImpulse = 100.0f;
    public float maxChargeTime = 3.0f;

    private float chargeTime = 0.0f;
    private bool isCharging = false;

    void Update()
    {
        // Start charging when the player presses Fire1.
        if (Input.GetButtonDown("Fire1"))
        {
            chargeTime = 0.0f;
            isCharging = true;
        }

        // Increase charge while holding the button.
        if (isCharging && Input.GetButton("Fire1"))
        {
            chargeTime += Time.deltaTime;
            chargeTime = Mathf.Clamp(
                chargeTime, 0, maxChargeTime
            );
        }

        // Shoot when the player releases the button.
        if (isCharging && Input.GetButtonUp("Fire1"))
        {
            ShootBullet();
            isCharging = false;
        }
    }

    void ShootBullet()
    {
        GameObject bullet = Instantiate(
            bulletPrefab,
            bulletSpawnPoint.position,
            bulletSpawnPoint.rotation
        );

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // Scale bullet force based on charge time.
        float bulletImpulse =
            (chargeTime / maxChargeTime) * bulletMaxImpulse;

        rb.AddForce(
            bulletSpawnPoint.forward * bulletImpulse,
            ForceMode.Impulse
        );
    }
}