using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float cameraMoveSpeed = 120f;

    public GameObject cameraFollowObj;

    public float minClampAngle = -80f;
    public float maxClampAngle = 80f;
    public float inputSensitivity = 150f;

    private float mouseX;
    private float mouseY;

    private float rotX = 0f;
    private float rotY = 0f;

    // Use this for initialization
    void Start () {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        rotY += mouseX * inputSensitivity * Time.deltaTime;
        rotX += mouseY * inputSensitivity * Time.deltaTime;

        // clamps camera rotation up and down
        rotX = Mathf.Clamp(rotX, minClampAngle, maxClampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0f);
        transform.rotation = localRotation;
	}

    private void LateUpdate()
    {
        CameraUpdater();
    }

    void CameraUpdater()
    {
        Transform target = cameraFollowObj.transform;

        float step = cameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
