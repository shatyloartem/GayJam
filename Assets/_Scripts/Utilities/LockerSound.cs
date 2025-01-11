using UnityEngine;

public class LockerSound : MonoBehaviour
{
    [SerializeField] private AudioClip _lockerOpenedSound;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void LockerOpenSound()
    {
        _audioSource.PlayOneShot(_lockerOpenedSound);
    }
}
