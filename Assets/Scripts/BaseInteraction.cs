using UnityEngine;
using System.Collections;

abstract public class BaseInteraction : MonoBehaviour {
    public abstract void Interact();
    public abstract bool CanInteract();
}
