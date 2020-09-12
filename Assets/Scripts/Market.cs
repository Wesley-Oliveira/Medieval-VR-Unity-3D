using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Market : MonoBehaviour
{
    //Definir valores dos itens
    public int valArrow = 50;
    public int valBall = 100;

    public AudioClip moneySound;
    private AudioSource audioSource;

	void Start ()
    {
        //Recuperar audio source
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = moneySound;
	}
	
    public void BuyArrow()
    {
        Bag bagPlayer = FindObjectOfType<Bag>();
        if(bagPlayer.myMoney >= valArrow)
        {
            bagPlayer.arrows++;
            bagPlayer.myMoney -= valArrow;

            audioSource.Play();
        }
    }

    public void BuyBall()
    {
        Bag bagPlayer = FindObjectOfType<Bag>();
        if (bagPlayer.myMoney >= valBall)
        {
            bagPlayer.balls++;
            bagPlayer.myMoney -= valBall;

            audioSource.Play();
        }
    }
}
