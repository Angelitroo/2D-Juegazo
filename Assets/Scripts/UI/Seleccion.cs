using UnityEngine;
using UnityEngine.UI;

public class Seleccion : MonoBehaviour
{
    [SerializeField] private RectTransform[] opciones;
    [SerializeField] private AudioClip sonidoSeleccion;//movemos opciones
    [SerializeField] private AudioClip sonidoInteraccion;//pulsamos opcion

    private RectTransform rect;
    private int seleccionActual;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {

        //cambiar posicion
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            CambiarSeleccion(-1);
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            CambiarSeleccion(1);
        }

        //interactuar
        if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
        {
            Interactuar();
        }

    }


    private void CambiarSeleccion(int cambiar)
    {
        seleccionActual += cambiar;
        if(cambiar != 0)
        {
            Sonidos.instancia.ReproducirSonido(sonidoSeleccion);
        }


        if (seleccionActual < 0)
        {
            seleccionActual = opciones.Length - 1;
        }
        else if (seleccionActual > opciones.Length -1)
        {
            seleccionActual = 0;            
        }
        rect.position = new Vector3(rect.position.x, opciones[seleccionActual].position.y, 0);
    }

    private void Interactuar()
    {
        Sonidos.instancia.ReproducirSonido(sonidoInteraccion);
        opciones[seleccionActual].GetComponent<Button>().onClick.Invoke();
    }
}
