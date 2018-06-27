using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynaLoad : MonoBehaviour
{
    [SerializeField] private TerrainLevel topLevel;

    public TerrainLevel TopLevel
    {
        get
        {
            return topLevel;
        }

        set
        {
            topLevel = value;
        }
    }

    private TerrainLevel[] listOfTerrain = null;



    // Use this for initialization
    void Start ()
    {
        topLevel.LoadSelf();

        topLevel.AddSelfAndChildrenToList(listOfTerrain);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}