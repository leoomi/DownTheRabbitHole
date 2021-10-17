using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    // maybe make a public toggle for free-cam (or something similar, not bound to the bounding box?)
    #region Publics
    public Transform target;
    #endregion

    #region Serializables
    [SerializeField]
    protected float xMin, xMax, yMin, yMax;
    protected float ratio = 2f;
    protected float smoothSpeed = 3f;
    #endregion

    #region Privates
    private Camera cam;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        cam = GameObject.Find("Camera").GetComponent<Camera>();
        // target = GameObject.Find("Player").transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            float time = Time.deltaTime;
            // maybe I should make these exposed? 
            //float camY = Mathf.Clamp(target.transform.position.y, yMin + cam.orthographicSize, yMax - cam.orthographicSize);
            //float camX = Mathf.Clamp(target.transform.position.x, xMin + ((xMax + cam.orthographicSize) / ratio), xMax - ((xMax + cam.orthographicSize) / ratio));
            float camX = target.transform.position.x;
            float camY = target.transform.position.y;
            this.transform.position = new Vector3(camX, camY, this.transform.position.z);
            /*Vector3.Lerp(
                this.transform.position,
                new Vector3(camX, camY, this.transform.position.z), smoothSpeed
                // Mathf.Lerp(target.transform.position.x, camX, time * time * (tstart - ratio * time)),
                // Mathf.Lerp(target.transform.position.y, camY, time * time * (tstart - ratio * time)),
                //camX,
                //camY,
                //this.transform.position.z
                );*/
        }
    }
}
