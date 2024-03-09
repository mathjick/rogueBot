using UnityEngine;

public class TM_Assault : TriggerModule
{
    #region Parameters
    public GameObject projectilePrefab;
    public float RPM;
    public int projectileSpeed;
    #endregion
    #region Variables
    private float timer = 0;
    private float timeBeetweenShots = 0;
    private bool isHolding = false;
    #endregion

    public void Start()
    {
        timeBeetweenShots = 60f / RPM;
        Debug.Log("timeBeetweenShots : " + timeBeetweenShots);
    }

    public void FixedUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (isHolding)
        {
            timer = timeBeetweenShots;
            Shoot();
        }
    }

    public override void Hold()
    {
        Debug.Log("Hold with module");
        isHolding = true;
    }

    public override void Release()
    {
        Debug.Log("Release with module");
        isHolding = false;
    }

    public void Shoot()
    {
        RaycastHit hit;
        Physics.Raycast(playerWeapon.inventory.playerController.playerView.transform.position,playerWeapon.inventory.playerController.playerView.transform.forward * 1000, out hit);
        if (hit.collider && hit.collider.tag != "tag_player")
        {
            var spawnSpec = playerWeapon.projectileLaunchAnchor.transform;
            var projectile = Instantiate(projectilePrefab, spawnSpec.position, spawnSpec.rotation);
            projectile.GetComponent<ModularProjectileBase>().owner = playerWeapon.inventory.gameObject;
            projectile.GetComponent<Rigidbody>().AddForce((hit.point - playerWeapon.projectileLaunchAnchor.transform.position).normalized * projectileSpeed);
        }
    }
}
