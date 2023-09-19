using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacle
{
    void DamagePlayer(int damageValue);

    Vector3 FindStartingPosition();

}
