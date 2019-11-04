using UnityEngine;

public class ARCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter:: " + other.name);
        other.GetComponent<MeshRenderer>().material.color = Color.black;
        // ...

        if(other.name == "x")
        {
            // ...
        }
    }

    private void OnTriggerStay(Collider other)
    {
        print("OnTriggerStay:: " + other.name);
        // ...
    }

    private void OnTriggerExit(Collider other)
    {
        print("OnTriggerExit:: " + other.name);
        other.GetComponent<MeshRenderer>().material.color = Color.white;
        // ...
    }
}