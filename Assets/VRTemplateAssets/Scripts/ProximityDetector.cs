using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private Transform controller;
    [SerializeField] private GameObject[] cubes;
    [SerializeField] private Color proximityColor = Color.green;
    [SerializeField] private float scale = 1.2f;
    [SerializeField] private float triggerDistance = 2f;
    [SerializeField, Range(0f, 180f)] private float triggerAngle = 20f;

    private Color[] originalColors;
    private Vector3[] originalScales;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

     // RECUPERATION COULEUR ET SCALE ORIGINALE DES CUBES
            originalColors = new Color[cubes.Length];
            originalScales = new Vector3[cubes.Length];
            for (int i = 0; i < cubes.Length; i++)
            {
                originalColors[i] = cubes[i].GetComponent<Renderer>().material.color;
                originalScales[i] = cubes[i].transform.localScale;
            }

     // AJOUT D'UN LINE RENDERER AU CONTROLLER
        LineRenderer lineRenderer = controller.GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;

        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;

        lineRenderer.positionCount = 2;

        // AJOUT D'UN TEXTE 3D AU CONTROLLER
        GameObject textObject = new GameObject("DistanceText");
        textObject.transform.SetParent(controller);
        textObject.transform.localPosition = new Vector3(0, 0.1f, 0.2f);
        textObject.transform.localRotation = Quaternion.identity;
        TextMesh textMesh = textObject.AddComponent<TextMesh>();
        textMesh.text = "";
        textMesh.fontSize = 32;
        textMesh.characterSize = 0.05f;          
        textMesh.anchor = TextAnchor.LowerCenter;
        textMesh.alignment = TextAlignment.Center;
        textMesh.color = Color.white;

    }

    // Update is called once per frame
    void Update()
    {
        LineRenderer lineRenderer = controller.GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

        TextMesh textMesh = controller.GetComponentInChildren<TextMesh>();
        textMesh.text = "";


        for (int i = 0; i < cubes.Length; i++)
        {
            float distance = Vector3.Distance(controller.position, cubes[i].transform.position);
            float angle = Vector3.Angle(controller.forward, cubes[i].transform.position - controller.position);


            if (distance < triggerDistance && angle < triggerAngle)
            {
             // CHANGEMENT DE COULEUR ET DE SCALE DES CUBES
                cubes[i].GetComponent<Renderer>().material.color = proximityColor;
                cubes[i].transform.localScale = Vector3.one * scale;


             // AFFICHAGE DU LINE RENDERER ENTRE LE CONTROLLER ET LE CUBE
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, controller.position);
                lineRenderer.SetPosition(1, cubes[i].transform.position);

             // AFFICHAGE DE LA DISTANCE ENTRE LE CONTROLLER ET LE CUBE
                
                textMesh.text = distance.ToString("F2") + "m, " + angle.ToString("F2") + "°";
            }
            else
            {
             // RESTAURATION DE LA COULEUR ET DU SCALE ORIGINALE DES CUBES
                cubes[i].GetComponent<Renderer>().material.color = originalColors[i];
                cubes[i].transform.localScale = originalScales[i];


            }
        }


    }
}
