using System.Numerics;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    private float velocidad = 10;
    private float direccion;
    private bool impacto;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private float duracion;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }


    private void Update()
    {
        if (impacto) return;
        float velocidadMovimiento = velocidad * Time.deltaTime * direccion;
        transform.Translate(velocidadMovimiento, 0, 0);

        duracion += Time.deltaTime;
        if (duracion > 5) gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        impacto = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explotar");

        if(collision.CompareTag("Enemigo"))
        {
            collision.GetComponent<Vida>().RecibirDaño(1);
        }
    }


    public void setDireccion(float _direccion)
    {
        duracion = 0;
        direccion = _direccion;
        gameObject.SetActive(true);
        impacto = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direccion)
            localScaleX = -localScaleX;

        transform.localScale = new UnityEngine.Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }


    private void Desactivar()
    {
        gameObject.SetActive(false);
    }
}