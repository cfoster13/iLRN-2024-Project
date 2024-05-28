using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBehaviour : MonoBehaviour
{
    bool isDragging = false;
    bool isColliding = false;

    Vector3 offset;
    Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        if (isColliding)
        {
            transform.position = originalPosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
    }
}
