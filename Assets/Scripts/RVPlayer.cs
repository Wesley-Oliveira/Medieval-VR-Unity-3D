using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RVPlayer : MonoBehaviour
{
    //Variaveis publicas
    public Camera cameraRayCasting;     //Camera para apontamento da posição e de OBJ 3D
    public float speed = 0.7f;         //Velocidade do Player
    public int distanceToMove = 10;        //Sinalização da posição
    public GameObject arrowToMove;       //A seta de apontamento
    public AudioClip clickSound;         //Quando o botão do oculos por clicado
    public GameObject bowAndArrow;       //Arco e flecha

    //Variaveis Privadas
    private AudioSource audioSource;      //Compomente para dar play no audio
    private RaycastHit Hit;
    private Vector3 startPoint;             //Posicao atual do player
    private Vector3 endPoint;               //Ponto de selecao
    private float startTime;                //Tempo em que o usuario clicou
    private float journeyLength;            //Distancia entre RVplayer e ponto selecionado
    private bool flagStop = false;          //Flag de parada de movimentacao
    private bool moviment = true;

    void Start ()
    {
        //Recuperar audioSource
        audioSource = GetComponent<AudioSource>();		
	}
	
	void Update ()
    {
        if(moviment)
        {
            //Raycasting de apontamento, selecionar os gameobjects da cena
            Ray ray = cameraRayCasting.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            //Obter o objeto apontado
            if (Physics.Raycast(ray, out Hit, distanceToMove))
            {
                //Debug.Log(Hit.transform.position + " : " + Hit.transform.name);

                float scaleArrow = Vector3.Distance(Hit.transform.position, this.transform.position) / 13000;
                arrowToMove.transform.localScale = new Vector3(scaleArrow, scaleArrow, scaleArrow);
                arrowToMove.transform.position = Hit.transform.position;
            }

            //Mecanismo de Movimentação
            if (GvrController.TouchDown || Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out Hit, distanceToMove))
                {
                    if (Hit.transform.tag == "AllowedPosition")
                    {
                        audioSource.clip = clickSound;
                        audioSource.Play();

                        startPoint = transform.position;
                        endPoint = Hit.transform.position;

                        startTime = Time.time;
                        journeyLength = Vector3.Distance(startPoint, endPoint);

                        flagStop = true;
                    }
                }
            }

            if (flagStop)
            {
                float distCovered = (Time.time - startTime) * speed;
                float fracJourney = distCovered / journeyLength;
                Vector3 move = Vector3.Lerp(startPoint, endPoint, fracJourney);
                this.transform.position = move;

                //Se player chegou a posicao final
                if (this.transform.position == endPoint)
                {
                    flagStop = false;
                }
            }
        }
	}

    public void setMovement(bool enable)
    {
        //Ativandos os movimentos
        moviment = enable;

        if(moviment)
        {
            //Procurar todos os canhoes
            var cannons = FindObjectsOfType<Cannon>();

            foreach(var cannon in cannons)
            {
                //Desativar todos os canhoes
                cannon.isActived = false;
            }

            //Ativar arco e flecha
            arrowToMove.SetActive(true);
            bowAndArrow.SetActive(true);
        }
    }
}
