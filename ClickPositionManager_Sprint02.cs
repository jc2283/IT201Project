using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickPositionManager_Sprint02 : MonoBehaviour
{

    private int brushes = 0;
    private GameObject primitive;
    private float red = 1f, green = 1f, blue = 1f, destroyTime = 3f, timeToDestroy = 3f, Xcuttoff;
    public Text mousePosition, sizeAmount, timerAmount, animAmount;

    [SerializeField]
    private float distance = 5f, distanceChange;

    private Vector3 clickPosition;
    private bool timedDestoryIsOn = true, isAnimTypeRandom, isAnimSpeedRandom, isSpawnTypeRandom, isSpawnTimeRAndom;
    public bool animationSpeedIsOn  = false;


    private Vector3 lastClickPosition = Vector3.zero;
    public Text lifetime;
    public float size = 2f;

    public GameObject paintedObject00, paintedObject01, paintedObject02, paintedObject03,paintedObject04;
    private Color paintedObjectColor, paintedObjectEmission, paintedOpacity;

    public Clock clock;

   // [SerializeField]
    //[Range(0.1f,1f)]
    private float emissionStrength = 0.2f;
    private float opacityStrength = 0.2f;

    public int animationState = 0;
    private float animationSpeed = 1f;

    public Dropdown animDropDown,shapeDropDown;

     void Update()

    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeAnimationState(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeAnimationState(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) ChangeAnimationState(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) ChangeSpawnObject(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) ChangeSpawnObject(4);
        if (Input.GetKeyDown(KeyCode.Alpha6)) ChangeSpawnObject(5);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == 11)//clock
                {
                    hit.transform.parent.GetComponent<Clock>().UpdateTime(hit.transform.localEulerAngles.y);
                   // Debug.Log(hit.transform.rotation.ToString());
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == 12)//paintedobject
                {
                    Destroy(hit.transform.gameObject);
                }
            }
           
        }

        if(Input.GetMouseButtonUp(1))
        {
            lastClickPosition = Vector3.zero;
        }


        if( Input.GetMouseButtonDown(1) || Input.GetMouseButton(1))//right click or hold

        {
            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0f, 0f, distance));

            if(isSpawnTimeRAndom)
            {
                changeShape((int)Random.Range(0.0f,1.99f));
            }

            switch(brushes)
            {

                case 0:
                    //primitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    primitive = Instantiate(paintedObject01, clickPosition, Quaternion.identity);
                    break;

                case 1:
                    //primitive = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    primitive = Instantiate(paintedObject02, clickPosition, Quaternion.identity);
                    break;

                case 2:
                    //primitive = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    primitive = Instantiate(paintedObject03, clickPosition, Quaternion.identity);
                    break;
                case 3:
                    //primitive = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    primitive = Instantiate(paintedObject04, clickPosition, Quaternion.identity);
                    break;
                default:
                    break;

            }


            if (primitive.GetComponent<Renderer>() != null)
            {
                paintedObjectColor = new Color(Random.Range(0.0f, red), Random.Range(0.0f, green), Random.Range(0.0f, blue));
                primitive.GetComponent<Renderer>().material.color = paintedObjectColor;
                paintedObjectEmission = new Color(paintedObjectColor.r * emissionStrength, paintedObjectColor.g * emissionStrength, paintedObjectColor.a * emissionStrength);
                primitive.GetComponent<Renderer>().material.SetColor("_EmissionColor", paintedObjectEmission);
            }
            else { }

            foreach (Transform child in primitive.transform)
            {
                if (child.gameObject.GetComponent<Renderer>() != null)
                {
                    paintedObjectColor = new Color(Random.Range(0.0f, red), Random.Range(0.0f, green), Random.Range(0.0f, blue));
                    child.gameObject.GetComponent<Renderer>().material.color = paintedObjectColor;
                    primitive.gameObject.GetComponent<PrefabData>().initialColorInfo.Add(paintedObjectColor);
                    paintedObjectEmission = new Color(paintedObjectColor.r * emissionStrength, paintedObjectColor.g * emissionStrength, paintedObjectColor.a * emissionStrength);
                    child.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", paintedObjectEmission);
                }

            }

            if(primitive.GetComponent<Animator>() != null)
            {
                if(isAnimTypeRandom)
                { animationState = (int)Random.Range(0f, 2.99f);
                    animDropDown.value = animationState;
                }
            
                primitive.GetComponent<Animator>().SetInteger("state", animationState);

            if (isAnimSpeedRandom) primitive.GetComponent<Animator>().speed = Random.Range(0f, animationSpeed);
            else primitive.GetComponent<Animator>().speed = animationSpeed;
            primitive.GetComponent<PrefabData>().initialAnimSpeed = primitive.GetComponent<Animator>().speed;
            //primitive.GetComponent<Animator>().speed = animationSpeed;
            }
           // primitive.transform.localScale = new Vector3(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), Random.Range(0.1f, 1f));
           // primitive.transform.position = clickPosition;
           // primitive.GetComponent<Renderer>().material.color = new Vector4(Random.Range(0f, red), Random.Range(0f, green), Random.Range(0f, blue), 1f);
           // primitive.transform.parent = this.transform;
          

           

            primitive.transform.parent = this.transform;

            if (timedDestoryIsOn)
            {
                if (isSpawnTimeRAndom) Destroy(primitive, Random.Range(0f, timeToDestroy));
                else Destroy(primitive, timeToDestroy);
            }
            lastClickPosition = clickPosition;

            if(animationSpeedIsOn)
            {
               
               primitive.gameObject.GetComponent<Animator>().speed = 0;
                    
            }

        }
        mousePosition.text = "Mouse Position x: " + Input.mousePosition.x.ToString("F0") + ", y: " + Input.mousePosition.y.ToString("F0");

    }
    public void changeShape(int temp)
    {
        brushes = temp;
        
    }
    public void ChangeAnimationTypeRandom(bool temp)
    {
        isAnimTypeRandom = temp;
    }
    public void ChangeAnimationSpeedRandom(bool temp)
    {
        isAnimSpeedRandom = temp;
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

    public void ToggleTimedDestroy(bool timer)
    {
        timedDestoryIsOn = timer;
    }

    public void ToggleAnimationSpeed(bool count)
    {
        animationSpeedIsOn = count;
    }


    public void ChangeDistance(float change)
    {
        distance = change;
    }
    public void ChangeEmission(float temp)
    {

        emissionStrength = temp;
    }

    public void ChangeSpawnObject(int temp)
    {
        brushes = temp;
        shapeDropDown.value = temp;
    }


    public void ChangeOpacity(float temp)
    {
        foreach(Transform child in transform)
        {
            if (child.gameObject.GetComponent<Renderer>() != null)
            {
                paintedObjectColor = child.GetComponent<Renderer>().material.GetColor("_Color");
                paintedObjectColor.a = temp;
                child.GetComponent<Renderer>().material.SetColor("_Color", paintedObjectColor);
            }
        }
        opacityStrength = temp;
    }

    public void ChangeSize(float temp)
    {
        foreach (Transform child in transform)
        {
            child.localScale = child.localScale * temp / size;
        }
        size = temp;
    }


    public void ChangeAnimationSpeed(float temp)
    {
        foreach(Transform child in transform)
        {
            if (child.gameObject.GetComponent<Animator>() != null)
            {
                child.gameObject.GetComponent<Animator>().speed = child.GetComponent<PrefabData>().initialAnimSpeed * temp ;
            }
        }
        animationSpeed = temp;
    }

    public void ChangeAnimationState(int temp)
    {
        animationState = temp;
        animDropDown.value = animationState;

        foreach(Transform child in transform)
        {
            if (child.gameObject.GetComponent<Animator>() != null)
            {
                child.gameObject.GetComponent<Animator>().SetInteger("state", animationState);
;            }
        }
    }
}
