/*  Author's Name:          Marcus Ngooi
 *  Last Modified By:       Marcus Ngooi
 *  Date Last Modified:     November 3, 2023
 *  Program Description:    A class representing a laser gun.
 *  Revision History:       November 3, 2023 (Marcus Ngooi): Initial LaserGun script.
 */

using System.Collections;
using UnityEngine;

public class LaserGun : Weapon
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform gunTransform;
    [SerializeField] private bool isActive;

    private const string ProjectileSpawner = "ProjectileSpawner";

    private void Start()
    {
        isActive = false;
        gunTransform = GameObject.FindWithTag(ProjectileSpawner).GetComponent<Transform>();
        weaponType = weaponSO.weaponType;
        skillName = weaponType.ToString();
        maxLevel = weaponSO.maxLevel;
        baseDamage = weaponSO.baseDamage;
        baseCooldown = weaponSO.baseCooldown;
        baseProjectileSpeed = weaponSO.baseProjectileSpeed;
    }
    public override void Behaviour()
    {
        gunTransform = PlayerController.Instance.gunTransform;
        isActive = true;
        StartCoroutine(Fire());
    }
    IEnumerator Fire()
    {
        while (isActive)
        {
            Instantiate(laserPrefab, gunTransform.position, gunTransform.rotation);
            yield return new WaitForSeconds(weaponSO.baseCooldown);
        }
    }
}