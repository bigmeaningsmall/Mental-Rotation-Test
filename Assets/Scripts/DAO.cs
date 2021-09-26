using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAO : MonoBehaviour{
    
    public static DAO instance;
    
    public Material defaultShapeMaterial;
    public Material highlightShapeMaterial;
    
    public Color defaultShapeColour;
    public Color highlightShapeColour;
    public Color correctShapeColour;
    public Color incorrectShapeColour;

    [Range(0,1)]
    public float animationDuration = 0.5f;
    [Range(0,2)]
    public float animationStrength = 1f;
    
    void Awake(){
        instance = this;
    }
}
