using UnityEngine;
using System.Collections;

public class BaseInteraction : MonoBehaviour {
    protected GameObject player;
    protected CharacterController charController;

    public virtual void Start()
    {
        player = GameObject.FindWithTag("Player");
        charController = player.GetComponent<CharacterController>();
    }

    virtual public void Interact()
    {

    }
    virtual public bool CanInteract()
    {
        return true;
    }
}
