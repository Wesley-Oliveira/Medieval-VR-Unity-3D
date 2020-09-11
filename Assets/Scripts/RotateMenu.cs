using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMenu : MonoBehaviour
{
    //Rotacao da cabeça player
    private TargetPlayer targetPlayer;

	void Start ()
    {
        //Recuperar o obj TargerPlayer
        targetPlayer = FindObjectOfType<TargetPlayer>();
	}
	
	void FixedUpdate ()
    {
        //Variavel de rotacao em Euler
        var menuEuler = transform.rotation.eulerAngles;

        //Aplicando rotacao em Y
        menuEuler.y = targetPlayer.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(menuEuler);
	}
}
