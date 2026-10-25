using UnityEngine;

public class MovimientoPelota : MonoBehaviour
{
    public float velocidad = 5f;

    public Transform paletaIzquierda;
    public Transform paletaDerecha;

    private Vector3 direccion;
    private Camera camara;

    void Start()
    {
        camara = Camera.main;
        direccion = new Vector3(0.3f, 1f, 0f).normalized;
    }

    void Update()
    {
        Vector3 nuevaPosicion = transform.position + direccion * velocidad * Time.deltaTime;

        // Rebote en los bordes superior e inferior.
        float limiteSuperior = 4.85f;
        float limiteInferior = -4.85f;

        if (nuevaPosicion.y >= limiteSuperior)
        {
            nuevaPosicion.y = limiteSuperior;
            direccion.y = -Mathf.Abs(direccion.y);
        }
        else if (nuevaPosicion.y <= limiteInferior)
        {
            nuevaPosicion.y = limiteInferior;
            direccion.y = Mathf.Abs(direccion.y);
        }

        // Rebote contra la paleta izquierda.
        if (direccion.x < 0f && paletaIzquierda != null)
        {
            float distanciaX = Mathf.Abs(nuevaPosicion.x - paletaIzquierda.position.x);
            float distanciaY = Mathf.Abs(nuevaPosicion.y - paletaIzquierda.position.y);

            if (distanciaX <= 0.3f && distanciaY <= 0.9f)
            {
                direccion.x = Mathf.Abs(direccion.x);
            }
        }

        // Rebote contra la paleta derecha.
        if (direccion.x > 0f && paletaDerecha != null)
        {
            float distanciaX = Mathf.Abs(nuevaPosicion.x - paletaDerecha.position.x);
            float distanciaY = Mathf.Abs(nuevaPosicion.y - paletaDerecha.position.y);

            if (distanciaX <= 0.3f && distanciaY <= 0.9f)
            {
                direccion.x = -Mathf.Abs(direccion.x);
            }
        }

        transform.position = nuevaPosicion;

        // Calcular los límites horizontales visibles de la cámara.
        if (camara != null)
        {
            float limiteDerecho = camara.ViewportToWorldPoint(new Vector3(1f, 0.5f, 0f)).x;
            float limiteIzquierdo = camara.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0f)).x;

            // Si la pelota sale por la derecha, vuelve al centro.
            if (transform.position.x >= limiteDerecho)
            {
                transform.position = Vector3.zero;
                direccion = new Vector3(-0.3f, 1f, 0f).normalized;

                Debug.Log("La pelota salió por la derecha.");
            }
            // Si la pelota sale por la izquierda, vuelve al centro.
            else if (transform.position.x <= limiteIzquierdo)
            {
                transform.position = Vector3.zero;
                direccion = new Vector3(0.3f, 1f, 0f).normalized;

                Debug.Log("La pelota salió por la izquierda.");
            }
        }
    }
}