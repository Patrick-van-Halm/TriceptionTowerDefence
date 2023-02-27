using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = CameraManager.Instance.MainCamera.transform.rotation;
        transform.eulerAngles += Vector3.up * 180;
    }
}
