using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
    Vector3 direction = new Vector3(1.0f, 0.0f, 0.0f);
    float speed = 1.0f;
    Rigidbody2D rbody;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
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
}
