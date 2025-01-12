using UnityEngine;

public class KeyCounter : MonoBehaviour
{
    public static KeyCounter Instance { get; private set; }

    private int _keyCount;

    private void Awake()
    {
        Instance = this;
    }

    public bool HasKey() => _keyCount > 0;

    public bool UseKey()
    {
        if (_keyCount > 0)
        {
            _keyCount--;

            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            Destroy(other.gameObject);
            _keyCount++;
        }
    }
}