using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    /*public float rotationSpeed = 10f;
    public float movementSpeed = 10f;

    private void Update()
    {
        // Поворот камеры
        float rotationX = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.Rotate(Vector3.up, rotationX);

        // Перемещение камеры
        float translationX = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float translationZ = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        float translationY = Input.GetAxis("Jump") * movementSpeed * Time.deltaTime;
        transform.Translate(translationX, translationY, translationZ);

        // Вращение камеры по вертикали
        float rotationY = Input.GetAxis("CameraUpDown") * rotationSpeed;
        Camera.main.transform.Rotate(Vector3.right, rotationY);
    }*/ // Скорость приближения/отдаления
    
    public float minZoom = 0.2f; // Минимальный зум
    public float maxZoom = 3f; // Максимальный зум

    public float rotateSpeed = 10.0f, speed = 10.0f, zoomSpeed = 10.0f;
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
        
        /*//Приближение при прокрутке колеса ммыши
        transform.position += transform.up * zoomSpeed * Time.deltaTime *
                              Input.GetAxis("Mouse ScrollWheel");*/
        
        // Получаем значение колеса мыши
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");

        // Изменяем размер объекта в соответствии с колесом мыши
        transform.localScale += new Vector3(scrollWheelInput, scrollWheelInput, scrollWheelInput) 
                                * zoomSpeed * _mult * Time.deltaTime;

        // Ограничиваем размер объекта в пределах minZoom и maxZoom
        transform.localScale = Vector3.ClampMagnitude(transform.localScale, maxZoom);

        // Если выходит за пределы допустимого размера, возвращаем его обратно
        if (transform.localScale.magnitude < minZoom)
        {
            transform.localScale = transform.localScale.normalized * minZoom;
        }
    }
}
