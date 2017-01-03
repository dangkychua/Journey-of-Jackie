using UnityEngine;
using System.Collections;

public class ThrowController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 10); // 20sec
    }
}
