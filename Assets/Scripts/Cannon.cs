using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Cannon : MonoBehaviour
{
    //Variaveis publicas
    public GameObject cannonBall;   //Bola de canhão
    public GameObject cannon;       //Canhão, parte metalica
    public AudioClip shootSound;     //Som de disparo do canhao

    public bool isActived = false;   //Ativar e desativar o canhao

    //Variaveis privadas
    private AudioSource audioSource;

    //Transformada inicial do canhao
    private Vector3 cannonPositionInit;
    private Quaternion cannonRotationInit;

    //Transformada inicial do canhao ao todo
    private Vector3 positionInit;
    private Quaternion rotationInit;

    private TargetPlayer targetPlayer;

    //Rotacao da cabeca do usuario
    private Quaternion rotationPlayer;

    //Bag
    private Bag bag;

	void Start ()
    {
        //Recuperar o audioSurce
        audioSource = GetComponent<AudioSource>();

        //Aplicando transformada inicial
        cannonPositionInit = cannon.transform.position;
        cannonRotationInit = cannon.transform.rotation;
        
        positionInit = this.transform.position;
        rotationInit = this.transform.rotation;

        //Recuperando o Player no AV
        targetPlayer = FindObjectOfType<TargetPlayer>();

        //Rotacao atual do usuario
        rotationPlayer = targetPlayer.transform.rotation;

        //Recuperando a Bag
        bag = FindObjectOfType<Bag>();
    }
	
	void Update ()
    {        
        if(isActived)
        {
            //Realizar a rotacao

            //Pegar a rotacao do player
            rotationPlayer = targetPlayer.transform.rotation;

            //Criando a variavel de rotacao do canhao todo
            var allCannonEuler = this.transform.rotation.eulerAngles;

            //Aplicando a rotacao da cabeca do player ao canhao todo
            allCannonEuler.y = rotationPlayer.eulerAngles.y;
            this.transform.rotation = Quaternion.Euler(allCannonEuler);

            //Criando variavel de rotacao do canhao
            var cannonPointerEuler = cannon.transform.rotation.eulerAngles;

            //Aplicando a rotacao do plauer ao canhao
            cannonPointerEuler.x = rotationPlayer.eulerAngles.x;
            cannon.transform.rotation = Quaternion.Euler(cannonPointerEuler);

            //Canhão atirar
            if ((GvrController.TouchDown || Input.GetMouseButtonDown(0)) && (bag.balls > 0))
            {
                //Subtrair uma bola da bag
                bag.balls--;

                //Disparar som
                audioSource.clip = shootSound;
                audioSource.Play();

                //Instanciar bolas de canhao
                GameObject ball = Instantiate(cannonBall);

                //Aplicando rotacao a bola de canhao
                ball.transform.position = cannonBall.transform.position;

                //Add script CannonBoll à bola
                ball.AddComponent<CannonBall>();

                //Adicionando componente RigidBody
                ball.AddComponent<Rigidbody>();
                ball.GetComponent<Rigidbody>().mass = 80;

                //Aplicar forca de disparo
                ball.GetComponent<Rigidbody>().AddForce(cannon.transform.forward * 200000);//alterar força

                //Destruir bola de canhao
                ball.GetComponent<CannonBall>().timeToDestroy();
            }
        }
        else
        {
            //manter rotacao inicial
            this.transform.rotation = rotationInit;
            this.transform.position = positionInit;

            cannon.transform.position = cannonPositionInit;
            cannon.transform.rotation = cannonRotationInit;            
        }     
	}
}
