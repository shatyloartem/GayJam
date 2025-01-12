using _Scripts.UI;
using _Scripts.Utilities;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int WalkState = Animator.StringToHash("WalkState");
    
    [SerializeField] private float moveSpeed = 5f;

    private bool _isGamePaused;

    private Vector2 _movementDirection;

    private Rigidbody2D rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private StepSoundController _stepSoundController;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _stepSoundController = GetComponentInChildren<StepSoundController>();
        
        LoadPosition();
    }

    private void Start()
    {
        UIGameController.OnGamePaused += OnGamePaused;
        UIGameController.OnGameUnpaused += OnGameUnpaused;
    }

    private void OnGameUnpaused()
    {
        _isGamePaused = false;
    }

    private void OnGamePaused()
    {
        _isGamePaused = true;
    }

    private void Update()
    {
        if (_isGamePaused) 
            return;

        _movementDirection.Set(InputManager.Movement.x, InputManager.Movement.y);

        rb.velocity = _movementDirection * moveSpeed;
        
        UpdateAnimations();

        if (_movementDirection == Vector2.zero)
            _stepSoundController.SoundOff();
        else 
            _stepSoundController.SoundOn(2);
    }

    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
        _stepSoundController.SoundOff();

        UIGameController.OnGamePaused -= OnGamePaused;
        UIGameController.OnGameUnpaused -= OnGameUnpaused;
    }

    private void LoadPosition()
    {
        var savedPosition = Checkpoint.Load();
        if(savedPosition == Vector2.zero)
            return;
        
        transform.position = savedPosition;
    }

    private void UpdateAnimations()
    {
        int state = 0;
        if(InputManager.Movement.x == 0 && InputManager.Movement.y > 0)
            state = 3;
        else if (InputManager.Movement.x == 0 && InputManager.Movement.y < 0)
            state = 2;
        else switch (InputManager.Movement.x)
        {
            case > 0:
                _spriteRenderer.flipX = false;
                state = 1;
                break;
            case < 0:
                _spriteRenderer.flipX = true;
                state = 1;
                break;
        }

        Debug.Log($"{InputManager.Movement.x} | {InputManager.Movement.y} | {state}");
        _animator.SetInteger(WalkState, state);
    }
}
