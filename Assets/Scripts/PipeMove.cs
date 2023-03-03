using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMove : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    void Update()
    {
        transform.position +=  speed * Time.deltaTime * Vector3.left;
    }
}
