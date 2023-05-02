using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager3D : MonoBehaviour
{


    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material[] defaultMaterials;
    [SerializeField] private InventoryManager Inventoryscript;

    public Material[] temp; 

    public Transform _selection;

    // Update is called once per frame
    void Update()
    {
        

        if (_selection != null)
        {
            
            var selectionRenderer = _selection.GetComponent<MeshRenderer>();
            //defaultMaterials[0] = selectionRenderer.materials[0];
            //temp[0] = selectionRenderer.materials[0];
            selectionRenderer.materials = temp;
            
            _selection = null;
        }
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                var selectionRenderer = selection.GetComponent<MeshRenderer>();
                if(selectionRenderer != null)
                {
                    selectionRenderer.materials = defaultMaterials;
                }
                _selection = selection;
            }
        }
        

        if(_selection != null)
        {
            if (Input.GetKeyDown("e"))
            {
                _selection.gameObject.SetActive(false);
                Inventoryscript.collectedObjects++;
            }
        }
    }
}
