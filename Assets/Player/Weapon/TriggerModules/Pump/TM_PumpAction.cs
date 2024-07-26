using UnityEngine;

public class TM_PumpAction : TriggerModule
{
    #region Parameters
    public int projectileSpeed;
    public int pelletCount;
    public float spread;
    #endregion
    #region Variables
    private float timer = 0;
    private bool isHolding = false;
    #endregion

    public void FixedUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (isHolding && actualNumberOfRounds > 0)
        {
            timer = 60f / RPM;
            Shoot();
        }
    }

    public override void Hold()
    {
        isHolding = true;
    }

    public override void Release()
    {
        isHolding = false;
    }

    public override void Shoot()
    {
        actualNumberOfRounds--;
        RaycastHit hit;
        for (int i = 0; i < pelletCount; i++)
        {
            Physics.Raycast(playerWeapon.inventory.playerController.playerView.transform.position, playerWeapon.inventory.playerController.playerView.transform.forward + new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), Random.Range(-spread, spread)) * 1000, out hit);
            if (hit.collider && hit.collider.tag != "tag_player")
            {
                var spawnSpec = playerWeapon.projectileLaunchAnchor.transform;
                var projectile = Instantiate(projectilePrefab, spawnSpec.position, spawnSpec.rotation);
                projectile.GetComponent<ModularProjectileBase>().owner = playerWeapon.inventory.gameObject;
                projectile.GetComponent<ModularProjectileBase>().damageData.damagesTypes = this.damageData.damagesTypes;
                projectile.GetComponent<ModularProjectileBase>().damageData.damages = this.damageData.damages;
                if (playerWeapon.inventory.moduleEquiped)
                {
                    projectile.GetComponent<ModularProjectileBase>().nextEffect = playerWeapon.inventory.moduleEquiped.attachToProjectile(projectile.GetComponent<ModularProjectileBase>());
                    playerWeapon.inventory.moduleEquiped.CallOnShot();
                }
                projectile.GetComponent<Rigidbody>().AddForce((hit.point - playerWeapon.projectileLaunchAnchor.transform.position).normalized * projectileSpeed);
            }
        }
        base.Shoot();
    }
}
