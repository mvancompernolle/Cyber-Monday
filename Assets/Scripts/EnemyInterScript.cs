using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyInterScript : BaseInteraction
{
    GameObject itemPrefab;
    public string[] itemNames;
    public float itemRotation = 0.0f;
    public float itemRotationSpeed = 90.0f;
    float itemAngleDiff;
    List<GameObject> items = new List<GameObject>();
    public float itemOffset = 0.11f;
    float cooldown = 0.0f;

    public override void Start()
    {
        base.Start();
        // init items
        for( int i = 0; i < itemNames.Length; ++i)
        {
            itemPrefab = (GameObject)Instantiate(Resources.Load("Prefabs/Item"));
            itemPrefab.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Items/" + itemNames[i], typeof(Sprite));
            itemPrefab.GetComponent<ItemInterScript>().itemName = itemNames[i];
            itemPrefab.layer = LayerMask.NameToLayer("Default");
            items.Add(itemPrefab);
        }
        itemAngleDiff = 360.0f / itemNames.Length;
    }

    public override void Interact()
    {
        if (cooldown > 0.0f) cooldown -= Time.deltaTime;
        // drop an item
        if(items.Count > 0 && cooldown <= 0.0f)
        {
            items[items.Count - 1].layer = LayerMask.NameToLayer("Interactable");
            items[items.Count - 1].transform.position += (new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f).normalized * 0.1f);
            items.RemoveAt(items.Count - 1);
            cooldown = 0.1f;
        }
    }

    public override bool CanInteract()
    {
        return true;
    }

    void Update() {
        // spin objects around enemy
        itemRotation += itemRotationSpeed * Time.deltaTime;
        for( int i = 0; i < items.Count; ++i)
        {
            float rads = Mathf.Deg2Rad * (itemRotation + itemAngleDiff * i);
            items[i].transform.position = transform.position + new Vector3(Mathf.Cos(rads), Mathf.Sin(rads), 0.0f) * itemOffset;
        }

        // turn towards player
        if ((player.transform.position - transform.position).magnitude < 1.0f)
        {
            transform.rotation = Quaternion.identity;
            Vector2 dir = player.transform.position - transform.position;
            float playerAngle = Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x) - 90;
            transform.Rotate(0.0f, 0.0f, playerAngle);
        }
    }

}
