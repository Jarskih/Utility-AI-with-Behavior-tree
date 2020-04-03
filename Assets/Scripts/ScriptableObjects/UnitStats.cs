using UnityEngine;
[CreateAssetMenu(menuName = "UnitStats")]
public class UnitStats : ScriptableObject
{
    public int maxHealth = 100;
    public int maxMana = 100;
    public int healing = 50;
    public int healingCost = 50;
    public int tauntCost = 50;
    public float attackRange = 1.5f;
    public int recoverySpeed = 1;
    public int sensingDistance = 15;
}