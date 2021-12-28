using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator_fast : MonoBehaviour
{
 
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * 5*Time.deltaTime);
    }
}
