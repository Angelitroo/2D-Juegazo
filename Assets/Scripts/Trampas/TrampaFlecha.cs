using UnityEngine;

public class TrampaFlecha : MonoBehaviour
{
    [SerializeField] private float tiempoAtaque;
    [SerializeField] private Transform puntoFuego;
    [SerializeField] private GameObject[] flechas; 
    private float temporizador;

    [Header("Sonidos")]
    [SerializeField] private AudioClip sonidoFlecha;

    private void Atacar()
    {
        temporizador = 0;

        Sonidos.instancia.ReproducirSonido(sonidoFlecha);
        flechas[FindFlecha()].transform.position = puntoFuego.position; 
        flechas[FindFlecha()].GetComponent<Enemigo_Proyectil>().ActivarProyectil();
    }

    private int FindFlecha()
    {
        for (int i = 0; i < flechas.Length; i++)
        {
            if (!flechas[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    private void Update()
    {
        temporizador += Time.deltaTime;
        if(temporizador >= tiempoAtaque)
        {
            Atacar();
        }
    }
}
