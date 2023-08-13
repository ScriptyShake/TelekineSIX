using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var cameraTransform = transform;
        cameraTransform.position = new Vector3(target.transform.position.x, cameraTransform.position.y, -10);
    }
}
