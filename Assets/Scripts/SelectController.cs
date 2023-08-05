using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class SelectController : MonoBehaviour
{
    [FormerlySerializedAs("selectCube")] public GameObject cube;
    public List<GameObject> players;
    private Camera _camera;
    public LayerMask layer, layerMask;
    private GameObject _cubeSelection;
    private RaycastHit _hit;

    //метод срабатываюший еще до метода Start()
    //в нем удобно получать различные компоненты
    private void Awake()
    {
        //скрипт находится на Main Camera,
        //идет обращение к Camera
        _camera = GetComponent<Camera>(); 
    }

    private void Update()
    {
        //логика передвижения игроков
        if (Input.GetMouseButtonDown(1) && players.Count > 0)
        {
            //определяем куда нажал пользователь
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit agentTarget, 1000f, layer))
                foreach (var el in players)
                    //указание точки в которую хотм отправить всех выбраных агентов
                    el.GetComponent<NavMeshAgent>().SetDestination(agentTarget.point); 
        }
        //отслеживание нажатия пользователя по мышке
        if (Input.GetMouseButtonDown(0))
        {
            foreach (var el in players)
            {
                el.transform.GetChild(0).gameObject.SetActive(false);
            }
            
            players.Clear(); //очищение списа объектов при нажатии selection
            
            //остлеживание координат мыши относительно того куда смотрит камера сейчас
            //нужно выпустить луч, и с чем этот луч соприкоснется
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            //в этом объекте будет вся информация относительно той 
            //позиции где луч соприкоснулся и другим объектом

            if (Physics.Raycast(ray, out _hit, 1000f /**длинна луча*/,
                    layer /**слой на который будет реагировать луч*/))
            {
                //создание куба селекции и для возможности удаление присвоение ссылки
                //на куб 
                _cubeSelection = Instantiate(cube, 
                    //позиция на которой создается куб селекции
                    new Vector3(_hit.point.x, 1, _hit.point.z), Quaternion.identity);
            }
        }

        //если куб существует - значит уже нажали по экрану, можно его увеличивать
        //в зависимости от движения мыши 
        if (_cubeSelection) 
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitDrag, 1000f /**длинна луча*/,
                    layer /**слой на который будет реагировать луч*/))
            {
                float xScale = (_hit.point.x - hitDrag.point.x) * -1;
                float zScale = _hit.point.z - hitDrag.point.z;

                if (xScale < 0.0f && zScale < 0.0f) 
                {
                    //вращение куба на 180 градусов
                    _cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                } 
                else if (xScale < 0.0f) 
                {
                    _cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                } 
                else if (zScale < 0.0f) 
                {
                    _cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(180, 0, 0));
                }

                else
                {
                    _cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                
                _cubeSelection.transform.localScale = new Vector3(Mathf.Abs(xScale), 1, Mathf.Abs(zScale));
            }
                
        }
        
        //уничтожение куба при отпускании мыши, а так же выбор объектов
        //если кнопка мыши поднимается 
        //и удаление куба только если он существует
        if (Input.GetMouseButtonUp(0) && _cubeSelection)
        {
            //получение всех объектов слоя Player которые попали в cubeSelection
            RaycastHit[] hits = Physics.BoxCastAll(
                _cubeSelection.transform.position, //позиция начала куба
                _cubeSelection.transform.localScale, //размер куба
                Vector3.up, //выбираются все элементы выше куба или попадающие в куб
                Quaternion.identity, //вращение
                0, //максимальное расстояние
                layerMask); //элементы с каким слоем выбирать

            foreach (var el in hits)
            {
                players.Add(el.transform.gameObject);
                //показ полосы здоровья, первый дочерний элемент каждого объекта становится активным
                el.transform.GetChild(0).gameObject.SetActive(true);
            }
            /*
            foreach (var el in hits) //добавление элементов внутрь списка
            {
                players.Add(el.transform.gameObject);
            }*/
            
            Destroy(_cubeSelection);
        }
    }
}

