using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBox : MonoBehaviour
{
    public void disable()
    {
        transform.GetComponent<Canvas>().enabled = false;
    }

}
