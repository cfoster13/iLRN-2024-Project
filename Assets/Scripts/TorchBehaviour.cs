using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchBehaviour : MonoBehaviour
{
    //public Sprite litObject;
    public GameObject fireParticlePrefab; // reference to the fire particle system prefab
    public Vector3 fireParticleOffset = new Vector3(0, 1, 0); // Offset for the particle system
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
            if (child.GetComponent<TorchIsHit>().GetIsColliding() && !child.GetComponent<TorchIsHit>().isLit)
            {
                // child.GetComponent<SpriteRenderer>().sprite = litObject;
                // Calculate the position with the offset
                Vector3 particlePosition = child.transform.position + fireParticleOffset;
                // Activate the fire particle system instead of changing the sprite
                Instantiate(fireParticlePrefab, particlePosition, Quaternion.identity, child.transform);
                
                child.GetComponent<TorchIsHit>().isLit = true;
            }
        }
    }


}
