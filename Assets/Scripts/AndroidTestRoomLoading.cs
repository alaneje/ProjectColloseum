using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AndroidTestRoomLoading : MonoBehaviour
{
    public int ScenenNumber;
    // Start is called before the first frame update
    void Start()
    {
        if(Application.platform == RuntimePlatform.Android && Debug.isDebugBuild)
        {
            SceneManager.LoadSceneAsync(ScenenNumber, LoadSceneMode.Additive);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
