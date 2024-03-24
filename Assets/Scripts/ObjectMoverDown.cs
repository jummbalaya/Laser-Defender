using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoverDown : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    void Update()
    {
        MoveObjectsDown();
        DestroyObject();
    }

    private void DestroyObject()
    {
        if (transform.position.y < -Camera.main.orthographicSize)
        {
            Destroy(gameObject);
        }
    }

    private void MoveObjectsDown()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }
}
