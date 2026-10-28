using UnityEngine;
using System.Collections;

public class MovimientoPelota : MonoBehaviour
{
    public float velocidad = 5f;
public float aceleracion = 0.5f;
public float velocidadMaxima = 12f;

    public Transform paletaIzquierda;
    public Transform paletaDerecha;

    public Marcador marcador;

    private Vector3 direccion;
    private Camera camara;
    private bool esperandoSaque = false;

    void Start()
    {
        camara = Camera.main;
        PrepararSaque();
    }

void PrepararSaque()
{
    int lado = Random.Range(0, 2);

    if (lado == 0)
    {
        direccion = Vector3.left;
    }
    else
    {
        direccion = Vector3.right;
    }
}

void RebotarEnPaleta(Transform paleta, bool haciaDerecha)
{
    float diferencia = transform.position.y - paleta.position.y;

    float porcentajeGolpe = Mathf.Clamp(diferencia / 0.9f, -1f, 1f);

    float direccionX = haciaDerecha ? 1f : -1f;

    direccion = new Vector3(direccionX, porcentajeGolpe * 0.8f, 0f).normalized;

    velocidad = Mathf.Min(velocidad + aceleracion, velocidadMaxima);
}

    void Update()
    {
            if (esperandoSaque)
    {
        return;
    }
    
        if (marcador != null && marcador.partidaTerminada)
    {
        return;
    }
    
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
    RebotarEnPaleta(paletaIzquierda, true);
}
        }

        // Rebote contra la paleta derecha.
        if (direccion.x > 0f && paletaDerecha != null)
        {
            float distanciaX = Mathf.Abs(nuevaPosicion.x - paletaDerecha.position.x);
            float distanciaY = Mathf.Abs(nuevaPosicion.y - paletaDerecha.position.y);

           if (distanciaX <= 0.3f && distanciaY <= 0.9f)
{
    RebotarEnPaleta(paletaDerecha, false);
}
        }

        transform.position = nuevaPosicion;

        // Calcular los límites horizontales visibles de la cámara.
        if (camara != null)
        {
            float limiteDerecho = camara.ViewportToWorldPoint(new Vector3(1f, 0.5f, 0f)).x;
            float limiteIzquierdo = camara.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0f)).x;

            // Si la pelota sale por la derecha, el jugador izquierdo gana un punto.
            if (transform.position.x >= limiteDerecho)
            {
                if (marcador != null)
                {
                    marcador.SumarPuntoJugador();
                }

                ReiniciarPelota();

                Debug.Log("La pelota salió por la derecha.");
            }
            // Si la pelota sale por la izquierda, el jugador derecho gana un punto.
            else if (transform.position.x <= limiteIzquierdo)
            {
                if (marcador != null)
                {
                    marcador.SumarPuntoEnemigo();
                }

                ReiniciarPelota();

                Debug.Log("La pelota salió por la izquierda.");
            }
        }
    }

    public void ReiniciarPelota()
{
    StopAllCoroutines();
    StartCoroutine(ReiniciarPelotaConPausa());
}

private IEnumerator ReiniciarPelotaConPausa()
{
    esperandoSaque = true;

    transform.position = Vector3.zero;
    direccion = Vector3.zero;
    velocidad = 5f;

    yield return new WaitForSeconds(1f);

    PrepararSaque();
    esperandoSaque = false;
}
}
