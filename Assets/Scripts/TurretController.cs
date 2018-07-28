using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TurretController : MonoBehaviour
{
    public GameObject pip;
    public Transform[] thisUnitSlots;
    public Material[] myMaterialsArray;

    private List<Material> myMaterials = new List<Material>();

    // Use this for initialization
    void Start ()
    {
        myMaterials = myMaterialsArray.ToList();
        int pipCount = 3;
        while (pipCount > 0)  // when the soldier spawns in he places pips on random edges of his base. this will change as we figure out a more permanent solution to pip spawning but as of now pip spawning is working as intended
        {
            Transform selectedSlot = thisUnitSlots[Random.Range(0, thisUnitSlots.Length - 1)];
            if (selectedSlot.childCount < 2)
            {
                GameObject spawnedPip = Instantiate(pip, new Vector3(selectedSlot.position.x, selectedSlot.position.y + 0.5f, selectedSlot.position.z), Quaternion.identity);
                spawnedPip.transform.parent = selectedSlot;
                Material randomMat = myMaterials[Random.Range(0, myMaterials.Count - 1)];
                spawnedPip.GetComponent<MeshRenderer>().material = randomMat;
                myMaterials.Remove(randomMat);
                pipCount--;
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
