using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour
{

    public LayerMask layer;
    private void Update()
    { //остлеживание координат мыши относительно того куда смотрит камера сейчас
        //нужно выпустить луч, и с чем этот луч соприкоснется
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //в этом объекте будет вся информация относительно той 
        //позиции где луч соприкоснулся и другим объектом
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f /**длинна луча*/,
                layer /**слой на который будет реагировать луч*/))
        {
            //данный скрипт находится на самом домике, поэтому это его position
            //изменение позиции дома
            transform.position = hit.point; //передвидение дома куда смотрит камера
        }
        
        if (Input.GetMouseButtonDown(1)) //отслеживание правой клавиши мыши
            Destroy(gameObject.GetComponent<PlaceObjects>()); //удаление скрипта передвигающего дом
    }
}
