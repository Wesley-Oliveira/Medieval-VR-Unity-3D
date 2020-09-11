using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLevel : MonoBehaviour
{

    //Definindo a Variavel do alvo
    public Target.TargetEnum target = Target.TargetEnum.None;

    //Definindo a pontuação de cada alvo
    private int valLevel1 = 100;
    private int valLevel2 = 50;
    private int valLevel3 = 10;

    private void OnCollisionEnter(Collision collision)
    {
        transform.parent.GetComponent<Target>().particleDust.Play();

        if(target == Target.TargetEnum.Level1)
        {
            transform.parent.GetComponent<Target>().particle100Points.Play();
            FindObjectOfType<ControllRank>().rank += valLevel1;
        }
        else if (target == Target.TargetEnum.Level2)
        {
            transform.parent.GetComponent<Target>().particle50Points.Play();
            FindObjectOfType<ControllRank>().rank += valLevel2;
        }
        else if (target == Target.TargetEnum.Level3)
        {
            transform.parent.GetComponent<Target>().particle10Points.Play();
            FindObjectOfType<ControllRank>().rank += valLevel3;
        }
    }
}
