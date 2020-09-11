using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passage : MonoBehaviour
{

    private RVPlayer player;
    private Vector3 posInit;        //Posicao inicial do player

	void Start ()
    {
        //Procurar Player na Cena
        player = FindObjectOfType<RVPlayer>();
        posInit = player.transform.position;  
    }
	
    //Entrar em construções
    public void GetInBuild()
    {
        //Posicao atual antes do teletransporte
        posInit = player.transform.position;

        //Fazer tele transporte
        player.transform.position = this.transform.position;
    }

    //Sair de construções
    public void GetOutBuild()
    {
        //Devolvendo o player para posicao inicial
        player.transform.position = posInit;
    }

}
