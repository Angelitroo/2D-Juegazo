using UnityEngine;
using UnityEngine.SceneManagement;

public class Cargador : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(1);
        }
    }
}