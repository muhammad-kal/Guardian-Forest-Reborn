using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(ParticleSystem))]
public class BibitCollision : MonoBehaviour
{
    public static Action<Vector3[]> bibitOnCollision;

    private void OnParticleCollision(GameObject other)
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();

        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        int jumlahCollision = ps.GetCollisionEvents(other, collisionEvents);

        Vector3[] posisiCollision = new Vector3[jumlahCollision];

        for(int i = 0; i < jumlahCollision; i++)
        {
            posisiCollision[i] = collisionEvents[i].intersection;
            //Debug.Log("Posisi :  " + posisiCollision[i]);


        }

        bibitOnCollision?.Invoke(posisiCollision);
    }
}
