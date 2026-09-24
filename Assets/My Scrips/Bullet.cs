using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;
    public float speed = 8;
    public int damage = 1;

    public bool appliesSlow = false;

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
            target.GetComponent<EnemyHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
