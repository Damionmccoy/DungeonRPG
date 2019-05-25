using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomTransfer : MonoBehaviour
{
    //Changes the camera x and y when transitioning rooms
    public Vector2 CameraMinChange;
    public Vector2 CameraMaxChange;
    //Changes the player x and y when transitioning rooms
    public Vector2 playerChange;
    //This is the camera script attached to the main camera
    private CameraMovement cam;
    //set true is you want to display text when transitioning rooms
    public bool needText = false;
    //This is the TextMeshPro text object to display the transition text
    [SerializeField]
    string txt2Display;
    [SerializeField]
    TextMeshProUGUI biomeText;
    [SerializeField]
    float DisplayTime;

    

    // Start is called before the first frame update
    void Start()
    {
        //get the camera movement script from the main camera
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        //check if the player has entered the trigger
        if (_other.CompareTag("Player")&& !_other.isTrigger)
        {
            //change the player and camera positions to that in the need room
            cam.CameraMinPositon += CameraMinChange;
            cam.CameraMaxPosition += CameraMaxChange;
            _other.transform.position += new Vector3(playerChange.x, playerChange.y, _other.transform.position.z);

            //check if text is needed for the transition if so display the text for a time.
            if (needText)
            {
                StartCoroutine(DisplayBiomeTextCo());
            }
        }
    }

    private IEnumerator DisplayBiomeTextCo()
    {
        biomeText.gameObject.SetActive(true);
        biomeText.text = txt2Display;
        yield return new WaitForSeconds(DisplayTime);
        biomeText.text = "";
        biomeText.gameObject.SetActive(false);
    }
}
