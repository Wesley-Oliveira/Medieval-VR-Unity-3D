using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TerrainMoveMechanism : MonoBehaviour
{

    public GameObject Quad;  //Quadrado do piso
    public int lengthX = 70;   //Tamanho do piso em X
    public int lengthZ = 70;   //Tamanho do piso em Z

    public float offSetX = 0.5f;   //Ajustes de distancia do centro dos quadrados em relacao ao piso
    public float offSetZ = 0.5f;

    [Range(0.01f, 0.2f)]
    public float height = 0.1f;   //altura do quadriculado em relacao ao piso

    private Vector3 posInit;    //Posicao inicial do piso


    //Inserir todos os quadriculados
	void Start ()
    {

        //posicao inicial do terreno
        posInit = new Vector3(this.transform.position.x,
                              this.transform.position.y + height,
                              this.transform.position.z);

        //For do eixo X
        for(int x = 0; x < lengthX; x++)
        {
            //For do eixo Z
            for(int z = 0; z < lengthZ; z++)
            {
                //Instanciando um Quad
                GameObject quadriculado = Instantiate(Quad);

                //Indentificar o quad
                quadriculado.name = "PlayerPosition";
                quadriculado.transform.tag = "AllowedPosition";

                //Definindo a posicao
                quadriculado.transform.position = new Vector3(posInit.x + x + offSetX,
                                                              posInit.y,
                                                              posInit.z + z + offSetZ);
                quadriculado.transform.parent = this.transform;
            }
        }
	}
}
