using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadReload : MonoBehaviour
{
    // A list to store all the objects that were marked with DontDestroyOnLoad
    private List<GameObject> dontDestroyObjects = new List<GameObject>();

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Awake()
    {
        // Find all the objects in the scene that were marked with DontDestroyOnLoad
        GameObject[] objects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objects)
        {
            if (obj.transform.parent == null && obj.hideFlags == HideFlags.None)
            {
                dontDestroyObjects.Add(obj);
                DontDestroyOnLoad(obj);
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reload all the objects that were marked with DontDestroyOnLoad
        foreach (GameObject obj in dontDestroyObjects)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
            }
        }
    }
}