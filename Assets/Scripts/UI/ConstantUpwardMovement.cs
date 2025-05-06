using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantUpwardMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private void Update()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }
}
