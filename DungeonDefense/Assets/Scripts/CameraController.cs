using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool isMove = true;

    public float camSpeed = 30f;

    public float camThickness = 10f;

    public float zoomSpeed = 5f;

    public float minZoom = 10f;

    public float maxZoom = 80f;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.defeatGame)
        {
            this.enabled = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            isMove = !isMove;
        if (!isMove)
            return;

        if(Input.GetKey("w") || Input.mousePosition.y >= Screen.height - camThickness)
        {
            transform.Translate(Vector3.forward * camSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= camThickness)
        {
            transform.Translate(Vector3.back * camSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - camThickness)
        {
            transform.Translate(Vector3.right * camSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= camThickness)
        {
            transform.Translate(Vector3.left * camSpeed * Time.deltaTime, Space.World);
        }

        float zoomScroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 camPos = transform.position;
        camPos.y -= zoomScroll * 1000 * zoomSpeed * Time.deltaTime;
        camPos.y = Mathf.Clamp(camPos.y, minZoom, maxZoom);

        transform.position = camPos;
    }
}
