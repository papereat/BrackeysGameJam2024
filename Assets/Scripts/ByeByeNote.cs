using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ByeByeNote : MonoBehaviour
{
    public void ByeBye()
    {
        transform.GetComponent<Canvas>().enabled = false;
    }
}
