using UnityEngine;
using System.Collections;

public class CameraDetection : MonoBehaviour
{
    private int DetectionTTL = 0;
    private Color origColor;
    private Material materialReference;
    private MeshRenderer rendererReference;


    // Use this for initialization
    void Start()
    {
        rendererReference = GetComponent<MeshRenderer>();
        materialReference = rendererReference.material;
        origColor = materialReference.color;
    }

    
    void Update()
    {

    }

    void FixedUpdate()
    {
        /* Using Time To Live approach, this runs on every cube.
        if (DetectionTTL > 0)
        {
            DetectionTTL--;
            if (DetectionTTL == 0)
            {
                materialReference.color = origColor;
            }
        }
         */
    }

    void OnWillRenderObject()
    {
        if (Camera.current == Camera.main)
        {
            if (DetectionTTL == 0)
            {
                materialReference.color = Color.blue;
                StartCoroutine("CleanUp");
            }
            DetectionTTL = 1;
        }
    }

    IEnumerator CleanUp()
    {
        // Executes only for rendered cubes until timeout.
        while(true)
        {            
            yield return new WaitForSeconds(2);
            if (!rendererReference.isVisibleFrom(Camera.main))
            {
                DetectionTTL = 0;
                materialReference.color = origColor;
                break;
            }
        }
        yield return null;
    }
}
