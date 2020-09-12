using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Arrow : MonoBehaviour
{

    //Componente de audio
    private AudioSource audioSource;

    //Som da flecha colidindo
    public AudioClip impactArrow;

	void Start ()
    {
        //Recuperando audioSource
        audioSource = GetComponent<AudioSource>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Arrow")
        {
            audioSource.clip = impactArrow;
            audioSource.Play();

            Destroy(this.GetComponent<Rigidbody>());
            Destroy(this.GetComponent<CapsuleCollider>());
        }
    }
}
