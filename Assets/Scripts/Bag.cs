using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bag : MonoBehaviour
{
    //Textos para exibir o conteudo da Bag
    public Text txtArrow;
    public Text txtMoney;
    public Text txtBall;

    public int myMoney = 2500;

    //Conteudo da Bag
    public int arrows = 15;
    public int balls = 7;

	void Update ()
    {
        txtArrow.text = arrows.ToString();
        txtBall.text = balls.ToString();
        txtMoney.text = myMoney.ToString();		
	}
}
