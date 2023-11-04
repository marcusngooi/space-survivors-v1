/*  Author's Name:          Mithul Koshy
 *  Last Modified By:       Marcus Ngooi
 *  Date Last Modified:     November 3, 2023
 *  Program Description:    Controls the player.
 *  Revision History:       (Mithul Koshy): Initial PlayerController script.
 *                          November 2, 2023 (Mithul Koshy): Integrated with PlayerTest scripts
 *                          November 3, 2023 (Marcus Ngooi): Added weapon and buff list and functions to add.
 *                                                           Made this script a Singleton.
 */

using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public float moveSpeed = 5f;
    public float currentHealth, maxHealth;
    public GameObject bulletPrefab;
    public Transform gunTransform;
    public float bulletSpeed = 10f;
    public float shootCooldown = 0.5f;
    public float bulletLifetime = 10f; // Time in seconds before bullets despawn
    private float lastShootTime;

    private Rigidbody2D rb;
    private Health playerHealth;
    public Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponentInChildren<Health>();
        playerHealth.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void Update()
    {
        // Player movement input
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        // Shooting
        if (Input.GetMouseButton(0) && Time.time - lastShootTime > shootCooldown)
        {
            Shoot();
        }

        if (currentHealth <= 0)
        {
            Debug.Log("GameOver");
        }
    }

    private void FixedUpdate()
    {
        // Move the player
        rb.velocity = movement.normalized * moveSpeed;

        // Rotate the player based on the movement direction
        if (movement.magnitude > 0)
        {
            float angle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, -angle);
        }

        // Update the camera's position to follow the player
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, 0.1f);
    }

    private void Shoot()
    {
        lastShootTime = Time.time;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = (mousePosition - gunTransform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = shootDirection * bulletSpeed;

        // Destroy the bullet after its lifetime expires
        Destroy(bullet, bulletLifetime);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            currentHealth--;
            playerHealth.UpdateHealthBar(currentHealth, maxHealth);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyProjectile"))
        {
            currentHealth--;
            playerHealth.UpdateHealthBar(currentHealth, maxHealth);
        }
    }
}
