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
    public int hitsLeft = 10;
    public static int numEnemies = 0;

    public override void Start()
    {
        base.Start();
        // init items
        for( int i = 0; i < itemNames.Length; ++i)
        {
            itemPrefab = (GameObject)Instantiate(Resources.Load("Prefabs/techno_liver"));
            itemPrefab.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Items/" + itemNames[i], typeof(Sprite));
            itemPrefab.GetComponent<ItemInterScript>().itemName = itemNames[i];
            itemPrefab.layer = LayerMask.NameToLayer("Default");
            items.Add(itemPrefab);
        }
        itemAngleDiff = 360.0f / itemNames.Length;
        numEnemies++;
    }

    public override void Interact()
    {

        // drop an item
        if(items.Count > 0)
        {
            items[items.Count - 1].layer = LayerMask.NameToLayer("Interactable");
            items[items.Count - 1].transform.position += (new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f).normalized * 0.1f);
            items.RemoveAt(items.Count - 1);
            cooldown = 0.25f;
        }
        else if(items.Count == 0)
        {
            hitsLeft--;
            if(hitsLeft <= 0 && charController.items.Count < 5)
            {
                itemPrefab = (GameObject)Instantiate(Resources.Load("Prefabs/techno_liver"));
                itemPrefab.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Items/bad_guy", typeof(Sprite));
                ItemInterScript script = itemPrefab.GetComponent<ItemInterScript>();
                script.itemName = "bad_guy";
                itemPrefab.layer = LayerMask.NameToLayer("Default");
                script.attached = true;
                script.attachPos = charController.addItem(itemPrefab, "bad_guy");
                if (script.attachPos == -1)
                {
                    Destroy(itemPrefab);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public override bool CanInteract()
    {
        return cooldown <= 0.0f;
    }

    void Update() {
        if (cooldown > 0.0f) cooldown -= Time.deltaTime;
        // spin objects around enemy
        itemRotation += itemRotationSpeed * Time.deltaTime;
        for( int i = 0; i < items.Count; ++i)
        {
            float rads = Mathf.Deg2Rad * (itemRotation + itemAngleDiff * i);
            items[i].transform.position = transform.position + new Vector3(Mathf.Cos(rads), Mathf.Sin(rads), 0.0f) * itemOffset;
        }
    }

}
