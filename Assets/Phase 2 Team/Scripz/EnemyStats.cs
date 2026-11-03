using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float moveSpeed;
    public float atkSpeed;
    public float damage;
    public float damageToBase;
    public float enemyHealth;

    // public float vulnerability; ? Use a list to check for and check for damage properties. Check for Fire/Ice/Properties in Excel Sheet. 
}
