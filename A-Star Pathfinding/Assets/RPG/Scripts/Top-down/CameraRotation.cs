using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Drawing;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float sensitivity = 0.5f;
    [SerializeField] private ECameraSetting cameraSetting;
    public float startingXRot;
    private float yRot;

    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int X, int Y);
    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out Point pos);
    Point cursorPos = new Point();

    void Start()
    {
        startingXRot = gameObject.transform.rotation.eulerAngles.x;
        GetCursorPos(out cursorPos);
        yRot = transform.localRotation.y;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        switch (cameraSetting)
        {
            case ECameraSetting.KeyMovement:
                KeyRotation();
                break;

            case ECameraSetting.MouseMovement:
                MouseRotation();
                break;
        }   
    }

    private void KeyRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            yRot += sensitivity;
            transform.localRotation = Quaternion.Euler(startingXRot, yRot, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            yRot -= sensitivity;
            transform.localRotation = Quaternion.Euler(startingXRot, yRot, 0);
        }
    }
    
    private void MouseRotation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetCursorPos(out cursorPos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            SetCursorPos(cursorPos.X, cursorPos.Y);
        }

        if (Input.GetMouseButton(0))
        {
            yRot += Input.GetAxis("Mouse X");
            transform.localRotation = Quaternion.Euler(startingXRot, yRot, 0);
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }
}

public enum ECameraSetting
{
    KeyMovement,
    MouseMovement
}
