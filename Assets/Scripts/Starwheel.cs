using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starwheel : MonoBehaviour
{
    [Range(2,50)]
    public int resolution = 10;
    MeshFilter[] meshFilters;
    StarwheelSector[] starwheelSectors;
    public MachineSettings.RotativeSettings settings;

    public Starwheel(MachineSettings.RotativeSettings settings)
    {
        this.settings = settings;
    }

    public void OnValidate()
    {
        Initialize();
        GenerateMesh();
    }
    void Initialize()
    {

        if (meshFilters == null || meshFilters.Length == 0 || meshFilters.Length < settings.numberOfStations)
        { 
            meshFilters = new MeshFilter[settings.numberOfStations];
        }
        starwheelSectors = new StarwheelSector[settings.numberOfStations];

        for (int i =0; i< settings.numberOfStations; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh" + i);
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }

            starwheelSectors[i] = new StarwheelSector(settings, meshFilters[i].sharedMesh, resolution);
        }
    }

    void GenerateMesh()
    {
        for(int i = 0 ; i< starwheelSectors.Length; i++)
        {
            starwheelSectors[i].ConstructSectorMesh(i);
        }

    }
}
