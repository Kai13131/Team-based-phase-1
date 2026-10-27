using UnityEngine;

public class Towers : MonoBehaviour
{
    public enum TowerType
    {
        Normal,
        Slow,
        Advanced
    }

    // Cost of the tower
    public int efficiencyCost = 1;
    public int upgradeEfficiencyCost = 1;
    

    // Tracks effienciency consumed by the tower.

    private int totalEfficiencyUsed = 0;

    public TowerType towerType;
    //Tower Prefab
    public GameObject bulletPrefab;
    //Tower stats
    public float attackSpeed = 1f;
    public float range = 3f;
    public int bulletDamage = 1;
    private float fireColddown = 0f;

    //upgrade
    public int upgradeLevel = 1;
    public int maxUpgradeLevel = 2;
    public int upgradeCost = 50;

    private BuildTile buildTile;

    //For some reason, the sprite change doesn't work
    //public Sprite level2Sprite;

    //private SpriteRenderer spriteRenderer;

    private void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();

        //UpdateSprite();
    }
    // Connect this tower to its BuildTile
    public void SetBuildTile(BuildTile tile)
    {
        buildTile = tile;
    }
    public BuildTile GetBuildTile()
    {
        return buildTile;
    }
    public bool RegisterEfficiency()
    {
        // Prevent double registration.
        if (totalEfficiencyUsed > 0)
            return false;

        // Reserve efficiency when building.
        if (!GameManager.Instance.SpendEfficiency(efficiencyCost))
        {
            Debug.Log("Not enough efficiency to build!");
            return false;
        }

        totalEfficiencyUsed = efficiencyCost;
        return true;
    }

    public void ReleaseEfficiency()
    {
        // Return all efficiency used by this tower.
        if (totalEfficiencyUsed <= 0)
            return;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.RefundEfficiency(totalEfficiencyUsed);
        }

        totalEfficiencyUsed = 0;
        }
    public void Upgrade()
    {
        if (upgradeLevel >= maxUpgradeLevel)
        {
            Debug.Log("Tower is already max level!");
            return;
        }

        //// Check if player has enough money
        //if (!GameManager.Instance.SpendMoney(upgradeCost))
        //{
        //    Debug.Log("Not enough money to upgrade!");
        //    return;
        //}

        // Check if player has enough efficiency to upgrade.
        if (!GameManager.Instance.SpendEfficiency(upgradeEfficiencyCost))
        {
            Debug.Log("Not enough efficiency to upgrade!");
            return;
        }

        // Track the additional efficiency consumed.
        totalEfficiencyUsed += upgradeEfficiencyCost;

        // Increase level
        upgradeLevel++;

        // Increase stats
        bulletDamage += 5;
        attackSpeed += 0.1f;
        range += 0.5f;
        //UpdateSprite();
        Debug.Log(
            "Tower upgraded! " +
            "Level: " + upgradeLevel +
            " Damage: " + bulletDamage +
            " Attack Speed: " + attackSpeed +
            " Range: " + range
        );
    }

    private void OnMouseDown()
    {
        if (TowerUpgradeUI.Instance != null)
        {
            TowerUpgradeUI.Instance.SelectTower(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        fireColddown -= Time.deltaTime;
        if (fireColddown <= 0f)
        {
            GameObject target = FindNearestEnemy();
            if (target != null)
            {
                Shoot(target);

                //animator.SetTrigger("Shoot");
                //audioSource.PlayOneShot(shootSound);

                fireColddown = attackSpeed;
            }

        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(
                transform.position,
                enemy.transform.position
                );

            if (distance < shortestDistance && distance <= range)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }
        // Check boss
        if (boss != null)
        {
            float bossDistance = Vector2.Distance(
                transform.position,
                boss.transform.position
            );

            if (bossDistance < shortestDistance && bossDistance <= range)
            {
                shortestDistance = bossDistance;
                nearestEnemy = boss;
            }
        }
        return nearestEnemy;
    }

    void Shoot(GameObject enemy)
    {
        GameObject bullet = Instantiate(
            bulletPrefab,
            transform.position,
            Quaternion.identity
            );

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.damage = bulletDamage;

        bulletScript.target = enemy.transform;



        switch (towerType)
        {
            case TowerType.Normal:
                SetBulletDamage(bulletDamage);
                break;
            case TowerType.Slow:
                SetBulletDamage(bulletDamage);
                bulletScript.appliesSlow = true;
                break;
            case TowerType.Advanced:
                SetBulletDamage(bulletDamage); // Higher damage for advanced tower
                break;
        }
    }

    public void SetBulletDamage(int damage)
    {
        bulletDamage = damage;
    }

    private void OnDestroy()
    {
        // Return efficiency when this tower is destroyed.
        if (GameManager.Instance != null)
        {
            ReleaseEfficiency();
        }
    }

    //private void UpdateSprite()
    //{
    //    if (spriteRenderer == null)
    //    {
    //        Debug.LogWarning("No SpriteRenderer found on tower!");
    //        return;
    //    }
    //
    //    if (upgradeLevel == 2)
    //    {
    //        spriteRenderer.sprite = level2Sprite;
    //    }
    //}
}
