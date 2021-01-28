using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreProject.Pool;

public class GameCont : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PoolerManager.Instance.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PoolerManager.Instance.GetPoolObjectForSeconds("EmptyGO", 3f);
        }
    }
}
