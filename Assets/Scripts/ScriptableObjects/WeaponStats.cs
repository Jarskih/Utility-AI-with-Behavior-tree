using UnityEngine;

[UnityEngine.CreateAssetMenu(fileName = "WeaponStat", menuName = "WeaponStats", order = 0)]
public class WeaponStats : ScriptableObject
{
    public enum WeaponType
    {
        Melee,
        Ranged
    }
    public int damage;
    public WeaponType weaponType;
    public GameObject projectile;
}