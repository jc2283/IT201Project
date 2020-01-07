using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickPositionManager_03PlaneRaycast : MonoBehaviour {

    public GameObject PreFabBrush1;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(1))
        {
            Vector3 clickPosition = -Vector3.one;

            //method 3: Raycast using plane

            Plane plane = new Plane(Vector3.forward, 0f);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distanceToPlane;

            if(plane.Raycast(ray, out distanceToPlane))
            {
                clickPosition = ray.GetPoint(distanceToPlane);
            }

            //Debug.Log(clickPosition);
            //primitive = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            //primitive.transform.position = clickPosition;
            //print out the position and spawn a sphere
            Debug.Log(clickPosition);
            // GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            // capsule.transform.position = clickPosition;

            GameObject tempGO = Instantiate(PreFabBrush1, clickPosition, Quaternion.identity);
            //Instantiate(PreFabBrush1);
            // PreFabBrush1.transform.position = clickPosition;
            Destroy(tempGO, 3f);
        }
    }


}
