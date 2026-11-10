using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [Header("Health")]
    public float maxHealth;

    [Header("Movement")]
    public float moveSpeed;
    public float slowedMoveSpeed;

    /// <summary>
    ///     This isn't fully implemented yet, but
    ///     I wanted the enemies to have an attack damage 
    ///     and attack speed on the barricade and base.
    /// </summary>
    [Header("Attack")]
    public float atkSpeed;
    public float damage;
    public float damageToBase;
    

    // public float vulnerability; ? Use a list to check for and check for damage properties. Check for Fire/Ice/Properties in Excel Sheet. 
}
