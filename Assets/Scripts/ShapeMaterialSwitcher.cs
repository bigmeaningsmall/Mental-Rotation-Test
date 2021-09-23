using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeMaterialSwitcher : MonoBehaviour
{
    // this script is placed on each shape and used to switch the child cubes to a spectific material 
    void Awake()
    {
        
    }

    void Start()
    {
        //maybe assign all materials from a parent singleton class so modifications can be made easily
    }
    
    void Update()
    {
        //lerp materials???
    }

    public void SwitchMaterial()
    {
        Debug.Log("CHOOSE TARGET MATERIAL");
    }
}
