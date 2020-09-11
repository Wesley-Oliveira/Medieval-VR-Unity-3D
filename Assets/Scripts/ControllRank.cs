using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllRank : MonoBehaviour
{
    //Contador de rank
    public int rank = 0;
    public Text points;

	// Update is called once per frame
	void Update ()
    {
        //Debug.Log("Pontuação: " + rank.ToString());
        points.text = rank.ToString();
    }
}
