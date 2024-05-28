using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchBehaviour : MonoBehaviour
{
    public Sprite litObject;
    public List<GameObject> torches;

    public bool torchesLoaded;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            torches.Add(child.gameObject);
        }
        torchesLoaded = true;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject child in torches)
        {
            if (child.GetComponent<TorchIsHit>().GetIsColliding())
            {
                child.GetComponent<SpriteRenderer>().sprite = litObject;
                child.GetComponent<TorchIsHit>().isLit = true;
            }
        }
    }


}
