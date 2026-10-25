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

        float limiteSuperior = 4.25f;
        float limiteInferior = -4.25f;

        float posicionY = Mathf.Clamp(transform.position.y, limiteInferior, limiteSuperior);

        transform.position = new Vector3(transform.position.x, posicionY, transform.position.z);
    }
}