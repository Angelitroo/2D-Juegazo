using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pantallaGameOver;
    [SerializeField] private GameObject opciones;
    [SerializeField] private AudioClip sonidoGameOver;

    private void Awake()
    {
        pantallaGameOver.SetActive(false);
        opciones.SetActive(false);
    }

    public void GameOver()
    {
        opciones.SetActive(true);
        pantallaGameOver.SetActive(true);
        Sonidos.instancia.ReproducirSonido(sonidoGameOver);
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene(0);
    }

    public void Salir()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}