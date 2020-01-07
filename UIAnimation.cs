using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour {

    private bool isHidden = false;
    private IEnumerator coroutine;
    public float animTime = 2f;
    private Vector3 showPos, hidePos, startPos;
    public Button hideButton;

    private void Start()
    {
        showPos = Vector3.zero;
        hidePos = new Vector3(-275f, 0f, 0f);

        if(!Application.isEditor)
        {
            transform.position = hidePos;
            isHidden = true;
            MoveUI();
        }

    }
    public void MoveUI()
    {
        if(isHidden)
        {
            coroutine = MovingUI(showPos);
            isHidden = false;
        }
        else
        {
            coroutine = MovingUI(hidePos);
            isHidden = true;
        }
        StartCoroutine(coroutine);
    }
    private IEnumerator MovingUI(Vector3 endPos)
    {
        float elaspedTime = 0f;
        startPos = transform.position;
        hideButton.interactable = false;
        while(Vector3.Distance(transform.position,endPos)> 0.01f)
        {
            transform.localPosition = Vector3.Lerp(startPos, endPos, elaspedTime / animTime);
            elaspedTime += Time.deltaTime;
            yield return null;
        }
        hideButton.interactable = true;
    }
}
