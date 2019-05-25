using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Target Variables")]
    public Transform target;
    [Range(0,1),Header("Smoothing Effect")]
    public float smoothing;
    [Header("Camera Bounding Values")]
    public Vector2 CameraMinPositon;
    public Vector2 CameraMaxPosition;
    public VectorValue CameraResetMin;
    public VectorValue CameraResetMax;
    //Private Variables
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize the camera by setting its location to the targets location
        transform.position = new Vector3( target.position.x, target.position.y,transform.position.z);
        //Set the cameras animator this is for screen effects 
        anim = GetComponent<Animator>();
        //Set the camera to the starting position
        CameraMinPositon = CameraResetMin.InitialValue;
        CameraMaxPosition = CameraResetMax.InitialValue;
    }

    
    void LateUpdate()
    {
        //Check if the target is moving
        if(transform.position != target.position)
        {
            //If the target has moved grab its new position
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
            //This keeps the camera set to the targets position unless it will move the camera out of its bounds
            targetPos.x = Mathf.Clamp(targetPos.x, CameraMinPositon.x, CameraMaxPosition.x);
            targetPos.y = Mathf.Clamp(targetPos.y, CameraMinPositon.y, CameraMaxPosition.y);
            //Use a lerp to move the camera to the target location using the smoothing value set 
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        }
    }

    /// <summary>
    /// This function just activates the kick animation  
    /// </summary>
    public void CameraKick()
    {
        anim.SetBool("kick active", true);//Activate the camera anaimation
        StartCoroutine(ScreenKickCo());
    }

    /// <summary>
    /// This coroutine just waits one frame and sets the camera back to the idle anaimation. 
    /// </summary>
    /// <returns></returns>
    public IEnumerator ScreenKickCo()
    {
        yield return null;
        anim.SetBool("kick active", false); //set the camera animation back to idle
    }
}
