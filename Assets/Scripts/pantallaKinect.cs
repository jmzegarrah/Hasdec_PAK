using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pantallaKinect : MonoBehaviour {
    public string SceneToLoad = "ModulosEntrenamiento";
    public float DelayTime = 5.0f;
    // Use this for initialization
    void Start () {
        StartCoroutine("Wait");

    }
    
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(DelayTime);

        SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Single);

    }

}
