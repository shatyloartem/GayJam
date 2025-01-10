using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 _movementDirection;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _movementDirection.Set(InputManager.Movement.x, InputManager.Movement.y);

        rb.velocity = _movementDirection * moveSpeed;
    }

    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
    }
}
