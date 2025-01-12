using _Scripts.UI;
using DG.Tweening;
using UnityEngine;

public class LockerHider : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private BoxCollider2D _boxCollider;
    private SpriteRenderer _spriteRenderer;
    private Transform _locker;

    private bool _isHidden;
    private bool _isLockerInRange;

    private void Awake()
    {
        _playerMovement = GetComponentInParent<PlayerMovement>();
        _boxCollider = GetComponentInParent<BoxCollider2D>();
        _spriteRenderer = GetComponentInParent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isHidden)
        {
            _playerMovement.enabled = true;
            _boxCollider.enabled = true;
            _isHidden = false;

            _spriteRenderer.sortingOrder = 3;

            PlaySound();
        }
        else if (Input.GetKeyDown(KeyCode.E) && _isLockerInRange)
        {
            _playerMovement.enabled = false;
            _boxCollider.enabled = false;
            _isHidden = true;

            _spriteRenderer.sortingOrder = 1;

            transform.parent.DOMove(_locker.position, .3f);

            PlaySound();
        }
    }

    private void PlaySound()
    {
        _locker.GetComponent<LockerSound>().LockerOpenSound();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Locker"))
        {
            _isLockerInRange = true;
            _locker = collision.transform;
            UIGameController.Instance.SetIconActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Locker"))
        {
            _isLockerInRange = false;
            UIGameController.Instance.SetIconActive(false);
        }
    }
}
