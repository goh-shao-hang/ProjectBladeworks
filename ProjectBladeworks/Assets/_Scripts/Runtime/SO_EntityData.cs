using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Scriptable Objects/Data/Entity Data")]
public class SO_EntityData : ScriptableObject
{
    [Header("Base Stats")]
    public float BaseAttack;
    public float BaseDefense;
    public float BaseSpeed;
    public float BaseAttackSpeed;

    [Header("Damage Bonuses")]
    public float SlashDamageBonus;
    public float BluntDamageBonus;
    public float ThrustDamageBonus;
    public float FireDamageBonus;
    public float IceDamageBonus;
    public float ThunderDamageBonus;

    [Header("Resistance")]
    public float SlashDamageResistance;
    public float BluntDamageResistance;
    public float ThrustDamageResistance;
    public float FireDamageResistance;
    public float IceDamageResistance;
    public float ThunderDamageResistance;

    [Header("Others")]
    public LayerMask DamageableLayers;
}
