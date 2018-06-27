using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainLevel : MonoBehaviour
{
    [SerializeField] private TerrainLevel[] children;
    public TerrainLevel[] Children
    {
        get
        {
            return children;
        }

        set
        {
            children = value;
        }
    }


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void LoadSelf()
    {

    }

    public void UnloadSelf()
    {

    }

    public void LoadChild(int child)
    {
        children[child].LoadSelf();
    }

    public void UnloadChild(int child)
    {
        children[child].UnloadSelf();
    }

    public void LoadAllChildren()
    {
        foreach (TerrainLevel child in children)
        {
            child.LoadAllChildren();
        }

        LoadSelf();
    }

    public void UnloadAllChildren()
    {
        foreach (TerrainLevel child in children)
        {
            child.UnloadAllChildren();
        }

        UnloadSelf();
    }

    public void OneLevelDown()
    {
        LoadAllChildren();
        UnloadSelf();
    }

    public void AddSelfAndChildrenToList(TerrainLevel[] list)
    {
        list[list.GetLength(0)] = this;

    }
}
