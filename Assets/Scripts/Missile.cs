using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

    private GameObject _target;
    public GameObject Explosion;

    public void SetTarget(GameObject target) {
        _target = target;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (_target != null && coll.gameObject == _target) {
            Destroy(coll.gameObject);
            Destroy(gameObject);
            GameObject expl = Instantiate(Explosion, coll.gameObject.transform.position, Quaternion.identity) as GameObject;
            Destroy(expl, 1.0f);
        }
    }
}
