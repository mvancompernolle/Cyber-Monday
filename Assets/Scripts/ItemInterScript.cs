using UnityEngine;
using System.Collections;

public class ItemInterScript : BaseInteraction
{
    public string itemName;
    bool attached = false;
    int attachPos;
    public float itemOffset = 0.11f;

    override public void Start()
    {
        base.Start();
        gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Items/" + itemName, typeof(Sprite));
        attachPos = -1;
    }

    public override void Interact()
    {
        // drop an item
        attachPos = charController.addItem(gameObject, name);
        if( attachPos < 5)
        {
            attached = true;
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    public override bool CanInteract()
    {
        return true;
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
