using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CannonEvent : EventTrigger
{

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        //Desativar todos os outros componentes do player
        FindObjectOfType<Bow>().gameObject.SetActive(false);
        FindObjectOfType<RVPlayer>().setMovement(false);

        GetComponent<Cannon>().isActived = true;
    }
}
