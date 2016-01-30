using UnityEngine;
using System.Collections;

public class testScript : BaseInteraction {
    public override void Interact()
    {
        Debug.Log("TESTING MOTHA FUOAKCA");
    }

    public override bool CanInteract()
    {
        return true;
    }
}
