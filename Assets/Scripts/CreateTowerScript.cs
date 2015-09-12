using UnityEngine;
using System.Collections;
using System.Linq;
using Vuforia;

public class CreateTowerScript : MonoBehaviour, IVirtualButtonEventHandler
{
    public GameObject[] Prefabs;

    private int SelectedTower = 0;
    private GameObject[] Towers = new GameObject[4];
    private GameObject towerHere;

    private RaycastHit[] allHits;

    void Start()
    {
        //Register virtual buttons
        VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        foreach (VirtualButtonBehaviour item in vbs)
        {
            item.RegisterEventHandler(this);
        }

        //Init tower models
        var PlacesObj = transform.Find("Places");

        var NUM_TOWERS = 4;
        if (Prefabs.Length == NUM_TOWERS && PlacesObj.childCount == NUM_TOWERS)
        {
            for (int i = 0; i < NUM_TOWERS; i++)
            {
                Towers[i] = Instantiate(Prefabs[i]);
                Towers[i].transform.parent = transform;
                Towers[i].transform.position = PlacesObj.GetChild(i).transform.position;
            }

            towerHere = Instantiate(Towers[0]);
            towerHere.transform.parent = transform;
            towerHere.transform.localScale = Towers[0].transform.localScale;
            towerHere.GetComponent<MeshRenderer>().enabled = false;

            Towers[0].transform.localScale = new Vector3(
                Towers[0].transform.localScale.x * 1.5f,
                Towers[0].transform.localScale.y * 1.5f,
                Towers[0].transform.localScale.z * 1.5f);
        }
    }

    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        GameObject graphicButton = null;
        //Debug.Log("Pressed");
        switch (vb.VirtualButtonName)
        {
            case "Next":
                graphicButton = transform.Find("Next").gameObject;
                break;
            case "Select":
                graphicButton = transform.Find("Select").gameObject;
                break;
        }

        if (graphicButton != null)
        {
            var meshrenderer = graphicButton.GetComponent<MeshRenderer>();
            meshrenderer.material.color = Color.blue;
        }
    }

    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
        GameObject graphicButton = null;
        switch(vb.VirtualButtonName)
        {
            case "Next":
                graphicButton = transform.Find("Next").gameObject;
                SelectNextTower();
                break;
            case "Select":
                graphicButton = transform.Find("Select").gameObject;
                SpawnCreateTower();
                break;
        }

        if (graphicButton != null)
        {
            var meshrenderer = graphicButton.GetComponent<MeshRenderer>();
            meshrenderer.material.color = Color.white;
        }
    }

    private void SelectNextTower()
    {
        Towers[SelectedTower].transform.localScale = new Vector3(
            Towers[SelectedTower].transform.localScale.x / 1.5f,
            Towers[SelectedTower].transform.localScale.y / 1.5f,
            Towers[SelectedTower].transform.localScale.z / 1.5f);
        Destroy(towerHere);

        SelectedTower++;
        SelectedTower %= 4;

        towerHere = Instantiate(Towers[SelectedTower]);
        towerHere.transform.parent = transform;
        towerHere.transform.localScale = Towers[SelectedTower].transform.localScale;

        Towers[SelectedTower].transform.localScale = new Vector3(
            Towers[SelectedTower].transform.localScale.x * 1.5f,
            Towers[SelectedTower].transform.localScale.y * 1.5f,
            Towers[SelectedTower].transform.localScale.z * 1.5f);

        
    }

    private void SpawnCreateTower()
    {
        if (!allHits.Any(hit => hit.transform.tag == "Base"))
        {
            //Don't create
            return;
        }


        var baseHit = allHits.First(hit => hit.transform.tag == "Base");

        var newTower = (GameObject)Instantiate(Towers[SelectedTower], baseHit.point, Quaternion.identity);
        newTower.transform.parent = transform;
        newTower.transform.localScale = Towers[SelectedTower].transform.localScale/1.5f;

        newTower.transform.parent = transform.parent;
    }

    void Update()
    {
        Towers[SelectedTower].transform.Rotate(0, 120 * Time.deltaTime, 0);
        var origin = transform.localPosition + (transform.forward * transform.localScale.z);
        allHits = Physics.RaycastAll(origin, -Vector3.up);

        if (allHits.Any(hit => hit.transform.tag == "Base"))
        {
            var baseHit = allHits.First(hit => hit.transform.tag == "Base");
            towerHere.transform.position = baseHit.point;
            towerHere.GetComponent<MeshRenderer>().enabled = true;
            Debug.DrawLine(origin, baseHit.point, Color.red, Time.deltaTime);
        }
        else
        {
            towerHere.GetComponent<MeshRenderer>().enabled = false;
        }

        //if (!GetComponent<MeshRenderer>().enabled)
        //{
        //    towerHere.GetComponent<MeshRenderer>().enabled = false;
        //}
    }

}
