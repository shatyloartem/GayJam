using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 _movementDirection;

    private Rigidbody2D rb;
    private StepSoundController _stepSoundController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _stepSoundController = GetComponentInChildren<StepSoundController>();
    }

    private void Update()
    {
        _movementDirection.Set(InputManager.Movement.x, InputManager.Movement.y);

        rb.velocity = _movementDirection * moveSpeed;

        if (_movementDirection == Vector2.zero)
            _stepSoundController.SoundOff();
        else 
            _stepSoundController.SoundOn(2);
    }

    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
        _stepSoundController.SoundOff();
    }
}
