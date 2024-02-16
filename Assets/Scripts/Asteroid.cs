using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float speed = 50.0f;
    public float maxLifeTime = 20.0f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;
    [SerializeField] private GameObject particleEffect;  //explosion
    AudioManager audioManager;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();   
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);  //randomize rotation
        transform.localScale = Vector3.one * size;  //short way to write new Vector3(this.size, this.size, this.size)

        rigidBody.mass = size;
    
    }   

    public void SetTrajectory(Vector2 direction)    //yorunge  //just like bullet Project()
    {
        rigidBody.AddForce(direction * speed);
        Destroy(gameObject, maxLifeTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet") 
        {
            audioManager.playClip(audioManager.hit);
            if((size * 0.5f) >= minSize)
            {
                CreateSplit();
                CreateSplit();
            }
            Instantiate(particleEffect, transform.position, transform.rotation);
            FindObjectOfType<GameManager>().GetScore(this);
            Destroy(gameObject);

        }

    }

    private void CreateSplit()
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, position, transform.rotation);
        half.size = size * 0.5f;
        half.SetTrajectory(Random.insideUnitCircle.normalized * speed);
    }

}
