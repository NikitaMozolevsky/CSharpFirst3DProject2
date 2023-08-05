using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAutogeneration : MonoBehaviour
{

    public GameObject car;
    public float timeBetweenCreation = 5.0f; 
    
    // Start is called before the first frame update
    void Start()
    {
        //Куратина - выполнение кода через определенные промежутки времени
        StartCoroutine(SpawnCar());
    }

    IEnumerator SpawnCar()
    {
        for (int i = 0; i <= 3; i++)
        {
            //ожидание перед созданием первой машинки
            yield return new WaitForSeconds(timeBetweenCreation);
            //создание машины в условленной точке
            //скрипт устанавливается на ангаре
            Instantiate(car, transform.GetChild(0).position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
