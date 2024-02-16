using System.Linq.Expressions;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    public GameObject barrel;
    private bool thrustUp;
    private bool thrustDown;
    private bool thrustRight;
    private bool thrustLeft;
    private Rigidbody2D playerRigid;
    AudioManager audioManager;
    [SerializeField] private GameObject particleEffect;  //explosion
    
    private Transform playerTransform; 

    [SerializeField]
    private float speed;  

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    
    private void Start() 
    {
        playerTransform = this.transform;
        playerRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        thrustUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        thrustDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        thrustRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        thrustLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);

        LAMouse();

        if(Input.GetMouseButtonDown(0))        {
            Shoot();
        }
        
    }

    private void FixedUpdate()  //is used to perform physics-related calculations and updates. It's called at fixed time intervals.
    {
        if(thrustUp) { //ok hangi yönü gösterirse o yöne gidecek

            playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + 0.05f, playerTransform.position.z);
        }
        if(thrustDown) {
            playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y - 0.05f, playerTransform.position.z);
        }
        if(thrustLeft) {
            playerTransform.position = new Vector3(playerTransform.position.x - 0.05f, playerTransform.position.y, playerTransform.position.z);
        }
        if(thrustRight) {
            playerTransform.position = new Vector3(playerTransform.position.x + 0.05f, playerTransform.position.y, playerTransform.position.z);
        }

    }

    private void LAMouse()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //this is the angle that the weapon must rotate around to face the cursor

        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        playerTransform.rotation = rotation;
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate (bulletPrefab, barrel.transform.position, playerTransform.rotation);
        
        //Vector2 bulletPositions = new Vector2(playerTransform.position.x, playerTransform.position.y);
        bullet.Project(playerTransform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            audioManager.playClip(audioManager.die);
            playerRigid.velocity = Vector3.zero;
            playerRigid.angularVelocity = 0.0f;
            

            gameObject.SetActive(false);         //turn of the gameobject entirely

            FindObjectOfType<GameManager>().PlayerDied();     
            Instantiate(particleEffect, transform.position, transform.rotation);
            
        }
    }

    

}
