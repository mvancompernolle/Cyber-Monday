using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterController : MonoBehaviour {
    Vector3 direction = new Vector3(1.0f, 0.0f, 0.0f);
    float speed = 1.0f;
    Rigidbody2D rbody;
    public List<KeyValuePair<GameObject, string>> items;
    public float itemRotation = 0.0f;
    float itemRotationSpeed = 90.0f;
    public bool isWalking = false;
    public bool isKicking = false;
    public Animator animator;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
        items = new List<KeyValuePair<GameObject, string>>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        itemRotation += itemRotationSpeed * Time.deltaTime;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        isWalking = (horizontal > 0.0f || vertical > 0.0f) ? true : false;
        animator.SetBool("isWalking", isWalking);
        float translation = speed * Time.deltaTime;

        // get mouse position
        Vector2 screenPoint = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector3 mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mouseDir.z = 0.0f;
        mouseDir.Normalize();

        //Debug.Log(transform.eulerAngles);
        float mouseAngle = Mathf.Rad2Deg * Mathf.Atan2(mouseDir.y, mouseDir.x);
        float angleDiff = mouseAngle - transform.eulerAngles.z - 90.0f;

        float distToMouse = (screenPoint - new Vector2(transform.position.x, transform.position.y)).magnitude;

        transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), angleDiff);
        transform.Translate(direction * horizontal * translation);
        if (distToMouse >= .15f || vertical <= 0.0f )
        {
            transform.Translate(new Vector3(-direction.y, direction.x) * vertical * translation);
        }
	}

    public int addItem(GameObject item, string name)
    {
        int pos = items.Count - 1;
        if(items.Count < 5)
        {
            items.Add(new KeyValuePair<GameObject, string>(item, name));
            pos++;
        }
        return pos;
    }

    public void removeItem()
    {
        if(items.Count > 0)
        {
            items.RemoveAt(items.Count - 1);
        }
        else
        {

        }
    }

}
