using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPositionManager_02ScreenPointToRay : MonoBehaviour
{

    public LayerMask clickMask;

    //Update is called once per frame
    private void Update()
    {
        //check for user input
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(1))
        {
            //create a vector to store the mouse position
            Vector3 clickPosition = -Vector3.one;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //create a raycasthit but its autpmatically populated from where the ray will hit the collider

            RaycastHit hit;

            /* if(Physics.Raycast(ray,out hit)) //export out the information to hit
             {
                 clickPosition = hit.point;
                 GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                 primitive.transform.position = clickPosition;

                 Debug.Log(clickPosition);
                 */

            if (Physics.Raycast(ray, out hit, 100f, clickMask))
            {
                clickPosition = hit.point;
                clickPosition = hit.point;
                GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                primitive.transform.position = clickPosition;
            }
            Debug.Log(clickPosition);
        }
    }
}


