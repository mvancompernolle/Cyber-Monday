using UnityEngine;
using System.Collections;

public class ItemInterScript : BaseInteraction
{
    public string itemName;
    public bool attached = false;
    public int attachPos;
    public float itemOffset = 0.11f;

    override public void Start()
    {
        base.Start();
        gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Items/" + itemName, typeof(Sprite));
    }

    public override void Interact()
    {
        // drop an item
        attachPos = charController.addItem(gameObject, itemName);
        if ( attachPos != -1 )
        {
            attached = true;
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    public override bool CanInteract()
    {
        return player.GetComponent<CharacterController>().items.Count < 5;
    }

    void Update()
    {
        if (attached)
        {
            float rads = Mathf.Deg2Rad * (charController.itemRotation + attachPos * 72.0f);
            transform.position = player.transform.position + new Vector3(Mathf.Cos(rads), Mathf.Sin(rads), 0.0f) * itemOffset;
        }
    }
}
