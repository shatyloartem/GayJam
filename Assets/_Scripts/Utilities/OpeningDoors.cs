using _Scripts.UI;
using UnityEngine;

public class OpeningDoors : MonoBehaviour
{
    [SerializeField] private Sprite _openedDoor;
    [SerializeField] private Sprite _closedDoor;

    [SerializeField] private AudioClip _doorSound, _doorClosedSound;

    [SerializeField] private Collider2D _playerCollider;

    private BoxCollider2D _boxCollider;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;

    private bool _isOpened;
    private bool _isPlayerInRange;
    private bool _isKeyUsed;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isOpened && _isKeyUsed && _isPlayerInRange)
        {
            Physics2D.IgnoreCollision(_boxCollider, _playerCollider, false);

            _isOpened = false;

            _spriteRenderer.sprite = _closedDoor;

            _audioSource.PlayOneShot(_doorSound);
        }
        else if (Input.GetKeyDown(KeyCode.E) && _isPlayerInRange)
        {
            if (_isKeyUsed || KeyCounter.Instance.UseKey())
            {
                Physics2D.IgnoreCollision(_boxCollider, _playerCollider, true);

                _isOpened = true;
                _isKeyUsed = true;

                _spriteRenderer.sprite = _openedDoor;

                _audioSource.PlayOneShot(_doorSound);
            }
        }
        
        if(Input.GetKeyDown(KeyCode.E) && _isPlayerInRange && !_isKeyUsed)
            _audioSource.PlayOneShot(_doorClosedSound);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = true;
            
            if(KeyCounter.Instance.HasKey())
                UIGameController.Instance.SetIconActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = false;
            UIGameController.Instance.SetIconActive(false);
        }
    }
}
