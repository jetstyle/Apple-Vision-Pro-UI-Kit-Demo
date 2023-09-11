using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadDemo : MonoBehaviour
{
    public GameObject UserPrefab;

    private void Start()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.LoadScene(1);
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        foreach (var es in FindObjectsOfType<EventSystem>())
            Destroy(es.gameObject);

        foreach (var cam in FindObjectsOfType<Camera>())
            Destroy(cam.gameObject);

        Instantiate(UserPrefab, Vector3.zero, Quaternion.identity);
    }
}
