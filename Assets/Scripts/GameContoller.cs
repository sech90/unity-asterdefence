using UnityEngine;

public class GameContoller : MonoBehaviour
{
    public GameObject Asteroid;
    public float AsteroidSpeed;

    private Turret _turret; 
    private bool _dragging = false;
    private Vector2 _spawnPoint;

    void Start()
    {
        _turret = GameObject.FindObjectOfType<Turret>();
    }


    void Update()
    {
        //left click.
        if (Input.GetMouseButtonDown(0) && !_dragging)
        {
            _dragging = true;
            _spawnPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        //released left button
        if (Input.GetMouseButtonUp(0) && _dragging)
        {
            _dragging = false;
            Vector2 releasePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (releasePoint != _spawnPoint){
                Vector2 direction = releasePoint - _spawnPoint;
                SpawnAsteroid(_spawnPoint, direction);
            }
        }
    }
    private void SpawnAsteroid(Vector3 pos, Vector3 direction)
    {
        GameObject asteroid = Instantiate(Asteroid, pos, Quaternion.identity) as GameObject;
        
        //for speed proportional to distance between the start point and end point
        //asteroid.GetComponent<Rigidbody2D>().velocity = direction;

        //for constant speed
        asteroid.GetComponent<Rigidbody2D>().velocity = direction.normalized * AsteroidSpeed;

        _turret.NotifyNewAsteroid(asteroid);
    }
}
