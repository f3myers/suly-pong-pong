using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoPaleta : MonoBehaviour
{
    public float velocidad = 5f;

    void Update()
    {
        float movimiento = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed)
            {
                movimiento = 1f;
            }
            else if (Keyboard.current.sKey.isPressed)
            {
                movimiento = -1f;
            }
        }

        transform.position += Vector3.up * movimiento * velocidad * Time.deltaTime;
    }
}