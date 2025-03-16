using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Mood/PlayerStats", order = 0)]
public class PlayerStats : ScriptableObject {
    public float WalkSpeed => _walkSpeed;
    public float SprintSpeed => _sprintSpeed;
    public float JumpForce => _jumpForce;
    public float GravityAcceleration => _gravityAcceleration;

    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityAcceleration;
}