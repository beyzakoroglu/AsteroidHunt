using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D bulletRigid;
    public float speed = 500.0f;
    public float maxLifeTime = 10.0f; 

    private void Awake()
    {
        bulletRigid = GetComponent<Rigidbody2D>();
        
    }

    public void Project(Vector2 direction) 
    {
        bulletRigid.AddForce(direction * speed);
        Destroy(this.gameObject, maxLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
