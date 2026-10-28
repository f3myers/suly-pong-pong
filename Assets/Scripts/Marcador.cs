
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

    public GameObject elementosPartida;
    public GameObject panelVictoria;

    void Start()
    {
        ActualizarMarcador();

        if (panelVictoria != null)
        {
            panelVictoria.SetActive(false);
        }

        if (textoVictoria != null)
        {
            textoVictoria.gameObject.SetActive(false);
        }

        if (botonReiniciar != null)
        {
            botonReiniciar.SetActive(false);
        }

        if (elementosPartida != null)
        {
            elementosPartida.SetActive(true);
        }
    }

    void ActualizarMarcador()
    {
        textoPuntosJugador.text = puntosJugador.ToString();
        textoPuntosEnemigo.text = puntosEnemigo.ToString();

        Debug.Log("Actualizando marcador: Jugador = " +
            puntosJugador + ", Enemigo = " + puntosEnemigo);
    }

    void ComprobarGanador()
    {
        if (puntosJugador >= puntosParaGanar)
        {
            MostrarVictoria("Ganador: Jugador 1");
        }
        else if (puntosEnemigo >= puntosParaGanar)
        {
            MostrarVictoria("Ganador: Jugador 2");
        }
    }

    void MostrarVictoria(string mensaje)
    {
        partidaTerminada = true;

        if (elementosPartida != null)
        {
            elementosPartida.SetActive(false);
        }

        if (panelVictoria != null)
        {
            panelVictoria.SetActive(true);
        }

        if (textoVictoria != null)
        {
            textoVictoria.text = mensaje;
            textoVictoria.gameObject.SetActive(true);
        }

        if (botonReiniciar != null)
        {
            botonReiniciar.SetActive(true);
        }

        Debug.Log(mensaje);
    }

    public void SumarPuntoJugador()
    {
        if (partidaTerminada) return;

        puntosJugador++;
        ActualizarMarcador();
        ComprobarGanador();

        Debug.Log("Jugador: " + puntosJugador);
    }

    public void SumarPuntoEnemigo()
    {
        if (partidaTerminada) return;

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
        ReiniciarMarcador();

        if (panelVictoria != null)
        {
            panelVictoria.SetActive(false);
        }

        if (textoVictoria != null)
        {
            textoVictoria.gameObject.SetActive(false);
        }

        if (botonReiniciar != null)
        {
            botonReiniciar.SetActive(false);
        }

        if (movimientoPelota != null)
        {
            movimientoPelota.ReiniciarPelota();
        }

        if (elementosPartida != null)
        {
            elementosPartida.SetActive(true);
        }

        Debug.Log("¡Nueva partida!");
    }
}
