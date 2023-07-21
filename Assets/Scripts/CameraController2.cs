using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{

    public float rotateSpeed = 10.0f, speed = 10.0f, zoomSpeed = 500.0f;
    private float _mult = 1f;

    private void Update()
    {
        //встроенный в Unity, отслеживает A и D, стрелки в право и лево 
        float horizontal = Input.GetAxis("Horizontal");
        //W и S, стрелаки вверх и вниз
        float vertical = Input.GetAxis("Vertical");

        float rotate = 0f;
        if (Input.GetKey(KeyCode.Q))
        {
            rotate = -1f;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            rotate = 1f;
        }

        _mult = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f;
        
        //Time.deltaTime - добавление плавности к действию
        //вращение объекта вокруг вертикальной оси
        //при нажатии кнопки Q или E камера вращается
        //Space.World - 1вращение каменры не относительно собственных, а относительно глобальных координат 
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * rotate * _mult,
            Space.World); 
        
        //передвигает камеру
        transform.Translate(new Vector3
            (horizontal, 0, vertical) * Time.deltaTime * _mult * speed,
            Space.Self); //hor/ver - поменять (возможно)

        //перемещение камеры колесом мыши
        transform.position += 
            transform.up * zoomSpeed * Time.deltaTime * _mult * Input.GetAxis("Mouse ScrollWheel");

        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y, -20f, 30f),
            transform.position.z);
    }
}
