using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

    private CircleCollider2D _safeZone;
    public Missile MissilePrefab;
    public float MissileSpeed = 5.0f;

    void Start() {
        _safeZone = GameObject.FindGameObjectWithTag("SafetyZone").GetComponent<CircleCollider2D>();
    }

    public void NotifyNewAsteroid(GameObject obj) {
        Rigidbody2D asteroid = obj.GetComponent<Rigidbody2D>();
        if (TrajectoryWithinSafetyZone(asteroid.position, asteroid.velocity)) {
            Vector3 missleV = CalculateMissileVelocity(asteroid.position, asteroid.velocity);
            Aim(missleV);
            ShootAt(obj, missleV);
        }
    }

    //From http://doswa.com/2009/07/13/circle-segment-intersectioncollision.html
    private bool TrajectoryWithinSafetyZone(Vector2 pos, Vector2 v) {
        Vector2 earthPos = (Vector2)_safeZone.transform.position;

        Vector2 trajectory = v.normalized;
        Vector2 toEarth = earthPos - pos;
        Vector2 projection = Vector2.Dot(toEarth, trajectory) * trajectory;
        Vector2 closestPoint = pos + projection;
        Vector2 closest_v = earthPos - closestPoint;

        if (closest_v.magnitude < _safeZone.radius)
            return true;
        return false;
    }

    //From http://danikgames.com/blog/how-to-intersect-a-moving-target-in-2d/
    private Vector3 CalculateMissileVelocity(Vector3 pos, Vector3 v) {
        Vector2 earthPos = (Vector2)_safeZone.transform.position;

        // Find the vector AB
        Vector2 AB = (Vector2)pos - earthPos;

        // Normalize it
        float ABmag = Mathf.Sqrt(AB.x * AB.x + AB.y * AB.y);
        AB.x /= ABmag;
        AB.y /= ABmag;

        // Project u onto AB
        float uDotAB = AB.x * v.x + AB.y * v.y;
        Vector2 uj = uDotAB * AB;

        // Subtract uj from u to get ui
        Vector2 ui = (Vector2)v - uj;

        // Set vi to ui (for clarity)
        Vector2 vi = ui;

        // Calculate the magnitude of vj
        float viMag = Mathf.Sqrt(vi.x * vi.x + vi.y * vi.y);
        float vjMag = Mathf.Sqrt(MissileSpeed * MissileSpeed - viMag * viMag);

        // Get vj by multiplying it's magnitude with the unit vector AB
        Vector2 vj = AB * vjMag;

        // Add vj and vi to get v
        return new Vector3(vj.x + vi.x, vj.y + vi.y);
    }

    private void Aim(Vector3 point) {
        Vector3 diff = point - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    private void ShootAt(GameObject asteroid, Vector3 velocity) {
        Missile missile = Instantiate(MissilePrefab, transform.position, transform.rotation) as Missile;
        missile.GetComponent<Rigidbody2D>().velocity = velocity;
        missile.SetTarget(asteroid);
    }
}
