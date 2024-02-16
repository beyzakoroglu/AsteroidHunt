using Unity.VisualScripting;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid asteroidPrefab;
    public float trajectoryVariance = 15.0f;
    public float spawnRate = 2.0f;
    public float spawnDistance = 15.0f;
    public int spawnAmount = 1;
    public float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", spawnRate, spawnRate);
    }

    void Update(){
        timer += Time.deltaTime;
    }

    private void Spawn() 
    {
        for (int i = 0; i < spawnAmount + (timer / 10); i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance; //spawnDirection = bir daire içindeki random noktalarda asteroid oluşturulacak
            Vector3 spawnPoint = transform.position + spawnDirection;

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward); 

            Asteroid asteroid = Instantiate(asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);

            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }

}
