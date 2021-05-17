using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billdoard : MonoBehaviour
{
    [SerializeField]
    Transform cam;

    public void AddCamera(Transform cam)
    {
        Debug.Log(cam.position);
        this.cam = cam;
    }

    //LateUpdate вызывается после вызова всех функций обновления
    void LateUpdate()
    {
        //Поворачивает transform так, чтобы прямой вектор указывал на текущее положение
        transform.LookAt(transform.position + cam.forward);
    }
}
