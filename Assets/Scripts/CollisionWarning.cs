using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWarning : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        this.transform.parent.parent.GetComponent<MAsteroid>().Collision(other);
    }
}
