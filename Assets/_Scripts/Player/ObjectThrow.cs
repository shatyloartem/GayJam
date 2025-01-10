using TMPro;
using UnityEngine;

public class ObjectThrow : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Throw();
    }

    private void Throw()
    {
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
