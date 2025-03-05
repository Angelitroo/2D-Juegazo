using UnityEngine;

public class Nivel : MonoBehaviour
{
    [SerializeField] private GameObject[] enemigos;
    private Vector3[] posicionInicial;

    private void Awake()
    {
        //Guardamos la posicion inicial de los enemigos
        posicionInicial = new Vector3[enemigos.Length]; 
        for (int i = 0; i < enemigos.Length; i++)
        {
            if(enemigos[i] != null)
                posicionInicial[i] = enemigos[i].transform.position;
        }
    }

    public void ResetearNivel(bool estado)
    {
        //Acticar/Desactivar a los enemigos
        for (int i = 0; i < enemigos.Length; i++)
        {
            if(enemigos[i] != null)
            {
                enemigos[i].SetActive(estado);
                enemigos[i].transform.position = posicionInicial[i];
            }
        } 
    }
}
