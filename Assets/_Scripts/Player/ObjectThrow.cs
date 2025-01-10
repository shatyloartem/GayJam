using DG.Tweening;
using UnityEngine;

public class ObjectThrow : MonoBehaviour
{
    [SerializeField] private GameObject _granadePrefab;

    private int _granadeCounter = 5;

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
        }
    }
}