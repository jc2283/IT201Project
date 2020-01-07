using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickPositionManager_Sprint01 : MonoBehaviour {

    private int brushes = 0;
    private GameObject primitive;
    private float red = 1f, green = 1f, blue = 1f;
    public Text mousePosition;

    [SerializeField]
    private float distance = 5f, distanceChange;

    private Vector3 clickPosition;
    //private bool timedDestoryIsOn = true;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                Destroy(hit.transform.gameObject);
            }
        }
        if( Input.GetMouseButtonDown(1) || Input.GetMouseButton(1))//right click or hold

        {
            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0f, 0f, distance));

            switch(brushes)
            {

                case 0:
                    break;

                case 1:
                    primitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    break;

                case 2:
                    primitive = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    break;

                case 3:
                    primitive = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    break;
                 

            }

            primitive.transform.localScale = new Vector3(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), Random.Range(0.1f, 1f));
            primitive.transform.position = clickPosition;
            primitive.GetComponent<Renderer>().material.color = new Vector4(Random.Range(0f, red), Random.Range(0f, green), Random.Range(0f, blue), 1f);
            primitive.transform.parent = this.transform;

           // if(timedDestoryIsOn)
           // {
           //     Destroy(primitive, 3f);
           // }

        }
        mousePosition.text = "Mouse Position x: " + Input.mousePosition.x.ToString("F0") + ", y: " + Input.mousePosition.y.ToString("F0");

    }
    public void changeShape(int tempShape)
    {
        brushes = tempShape;
    }

    public void changeRed(float tempRed)
    {
        red = tempRed;
    }

    public void changeGreen(float tempGreen)
    {
        green = tempGreen;
    
    }
    public void changeBlue(float tempBlue)
    {
        blue = tempBlue;
    }

    public void destoryObjects()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);

        }
    }

   // public void ToggleTimedDestroy(bool timer)
   // {
       // timedDestoryIsOn = timer;
   // }
    public void ChangeDistance(float change)
    {
        distance = change;
    }

}
