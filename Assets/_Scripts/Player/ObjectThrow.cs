using DG.Tweening;
using TMPro;
using UnityEngine;

public class ObjectThrow : MonoBehaviour
{
    [SerializeField] private GameObject _granadePrefab;
    [SerializeField] private TMP_Text _grenadeCounterText;

    private int _granadeCounter = 5;

    private void Awake()
    {
        UpdateCounter();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Throw();
    }

    private void Throw()
    {
        if (_granadeCounter > 0)
        {
            Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject granade = Instantiate(_granadePrefab, transform.position, Quaternion.identity);
            granade.transform.DOMove(cursorPosition, .35f);

            _granadeCounter--;
            UpdateCounter();
        }
    }

    private void UpdateCounter()
    {
        _grenadeCounterText.text = _granadeCounter.ToString();
    }
}
