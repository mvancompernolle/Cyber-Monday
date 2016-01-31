using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterController : MonoBehaviour {
    Vector3 direction = new Vector3(1.0f, 0.0f, 0.0f);
    float speed = 1.0f;
    public List<KeyValuePair<GameObject, string>> items;
    public float itemRotation = 0.0f;
    float itemRotationSpeed = 90.0f;
    public bool isWalking = false;
    public bool isKicking = false;
    public float kickAnimCooldown = 0.25f, currentKickTime;
    bool keyUp = false;
    public Animator animator;
    public bool gameOver = false;

    public AudioClip pickupSound;
    public AudioClip dieSound;
    public AudioClip kickSound;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;
    

    // Use this for initialization
    void Start () {
        items = new List<KeyValuePair<GameObject, string>>();
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
	}

    void OnGUI()
    {
        if (gameOver) {
            string gameOverText = "";
            int textSize = Screen.width / 40;
            textSize = textSize < 12 ? 12 : textSize;
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, new Color(0.0f, 0.0f, 0.0f, 0.5f));
            texture.Apply();
            GUI.skin.box.normal.background = texture;
            if (EnemyInterScript.numEnemies != 0)
            {
                gameOverText = "<size=" + textSize + ">Those sneaky cyber people kicked your butt..\n Monday could not get any worse than this.\n"
                    + "Initiating time relapse...\n Press Enter to restart or press Escape to exit.</size>";
            }
            else
            {
                gameOverText = "<size=" + textSize + ">Ritual!!! You win!!!\n"
                    + "Initiating time relapse...\n Press Enter to restart or press Escape to exit.</size>";
            }
            GUI.Box(new Rect(Screen.width * 0.25f, Screen.height * 0.25f, Screen.width * 0.5f, Screen.height * 0.5f), GUIContent.none);
            GUI.Label(new Rect(Screen.width * 0.25f, Screen.height * 0.25f, Screen.width * 0.5f, Screen.height * 0.5f), gameOverText);
        }

    }

    // Update is called once per frame
    void Update() {
        if (EnemyInterScript.numEnemies == 0) gameOver = true;
        if (gameOver)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                Application.LoadLevel(0);
            }
            else if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
            return;
        }

        if (currentKickTime >= 0.0f) { currentKickTime -= Time.deltaTime; }
        itemRotation += itemRotationSpeed * Time.deltaTime;

        float vertical = Input.GetAxis("Vertical");

        // set kicking
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isKicking = true;
            currentKickTime = kickAnimCooldown;
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(kickSound, vol);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            currentKickTime = 0.0f;
        }
        if(currentKickTime <= 0.0f)
        {
            isKicking = false;
        }

        // get mouse position
        Vector2 screenPoint = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector3 mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mouseDir.z = 0.0f;
        mouseDir.Normalize();

        //Debug.Log(transform.eulerAngles);
        float mouseAngle = Mathf.Rad2Deg * Mathf.Atan2(mouseDir.y, mouseDir.x);
        float angleDiff = mouseAngle - transform.eulerAngles.z - 90.0f;

        float distToMouse = (screenPoint - new Vector2(transform.position.x, transform.position.y)).magnitude;

        isWalking = (distToMouse >= .15f && vertical > 0.0f) ? true : false;
        animator.SetBool("isKicking", isKicking);
        animator.SetBool("isWalking", isWalking);
        float translation = (speed + (isKicking ? 0.5f : 0.0f)) * Time.deltaTime;



        transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), angleDiff);
        if (distToMouse >= .15f && vertical > 0.0f )
        {
            transform.Translate(new Vector3(-direction.y, direction.x) * vertical * translation);
        }
	}

    public int addItem(GameObject item, string name)
    {
        Debug.Log(items.Count);
        if(items.Count < 5)
        {
            items.Add(new KeyValuePair<GameObject, string>(item, name));
            if (name == "bad_guy")
            {
                float vol = Random.Range(volLowRange, volHighRange);
                source.PlayOneShot(dieSound, vol);
            }
            else
            {
                float vol = Random.Range(volLowRange, volHighRange);
                source.PlayOneShot(pickupSound, vol);
            }
            return items.Count - 1;
        }
        else
        {
            return -1;
        }
    }

    public void removeItem()
    {
        if(items.Count > 0)
        {
            items[items.Count - 1].Key.layer = LayerMask.NameToLayer("Interactable");
            items[items.Count - 1].Key.transform.position += (new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f).normalized * 0.1f);
            items[items.Count - 1].Key.GetComponent<ItemInterScript>().attached = false;
            items.RemoveAt(items.Count - 1);
        }
        else
        {
            // show game over message and restart the game
            gameOver = true;
        }
    }

}
