using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Header("New scene variables")]
    public string SceneToLoad;
    public Vector2 PlayerPosition;
    public VectorValue StoredPlayerPosition;
    public Vector2 NewCameraMinPosition;
    public Vector2 NewCameraMaxPosition;
    public VectorValue CameraMinPosition;
    public VectorValue CameraMaxPosition;


    [Header("Scene transition effect variables")]
    public GameObject SceneTransitionOut; //this would be a fade out object or something similar. 
    public GameObject SceneTransitionIn;  //this would be a fade in object or something similar.
    public float FadeWait;

    private void Awake()
    {
        if(SceneTransitionIn != null)
        {
            GameObject panel = Instantiate(SceneTransitionIn, Vector3.zero, Quaternion.identity) as GameObject;
            //destroy after 1 second
            Destroy(panel, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.CompareTag("Player") && !_other.isTrigger)
        {
            StoredPlayerPosition.InitialValue = PlayerPosition;
            //SceneManager.LoadScene(SceneToLoad);
            StartCoroutine(TansitionCo());
        }

    }

    public IEnumerator TansitionCo()
    {
        if (SceneTransitionOut != null)
        {
            Instantiate(SceneTransitionOut, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(FadeWait);
        ResetCameraBounds();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    public void ResetCameraBounds()
    {
        CameraMaxPosition.InitialValue = NewCameraMaxPosition;
        CameraMinPosition.InitialValue = NewCameraMinPosition;
    }
}
