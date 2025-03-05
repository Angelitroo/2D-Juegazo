using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pantallaGameOver;
    [SerializeField] private AudioClip sonidoGameOver;

    private void Awake()
    {
        pantallaGameOver.SetActive(false);
    }

    public void GameOver()
    {
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