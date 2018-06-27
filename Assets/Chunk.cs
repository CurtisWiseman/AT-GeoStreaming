using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private Transform player = null;
    [SerializeField] private float distance;

    [SerializeField] private int currentGeoDetailLevel;
    [SerializeField] private int newGeoDetailLevel;
    [SerializeField] private float[] detailDistances = null;

    [SerializeField] private int chunkID = 0;
    [SerializeField] private float chunkX = 0;
    [SerializeField] private float chunkY = 0;
    [SerializeField] private float textureWidthByChunks = 0;

    [SerializeField] private int currentTexDetailLevel;
    [SerializeField] private int newTexDetailLevel;

    void Start ()
    {
        currentGeoDetailLevel = detailDistances.Length - 1;
        currentTexDetailLevel = detailDistances.Length - 1;
        distance = Vector3.Distance(this.transform.position, player.position);
    }
	
	void Update ()
    {
        distance = Vector3.Distance(this.transform.position, player.position);

        newGeoDetailLevel = -1;
        newTexDetailLevel = -1;

        for (int i = 0; i < detailDistances.Length; i++)
        {
            if (distance < detailDistances[i])
            {
                newGeoDetailLevel = i;
                newTexDetailLevel = i;
                break;
            }
        }

        if(newGeoDetailLevel == -1)
        {
            newGeoDetailLevel = detailDistances.Length - 1;
        }

        if (newTexDetailLevel == -1)
        {
            newTexDetailLevel = detailDistances.Length - 1;
        }

        if (newGeoDetailLevel != currentGeoDetailLevel)
        {
            //Load new geometry detail level, unload current
            ChangeGeoDetailLevel();
        }

        if (newTexDetailLevel != currentTexDetailLevel)
        {
            //Load new texture detail level, unload current
            ChangeTexDetailLevel();
        }
    }

    void ChangeGeoDetailLevel()
    {
        ResourceRequest detailMeshFilter = Resources.LoadAsync<MeshFilter>("SNL" + newGeoDetailLevel + "I" + chunkID);
        WaitUntil.Equals(detailMeshFilter.isDone, true);
        MeshFilter detailMeshDone = (MeshFilter)detailMeshFilter.asset;
        Destroy(this.gameObject.GetComponent<MeshFilter>().mesh);
        this.gameObject.GetComponent<MeshFilter>().mesh = detailMeshDone.sharedMesh;

        //Update detail level variable
        currentGeoDetailLevel = newGeoDetailLevel;
    }

    void ChangeTexDetailLevel()
    {
        Material mat = this.gameObject.GetComponent<MeshRenderer>().material;

        //Calculate power of 2 for detail offsets
        float power = Pow(2.0f, newTexDetailLevel);

        //Calculate base offsets and scales
        float x_scale = 0.666666f / power;
        float y_scale = 1.0f / power;
        float x_offset = 0.666666f;
        float y_offset = 1.0f / power;

        if (newTexDetailLevel == 0)
        {
            x_offset = 0.0f;
            y_offset = 0.0f;
        }
        else
        {
            x_offset = 0.666666f;
            y_offset = 1.0f / power;
        }

        //Calculate chunk ID offsets and scales
        x_scale /= textureWidthByChunks;
        y_scale /= textureWidthByChunks;
        x_offset += ((chunkX / textureWidthByChunks) / power) * 0.666666f;
        y_offset += (chunkY / textureWidthByChunks) / power;

        //Apply UV deformations
        mat.SetTextureScale("_MainTex", new Vector2(x_scale, y_scale));
        mat.SetTextureOffset("_MainTex", new Vector2(x_offset, y_offset));

        //Update detail level variable
        currentTexDetailLevel = newTexDetailLevel;
    }

    static public float Pow(float number, int power)
    {
        float originalNumber = number;
        float output = 1.0f;
        for(int i = 0; i < power; i++)
        {
            output *= originalNumber;
        }
        return output;
    }
}