using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneMaster
{
   
    public static void UnloadScene(string Name)
    {
        SceneManager.UnloadSceneAsync(Name);
    }

    public static void LoadScene(string Name)
    {
        SceneManager.LoadSceneAsync(Name, LoadSceneMode.Additive);
    }

    public static void SetActiveScene(Scene scene)
    {
        SceneManager.SetActiveScene(scene);
    }
}
