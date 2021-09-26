using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShapeSelector : MonoBehaviour
{
    private Material shapeMat;
    
    private Color defaultC;
    private Color highlightC;
    private Color correctC;
    private Color incorrectC;
    private float d;
    private float str;

    public Transform[] t = new Transform[0];
    public Renderer[] r = new Renderer[0];
    
    void Start(){
        // t = new Transform[transform.childCount];
        // r = new Renderer[t.Length];

        shapeMat = DAO.instance.defaultShapeMaterial;
        
        defaultC = DAO.instance.defaultShapeColour;
        highlightC = DAO.instance.highlightShapeColour;
        correctC = DAO.instance.correctShapeColour;
        incorrectC = DAO.instance.incorrectShapeColour;

        d = DAO.instance.animationDuration;
        str = DAO.instance.animationStrength;
        
        t = GetComponentsInChildren<Transform>();
        r = GetComponentsInChildren<Renderer>();
        
        for (int i = 0; i < r.Length; i++){
            
        }
        Initialise();
    }

    void Initialise(){
        for (int i = 0; i < r.Length; i++){
            r[i].material = shapeMat;
            r[i].material.color = defaultC;
        }   
    }
    public void Select(){
        StartCoroutine(ColourShapes(highlightC, 0.025f));
        StartCoroutine(AnimateShapes(str,0.025f));
    }
    public void Correct(){
        StartCoroutine(ColourShapes(correctC, 0.025f));
        StartCoroutine(AnimateShapes(str,0.025f));
    }
    public void Incorrect(){
        StartCoroutine(ColourShapes(incorrectC, 0.025f));
        StartCoroutine(AnimateShapes(str,0.025f));
    }
    public void Reset(){
        StartCoroutine(ColourShapes(defaultC, 0.01f));
        StartCoroutine(AnimateShapes(str/4,0.01f));
    }
    public void ResetColour(){
        StartCoroutine(ColourShapes(defaultC, 0.01f));
    }
    private IEnumerator ColourShapes(Color c, float w){
        for (int i = 0; i < r.Length; i++){
            r[i].material.DOColor(c, d);
            yield return new WaitForSeconds(w);
        }
    }
    private IEnumerator AnimateShapes(float s, float w){
        for (int i = 0; i < t.Length; i++){
            t[i].DOShakeScale(d, s, 10, 90f, true);
            yield return new WaitForSeconds(w);
        }   
        yield return new WaitForSeconds(d);
        for (int i = 0; i < t.Length; i++){
            t[i].DOScale(new Vector3(1, 1, 1), d/4);
        }   
    }

}
