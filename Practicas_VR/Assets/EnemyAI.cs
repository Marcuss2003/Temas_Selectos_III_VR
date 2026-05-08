using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour {
    public Transform player; 
    private NavMeshAgent agent;
    private Animator anim; // [1] Variable para el componente Animator

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>(); // [2] Obtenemos el Animator de Mordecai
    }

    void Update() {
        if (player != null) {
            agent.SetDestination(player.position);
        }

        // [3] Lógica para la animación única
        // Medimos la velocidad actual del NavMeshAgent
        float velocidadActual = agent.velocity.magnitude;

        if (velocidadActual > 0.1f) {
            // Si se mueve, la animación se reproduce a velocidad normal
            anim.speed = 1; 
        } else {
            // Si se detiene (llegó al jugador), la animación se pausa
            anim.speed = 0; 
        }
    }
}
