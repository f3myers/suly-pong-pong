using UnityEngine;

public class Marcador : MonoBehaviour
{
    public int puntosJugador = 0;
    public int puntosEnemigo = 0;

    public TMPro.TMP_Text textoPuntosJugador;
public TMPro.TMP_Text textoPuntosEnemigo;


    public int puntosParaGanar = 5;

    public bool partidaTerminada = false;

    public TMPro.TMP_Text textoVictoria;

    public GameObject botonReiniciar;

    public MovimientoPelota movimientoPelota;

    void Start()
{
    ActualizarMarcador();
}

    void ActualizarMarcador()
{
    textoPuntosJugador.text = puntosJugador.ToString();
    textoPuntosEnemigo.text = puntosEnemigo.ToString();

    Debug.Log("Actualizando marcador: Jugador = " + puntosJugador + ", Enemigo = " + puntosEnemigo);
}

void ComprobarGanador()
{
    if (puntosJugador >= puntosParaGanar)
    {
        partidaTerminada = true;

        if (botonReiniciar != null)
        {
            botonReiniciar.SetActive(true);
        }

        if (textoVictoria != null)
        {
            textoVictoria.text = "¡EL JUGADOR IZQUIERDO HA GANADO!";
            textoVictoria.gameObject.SetActive(true);
        }

        Debug.Log("¡El jugador izquierdo ha ganado!");
    }
    else if (puntosEnemigo >= puntosParaGanar)
    {
        partidaTerminada = true;

        if (botonReiniciar != null)
        {
            botonReiniciar.SetActive(true);
        }

        if (textoVictoria != null)
        {
            textoVictoria.text = "¡EL JUGADOR DERECHO HA GANADO!";
            textoVictoria.gameObject.SetActive(true);
        }

        Debug.Log("¡El jugador derecho ha ganado!");
    }
}

    public void SumarPuntoJugador()
    {
        puntosJugador++;

        ActualizarMarcador();

        ComprobarGanador();

        Debug.Log("Jugador: " + puntosJugador);
    }

    public void SumarPuntoEnemigo()
    {
        puntosEnemigo++;

        ActualizarMarcador();

        ComprobarGanador();

        Debug.Log("Enemigo: " + puntosEnemigo);
    }

    public void ReiniciarMarcador()
{
    puntosJugador = 0;
    puntosEnemigo = 0;

    partidaTerminada = false;

    ActualizarMarcador();

    Debug.Log("La partida se ha reiniciado.");
}

public void ReiniciarPartida()
{
    // Reiniciar los puntos y el estado de la partida.
    ReiniciarMarcador();

    // Ocultar el mensaje de victoria.
    if (textoVictoria != null)
    {
        textoVictoria.gameObject.SetActive(false);
    }

    // Ocultar el botón de reinicio.
    if (botonReiniciar != null)
    {
        botonReiniciar.SetActive(false);
    }

    // Devolver la pelota al centro.
    if (movimientoPelota != null)
    {
        movimientoPelota.ReiniciarPelota();
    }

    Debug.Log("¡Nueva partida!");
}
}