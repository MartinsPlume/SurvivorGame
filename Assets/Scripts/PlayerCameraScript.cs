using UnityEngine;

public class PlayerCameraScript : MonoBehaviour
{
    public float CameraSpeed = 100.0f;
    public GameObject CameraFollowObject;
    Vector3 FollowPosition;
    public float clampAngle = 80.0f;
    public float inputSensitivity = 300.0f;
    public GameObject CameraObject;
    public GameObject PlayerObject;
    public float camDistanceXtoPlayer;
    public float camDistanceYtoPlayer;
    public float camDistanceZtoPlayer;
    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalInputZ;
    public float smoothX;
    public float smoothY;
    private float rotY = 0.0f;
    private float rotX = 0.0f;

    private void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // rotācija input
        float inputx = Input.GetAxis("RightStickHorizontal");
        float inputz = Input.GetAxis("RightStickVertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        finalInputX = inputx + mouseX;
        finalInputZ = inputz + mouseY;

        rotY =+ finalInputX + inputSensitivity * Time.deltaTime;
        rotX =+ finalInputZ + inputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;

    }

    private void LateUpdate()
    {
        CameraUpdater();
    }

    void CameraUpdater()
    {
        // kamera seko rotācijai
        Transform target = CameraFollowObject.transform;

        float step = CameraSpeed;// * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }


}
