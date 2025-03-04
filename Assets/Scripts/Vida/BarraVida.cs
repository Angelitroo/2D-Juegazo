using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    [SerializeField] private Vida vidaJugador;
    [SerializeField] private UnityEngine.UI.Image totalBarraVida;
    [SerializeField] private UnityEngine.UI.Image actualBarraVida;

    private void Start()
    {
        totalBarraVida.fillAmount = vidaJugador.vidaActual / 10;
    }

    private void Update()
    {
        actualBarraVida.fillAmount = vidaJugador.vidaActual / 10;
    }

}
