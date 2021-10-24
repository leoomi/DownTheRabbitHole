using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    // maybe make a public toggle for free-cam (or something similar, not bound to the bounding box?)
    #region Publics
    public Transform target;
    public Camera cam;
    #endregion

    #region Serializables
    [SerializeField]
    readonly float scalefactor = 1920f;
    [SerializeField]
    public float rbound, lbound, tbound, bbound;
    [SerializeField]
    public GameObject gameFloor;
    [SerializeField]
    public float height;
    [SerializeField]
    public bool toggleScaling = false;
    protected float ratio = 2f;
    protected float smoothSpeed = 3f;
    public Vector2 fixedLocation = new Vector2(0, 0);
    public bool toggleFollow = false;

    #endregion

    #region Privates
// :)
    #endregion

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void setCameraTarget()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        gameFloor = GameObject.FindGameObjectWithTag("Floor"); // only have 1 gameobject with this tag please
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void scaleCameraOrthographicSize()
    {
        if (!toggleScaling)
        {
            return;
        }
        Resolution current = Screen.currentResolution;
        float aspect = (float)current.width / (float)current.height;
        float orthoSize = scalefactor / aspect / 200;
        if (cam) cam.orthographicSize = orthoSize;
        lbound = (aspect * current.width / current.height) - gameFloor.transform.localScale.x / 2f;
        rbound = gameFloor.transform.localScale.x / 2f -(aspect * current.width / current.height);
        tbound = orthoSize - gameFloor.transform.localScale.y / 2f;
        bbound = gameFloor.transform.localScale.y / 2f - orthoSize;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // should change only when changing resolution?
        // it will do for now
        scaleCameraOrthographicSize();

        if (target)
        {
            float time = Time.deltaTime;
            // maybe I should make these exposed?
            //float camY = Mathf.Clamp(target.transform.position.y, yMin + cam.orthographicSize, yMax - cam.orthographicSize);
            //float camX = Mathf.Clamp(target.transform.position.x, xMin + ((xMax + cam.orthographicSize) / ratio), xMax - ((xMax + cam.orthographicSize) / ratio));
            /*float camX = target.transform.position.x;
            float camY = target.transform.position.y;
            this.transform.position = new Vector3(camX, camY, this.transform.position.z);
            */

            if (toggleFollow)
            {
                Vector3 pos = new Vector3(target.position.x, target.position.y, height);
                // ensures that no errors appear if scaling isnt applied but target follow is
                pos.x = (toggleScaling is false ? pos.x : Mathf.Clamp(pos.x, lbound, rbound));
                pos.y = (toggleScaling is false? pos.y : Mathf.Clamp(pos.y, bbound, tbound));
                cam.gameObject.transform.position = pos;
            }
            else if (cam != null)
            {
                cam.gameObject.transform.position = new Vector3(fixedLocation.x, fixedLocation.y, height);
            }

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
