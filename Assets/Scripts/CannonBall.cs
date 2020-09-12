using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    //Metodo de chamada para destruicao da bola
    public void timeToDestroy()
    {
        StartCoroutine(DestroyBall());
    }

    //Destruir a bola depois de um tempo
    IEnumerator DestroyBall()
    {
        yield return new WaitForSeconds(7);
        Destroy(this.gameObject);
    }
}
