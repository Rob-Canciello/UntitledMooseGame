using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{

    public float rayLength;
    private RaycastHit hit;

    // Here we create a layermask so that we can assign specfic layers of "vision" to our raycast
    // Anything that's not on these layers, will NOT be seen by our raycast, and therefore ignored.
    public LayerMask layerMask;

    public Material highlightMaterial;
    private Material savedMaterial;

    private GameObject curObject;


    // Update is called once per frame
    void Update()
    {
   

        Debug.DrawRay(transform.position, transform.forward * rayLength, Color.magenta);

        // If on a given update frame, my raycast hits something...
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength, layerMask))
        {

            if(Input.GetKeyDown(KeyCode.E) && curObject != null)
            {
                ObjectInteraction(curObject);
            }

            // If curobject isn't assigned an object (null), then we have to:
            if (curObject == null)
            {
                curObject = hit.collider.gameObject;

                // Here we're saving the material of the object we're currently looking at into our
                // placeholder "savedMaterial", which is of the type Material, so it all works out. :D
                savedMaterial = curObject.GetComponent<MeshRenderer>().material;

                curObject.GetComponent<MeshRenderer>().material = highlightMaterial;
            }

            else if (curObject != hit.collider.gameObject)
            {
                NullifyCurObject();
            }
        }

        // ELSE, if we're NOT hitting anything this frame with our Raycast.
        else
        {
            if (curObject != null)
            {
                NullifyCurObject();
            }
        }
    }

    void NullifyCurObject()
    {
        curObject.GetComponent<MeshRenderer>().material = savedMaterial;
        curObject = null;
    }

    void ObjectInteraction(GameObject objectFromRaycast)
    {
        if(objectFromRaycast.tag == "Door")
        {
            
                print("Door");
        
        }
    }
}

