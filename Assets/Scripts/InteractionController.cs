using UnityEngine;
using System.Collections;

public class InteractionController : MonoBehaviour {
    float distToInteraction = 0.15f;
    Rigidbody2D rbody;
    CircleCollider2D collider;
    public GameObject selector;
    SpriteRenderer selectorRenderer;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        selectorRenderer = selector.GetComponent<SpriteRenderer>();
        selectorRenderer.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        // cast ray onto map and see if it collides with object
        float radius = collider.bounds.size.x * 0.55f;
        Vector3 mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mouseDir.z = 0;
        mouseDir.Normalize();
        Debug.DrawRay(rbody.transform.position + (mouseDir * radius), mouseDir * distToInteraction, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(rbody.transform.position + (mouseDir * radius), mouseDir, distToInteraction, 1 << LayerMask.NameToLayer("Interactable"));

        if (hit)
        {
            InteractionScript script = hit.collider.gameObject.GetComponent<InteractionScript>();
            BaseInteraction interactionComp = ((BaseInteraction)hit.transform.gameObject.GetComponent(script.scriptName));
            if ( interactionComp.CanInteract())
            {
                // draw indicator saying you can interact
                selectorRenderer.enabled = true;
                selector.transform.position = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y, hit.collider.transform.position.z);
                float selectorRad = hit.collider.bounds.size.x * 3.0f;
                selector.transform.localScale = new Vector3(selectorRad, selectorRad, selectorRad);

                if (Input.GetKey(KeyCode.Space))
                {
                    interactionComp.Interact();
                }
            }
            else
            {
                selectorRenderer.enabled = false;
            }
        }
        else
        {
            selectorRenderer.enabled = false;
        }
    }
}
