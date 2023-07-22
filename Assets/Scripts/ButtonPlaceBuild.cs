using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlaceBuild : MonoBehaviour
{

    public GameObject building; //строение

    public void PlaceBuild()
    {
        Instantiate(building, Vector3.zero /**расположение в начальных координатах*/,
            Quaternion.identity /**задание начального вращения (нет)*/); //этот метод позвонляет создать игровое объект/префаб
    }

}
