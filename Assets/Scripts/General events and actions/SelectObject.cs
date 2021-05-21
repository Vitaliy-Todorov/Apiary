using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))  //Если нажали левую кнопку мыши
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //Создаем луч, который будет запущен от курсора экрана в 3D пространство, от камеры
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                IOnClick onClick = hit.transform.GetComponent<IOnClick>();
                if(onClick != null)
                    onClick.OnClick();
            }
        }
    }
}
