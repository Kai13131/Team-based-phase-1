using UnityEngine;

public class Towers : MonoBehaviour
{
    public enum TowerType
    {
        Normal,
        Slow,
        Advanced
    }

    public TowerType towerType;
    public float range = 3f;
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public int bulletDamage = 1;
    public int cost = 50; // Cost of the tower

    public Animator animator;

    public AudioSource audioSource;
    public AudioClip shootSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        fireCountdown -= Time.deltaTime;
        if (fireCountdown <= 0f)
        {
            GameObject target = FindNearestEnemy();
            if (target != null)
            {
                Shoot(target);

                animator.SetTrigger("Shoot");
                audioSource.PlayOneShot(shootSound);

                fireCountdown = fireRate;
            }

        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
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
}
