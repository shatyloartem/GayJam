using DG.Tweening;
using UnityEngine;

public class LockerItem : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private BoxCollider2D _boxCollider;
    private SpriteRenderer _spriteRenderer;
    
    private bool _isHidden;
    private bool _isLockerInRange;
    private Transform _locker;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isHidden)
        {
            _playerMovement.enabled = true;
            _boxCollider.enabled = true;
            _isHidden = false;
            _spriteRenderer.sortingOrder = 3;
        }
        else if (Input.GetKeyDown(KeyCode.E) && _isLockerInRange)
        {
            _playerMovement.enabled = false;
            _boxCollider.enabled = false;
            _isHidden = true;
            _spriteRenderer.sortingOrder = 1;
            transform.DOMove(_locker.position, .3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Locker"))
        {
            _isLockerInRange = true;
            _locker = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Locker"))
        {
            _isLockerInRange = false;
        }
    }
}