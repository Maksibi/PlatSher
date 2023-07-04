using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    [field: Header("Movement stats")]
    [field: SerializeField] public float speed{get; private set;} = 5;
    [field: SerializeField] public float jumpForce {get; private set;}= 5;
    [field: SerializeField] public float slideMultiplier{get; private set;} = 0.9f;

    [field: Header("Roll stats")]
    [field: SerializeField] public float rollLength { get; private set; } = 0.75f;
    [field: SerializeField] public float rollVelocityMultiplier{get; private set;} = 1.5f;

    [field: Header("Combat stats")]
    [field: SerializeField] public int damage {get; private set;} 
    [field: SerializeField] public int health { get; private set; } 
}
