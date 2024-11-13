using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RintanganMoveScript : MonoBehaviour
{
    public float moveSpeed;
    public float deadZone = -12;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < deadZone)
        {
            Debug.Log("Ice Deleted");
            Destroy(gameObject);
        }

        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
    }
}
