using UnityEngine;

public class EnemigoPatrulla : MonoBehaviour
{
    [Header("Puntos de patrulla")]
    [SerializeField] private Transform izquierdaFinal;
    [SerializeField] private Transform derechaFinal;

    [Header("Enemigo")]
    [SerializeField] private Transform enemigo;

    [Header("Parametros de movimiento")]
    [SerializeField] private float velocidad;
    private Vector3 escalaInicial;
    private bool movimientoIzquierda; 

    [Header("Tiempo de espera")]
    [SerializeField] private float tiempoEspera;
    private float temporizador;

    [Header("Parametros de animacion")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        escalaInicial = enemigo.localScale;
    }

    private void Update()
    {
        if (movimientoIzquierda)
        {
            if (enemigo.position.x > izquierdaFinal.position.x)
            {
                MoverseLados(-1);
            }
            else
            {
                CambiarDireccion();
            }
        }
        else
        {
            if (enemigo.position.x < derechaFinal.position.x)
            {
                MoverseLados(1);
            }
            else
            {
                CambiarDireccion();
            }
        }
    }
    private void OnDisable()
    {
        anim.SetBool("moviendo", false);
    }


    private void CambiarDireccion()
    {
        anim.SetBool("moviendo", false);

        temporizador += Time.deltaTime;

        if (temporizador >= tiempoEspera)
        {
            movimientoIzquierda = !movimientoIzquierda;
        }       
    }

    private void MoverseLados(int direccion)
    {
        temporizador = 0;
        anim.SetBool("moviendo", true);

        // Mirar a un lado
        enemigo.localScale = new Vector3(Mathf.Abs(escalaInicial.x) * direccion, 
            escalaInicial.y, escalaInicial.z);

        // Moverse hacia ese lado
        enemigo.position = new Vector3(enemigo.position.x + Time.deltaTime * direccion * velocidad, enemigo.position.y, enemigo.position.z);
    }
}