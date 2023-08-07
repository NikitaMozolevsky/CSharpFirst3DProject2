using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemy : MonoBehaviour
{
    //точки создания ангаров
    public Transform[] points;
    public GameObject factory;

    private void Start()
    {
        StartCoroutine(SpawnFactory());
    }

    private IEnumerator SpawnFactory()
    {
        for (int i = 0; i < points.Length; i++)
        {
            yield return new WaitForSeconds(10f);

            int randomRotation = UnityEngine.Random.Range(0, 360);
            //создание factory, но создаваться он будет на позиции мышки если указать координаты
            //в этой же функции, поэтому
            GameObject spawn = Instantiate(factory);
            //удаление скрипта установки дома
            Destroy(spawn.GetComponent<PlaceObjects>());
            spawn.transform.position = points[i].position; //установка позиции
            spawn.transform.rotation = Quaternion.Euler(new Vector3(0, randomRotation, 0));
            spawn.GetComponent<CarAutogeneration>().enabled = true;
        }
    }
}
