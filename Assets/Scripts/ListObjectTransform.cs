using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ListObjectTransform : MonoBehaviour
{
    private bool isFunctionRunning = false;
    public XRSimpleInteractable simpleInteractable;
    private bool typeS;
    public XRPokeFollowAffordance XRPFA;
    private bool logic = true;
    public float duration = 5.0f;
    private List<Vector3> listPObj = new List<Vector3>();
    [Serializable]
    public class KeyValuePair
    {
        public GameObject partOfModel;
        public Vector3 val;

    }

    
    void Start()
    {
        
    }

    public List<KeyValuePair> MyList = new List<KeyValuePair>();
    Dictionary<GameObject, Vector3> myDict = new Dictionary<GameObject, Vector3>();

    void Awake()
    {
       
        Debug.Log(simpleInteractable);
        // Подписываемся на событие "входа" (selectEntered)
        simpleInteractable.selectEntered.AddListener(OnSelectEntered);
        

        foreach (var kvp in MyList)
        {
            myDict[kvp.partOfModel] = kvp.val;
        }
        foreach (KeyValuePair<GameObject, Vector3> kvp in myDict)
        {
            
            listPObj.Add(kvp.Key.transform.position);
        }
    }

    

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (isFunctionRunning)
        {
            // Если функция уже выполняется, прекращаем выполнение
            return;
        }
        StartCoroutine(Logic());
    }

    public void TransformObj()
    {
        foreach (KeyValuePair<GameObject, Vector3> kvp in myDict)
        {
            StartCoroutine(MoveSmoothly(kvp.Key.transform, kvp.Value, duration, true));
            //kvp.Key.transform.localPosition = new Vector3(kvp.Value.x, kvp.Value.y, kvp.Value.z);
        }
        
    }

    public IEnumerator MoveSmoothly(Transform target, Vector3 targetPosition, float duration, bool typeS)
    {
        float elapsedTime = 0.0f;
        Vector3 initialPosition;
        if (typeS)
        {
            initialPosition = target.localPosition;
        }
        else
        {
            initialPosition = target.position;
        }
        
        while (elapsedTime < duration)
        {
            if (typeS)
            {
                target.localPosition = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            }
            else
            {
                target.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            }
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (typeS)
        {
            target.localPosition = targetPosition;
        }
        else
        {
            target.position = targetPosition;
        }
        
    }

    public void BackTransformObj()
    {
        int i = 0;
        foreach (KeyValuePair<GameObject, Vector3> kvp in myDict)
        {
            StartCoroutine(MoveSmoothly(kvp.Key.transform, listPObj[i], duration, false));
            //kvp.Key.transform.position = new Vector3(listPObj[i].x, listPObj[i].y, listPObj[i].z);
            i++;
        }
        
    }

    private IEnumerator Logic()
    {
        isFunctionRunning = true;

        if (logic)
        {
            TransformObj();
            logic = false; 
        }
        else
        {
            BackTransformObj();
            logic = true;
           
        }
        Debug.Log("Выполняется функция Unity");

        yield return new WaitForSeconds(duration);

        // Функция завершена, сбрасываем флаг
        isFunctionRunning = false;

        Debug.Log("Функция Unity завершена");
    }
}
