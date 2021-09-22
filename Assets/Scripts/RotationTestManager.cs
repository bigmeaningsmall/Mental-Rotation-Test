using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RotationTestManager : MonoBehaviour{
    
    public List<GameObject> shapes;

    public Transform shapePositionZero;
    public Transform[] shapePositions = new Transform[4];
    // public Transform[] shapeColliders = new Transform[4];
    
    public int shapeTotal;
    public int shapeIndex = 0;

    public Transform[] s = new Transform[0];

    void Start(){
        shapeTotal = shapes.Count;

        for (int i = 0; i < shapes.Count; i++){
            shapes[i].SetActive(false);
        }

        // shapeColliders = shapePositions;
        
        SetShapes();
    }
    void SetShapes(){
        
        //deactivate all
        for (int i = 0; i < shapes.Count; i++){
            shapes[i].SetActive(false);
        }
        
        //activate shape at index
        shapes[shapeIndex].SetActive(true);
        
        //get child shapes from index
        s = new Transform[shapes[shapeIndex].transform.childCount];
        
        //shuffle positions
        ShufflePositions();
        
        //get and position shapes 
        for (int i = 0; i < s.Length; i++){
            s[i] = shapes[shapeIndex].transform.GetChild(i);
        }
        s[0].transform.position = shapePositionZero.position;
        for (int i = 0; i < shapePositions.Length; i++){
            s[i+1].transform.position = shapePositions[i].position;
            Debug.Log(s[i]);
            //animate
        }
    }
    
    public void ShufflePositions(){
        Transform temp;
        for (int i = 0; i < shapePositions.Length; i++) {
            int rnd = Random.Range(0, shapePositions.Length);
            temp = shapePositions[rnd];
            shapePositions[rnd] = shapePositions[i];
            shapePositions[i] = temp;
        }
        
    }
    
    //TODO - Work out how to get which shape is at which position
    void Update(){
        if (Input.GetMouseButtonDown(0)){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)){
                if (hit.transform == shapePositions[0]){
                    Debug.Log("shape selected: " + int.Parse(shapePositions[0].transform.name).ToString());
                    Debug.Log(shapePositions[0].transform);
                }
                if (hit.transform == shapePositions[1]){
                    Debug.Log("shape selected: " + shapePositions[1].transform.name);
                }
                if (hit.transform == shapePositions[2]){
                    Debug.Log("shape selected: " + shapePositions[2].transform.name);
                }
                if (hit.transform == shapePositions[3]){
                    Debug.Log("shape selected: " + shapePositions[3].transform.name);
                }
            }
        }
    }
}
