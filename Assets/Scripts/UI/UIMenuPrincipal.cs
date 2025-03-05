using UnityEngine;
using UnityEngine.SceneManagement;
public class UIMenuPrincipal : MonoBehaviour
{

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jugar();
        }
    }

    public void Jugar()
    {
        SceneManager.LoadScene(1);
    }
}
