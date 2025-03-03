using System;
using Unity.Multiplayer.Center.Common;
using Unity.VisualScripting;
using UnityEngine;

public class JugadorAtaque : MonoBehaviour
{
    [SerializeField] private float ataqueCooldown;
    [SerializeField] private Transform puntoFuego;
    [SerializeField] private GameObject[] bolasFuego;

    private Animator anim;
    private JugadorMovimiento jugadorMovimiento;
    private float temporizador = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>(); 
        jugadorMovimiento = GetComponent<JugadorMovimiento>();
    }
 

    private void Update()
    {
        if(Input.GetMouseButton(0) && temporizador > ataqueCooldown && jugadorMovimiento.isAtaque())
        Atacar();

        temporizador += Time.deltaTime;

    }

    private void Atacar()   
    {
        anim.SetTrigger("atacar");
        temporizador = 0;
        
        bolasFuego[FindbolaFuego()].transform.position = puntoFuego.position;
        bolasFuego[FindbolaFuego()].GetComponent<Proyectil>().setDireccion(Mathf.Sign(transform.localScale.x));
    }

    private int FindbolaFuego() 
    {
        for (int i = 0; i < bolasFuego.Length; i++)
        {
            if (!bolasFuego[i].activeInHierarchy)
                return i;
        }
        return 0;
    
    }
}
