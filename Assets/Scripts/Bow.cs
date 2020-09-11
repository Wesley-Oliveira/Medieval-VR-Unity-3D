using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bow : MonoBehaviour
{
    //Variaveis Privadas
    private Animation anim;     //Animação do arco
    private AudioSource audioSource;        //Audio do Arco
    private float initPosX;             //Posicao inicial em X da flecha
    private bool ToArm = false;         //Flag para armar o arco
    private bool ArmArrow = false;       //Flag para animar a flecha

    //Bag
    private Bag bag;

    //Variaveis Publicas
    public GameObject arrow;        //Flecha
    public AudioClip armSound;      //Som puxando a linha do arco
    public AudioClip shootSound;    //Som do disparo

	void Start ()
    {

        //Recuperar componentes
        anim = GetComponent<Animation>();
        audioSource = GetComponent<AudioSource>();


        //Recuperando a bag
        bag = FindObjectOfType<Bag>();

    }
	
	void Update ()
    {

        //Quando o gatilho for pressionado
        if((GvrController.TouchDown || Input.GetMouseButtonDown(0)) && (bag.arrows > 0))
        {
            TriggerPress();
        }

        if(ArmArrow)
        {
            if (arrow.transform.localPosition.x > -0.169f)
            {
                arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x - (Time.deltaTime) / 1.09f,
                                                            arrow.transform.localPosition.y,
                                                            arrow.transform.localPosition.z);
            }
            else
            {
                ArmArrow = false;
                ToArm = true;
            }
        }

		
	}

    //Metodo botao clicado
    public void TriggerPress()
    {
        //Debug.Log("Gatilho Pressionado!!!");

        if(!ToArm) //Se não tiver armado
        {
            initPosX = arrow.transform.localPosition.x;
            EngageArchery();
            ArmArrow = true;   //Flag para iniciar a animacao de armar a flecha
        }
        else
        {
            DisengageArchery();
        }

    }

    //Armando o Arco com flecha
    void EngageArchery()
    {
        anim.Play("Arm");
        if(anim.IsPlaying("Arm"))
        {
            ToArm = true;   //Colocar flag em true, como arma
            audioSource.clip = armSound;
            audioSource.Play();
        }
    }

    //Disparando flecha
    void DisengageArchery()
    {
        //Animação do tiro
        anim.Play("Shoot");

        //Subtrair flecha da bag
        bag.arrows--;

        if(anim.IsPlaying("Shoot"))
        {
            audioSource.clip = shootSound;
            audioSource.Play();

            ToArm = false; //Depois do disparo, o arco não esta mais armado
        }

        //Criar o mecanismo de disparo
        //Para cada disparo criaremos uma nova flecha

        //Instanciar novas flechas
        GameObject newArrow = Instantiate(arrow);

        //Definindo transform da flecha
        newArrow.transform.position = arrow.transform.position;
        newArrow.transform.rotation = arrow.transform.rotation;

        //Inserindo collider
        newArrow.AddComponent<CapsuleCollider>();

        //Inserindo componente RigidBody à flecha
        newArrow.AddComponent<Rigidbody>();

        //Adicionando força na flecha para ser disparada
        newArrow.GetComponent<Rigidbody>().AddForce(newArrow.transform.up * -2000);      

        //Retornar a flecha original para sua posicao inicial
        arrow.transform.localPosition = new Vector3(initPosX,
                                                    arrow.transform.localPosition.y,
                                                    arrow.transform.localPosition.z);
    }
}
