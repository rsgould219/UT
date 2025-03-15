using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckController : MonoBehaviour
{
    void Start(){

    }
    // Start is called before the first frame update
    void Update(){
        WorldController.Instance.loadFromMenuCheck();
    }
}
