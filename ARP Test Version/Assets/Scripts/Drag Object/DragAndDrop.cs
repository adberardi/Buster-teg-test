using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    bool isDrag;
    Transform focus;
    Camera cameraAR;
    Vector3 screenPos;
    Vector3 offset;
    RaycastHit hit;
    Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        isDrag = false;
        cameraAR = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 1)
        {
            ray = cameraAR.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                // Entra solo si entra en colision con otro objeto (usar colliders)
                focus = hit.collider.transform;
                Debug.Log(" Colision = " + focus.name);

                // screenPos: donde se ubica nuestro objeto en la pantalla.
                screenPos = cameraAR.WorldToScreenPoint(focus.position);
                //Obteniendo la posicion del objeto
                offset = focus.position - cameraAR.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z));
                isDrag = true;
            }
            else if (Input.touchCount == 0 && isDrag)
            {
                isDrag = false;
            }
        }
        else
        {
            if (isDrag)
            {
                // Se actualiza la posicion del objeto al moverse.
                Vector3 currentScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z);
                Vector3 currentPos = cameraAR.ScreenToWorldPoint(currentScreenPosition) + offset;

                focus.position = currentPos;
            }
        }
    }
}
