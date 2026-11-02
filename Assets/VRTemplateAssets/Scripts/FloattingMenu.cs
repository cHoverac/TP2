using TMPro;
using UnityEngine;
using static UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics.HapticsUtility;

public class FloattingMenu : MonoBehaviour
{

    [SerializeField] private Transform controller;
    [SerializeField] private Transform panel;
    [SerializeField] private TextMeshProUGUI tmp;

    void Update()
    {
        Vector3 localPos = controller.InverseTransformPoint(panel.position);
        Vector3 worldPos = panel.position;

        tmp.text = "Local : " + localPos.ToString("F2") + "\n" +
                   "Global : " + worldPos.ToString("F2");
    }

    // Update is called once per frame
    void Start()
    {
        
    }
}
