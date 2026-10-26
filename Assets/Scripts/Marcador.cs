using UnityEngine;

public class Marcador : MonoBehaviour
{
    public int puntosJugador = 0;
    public int puntosEnemigo = 0;

    public TMPro.TMP_Text textoPuntosJugador;
public TMPro.TMP_Text textoPuntosEnemigo;


    public int puntosParaGanar = 5;

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

    public void SumarPuntoJugador()
    {
        puntosJugador++;

        ActualizarMarcador();

        Debug.Log("Jugador: " + puntosJugador);
    }

    public void SumarPuntoEnemigo()
    {
        puntosEnemigo++;

        ActualizarMarcador();

        Debug.Log("Enemigo: " + puntosEnemigo);
    }
}