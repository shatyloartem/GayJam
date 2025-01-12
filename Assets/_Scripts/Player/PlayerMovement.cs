using System;
using _Scripts.UI;
using _Scripts.Utilities;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private bool _isGamePaused;

    private Vector2 _movementDirection;

    private Rigidbody2D rb;
    private StepSoundController _stepSoundController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
}
