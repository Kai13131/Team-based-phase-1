using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;
    public float speed = 8;
    public int damage = 1;

    public bool appliesSlow = false;

    // V2 Edition:

    [Header("Chain Lightning")]
    public bool appliesChainLightning = false;

    public int maxBounces = 3;

    public float chainRadius = 3f;

    private int currentBounces = 0;

    private readonly System.Collections.Generic.List<Transform> hitTargets = new(); // Essential to create a new list of enemies in order for it not to break easy.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        //The bullet move towards to the enemy
        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
            );


        //calculate the distance if bullete close enough to nemey
        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            if (target != null)
            {
                if (appliesSlow)
                {
                    EnemyMovement enemy = target.GetComponent<EnemyMovement>();
                    if (enemy != null)
                    {
                        enemy.ApplySlow(2f);
                    }
                }
            }

            EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();

            // Chain Lightning Damage

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            // Grabs from initialized private new list and places struck enemies on that list.

            hitTargets.Add(target);

            // Bounce back to another enemy.

            if (appliesChainLightning && currentBounces < maxBounces)
            {
                Transform nextTarget = FindNextTarget();

                if (nextTarget != null)
                {
                    target = nextTarget;
                    currentBounces++;
                    return;
                }
            }
           
            Destroy(gameObject);
        }
    }

    private Transform FindNextTarget()
    {
        {
            // Using colliders to have enemies be targeted in a Chain Radius

            Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position,chainRadius);

            Transform nearestTarget = null;
            float shortestDistance = Mathf.Infinity;

            foreach (Collider2D enemy in nearbyEnemies)
            {
                // Only target enemies with health.
                if (enemy.GetComponent<EnemyHealth>() == null)
                    continue;

                // Prevent hitting the same enemy twice.
                if (hitTargets.Contains(enemy.transform))
                    continue;

                float distance = Vector2.Distance(transform.position,enemy.transform.position);

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestTarget = enemy.transform;
                }
            }

            return nearestTarget;
        }
    }
}
