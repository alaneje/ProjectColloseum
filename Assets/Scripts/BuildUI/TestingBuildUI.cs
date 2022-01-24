using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingBuildUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLoadedGame()
    {
        SceneMaster.LoadScene("Combat");
        SceneMaster.UnloadScene(this.gameObject.scene.name);
    }
}
