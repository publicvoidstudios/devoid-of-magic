using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    private Vector2 startPosition;
    [SerializeField]
    public Camera camera;
    

    // Update is called once per frame
    void Update()
    {
        camera.transform.position = transform.position;
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = camera.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            float posX = camera.ScreenToWorldPoint(Input.mousePosition).x - startPosition.x;
            float posY = camera.ScreenToWorldPoint(Input.mousePosition).y - startPosition.y;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x - posX, -7f, 7f), Mathf.Clamp(transform.position.y - posY, -5.5f, 5.5f), transform.position.z); 

        }
    }
}
