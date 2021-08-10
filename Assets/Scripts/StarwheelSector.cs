using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarwheelSector
{
    Mesh mesh;
    int resolution;
    MachineSettings.RotativeSettings settings;

    public StarwheelSector(MachineSettings.RotativeSettings settings, Mesh mesh, int resolution)
    {
        this.mesh = mesh;
        this.resolution = resolution;
        this.settings = settings;
    }

    public void ConstructSectorMesh(int sectorNum)
    {
        Vector3[] vertices = new Vector3[resolution + 2];
        int[] triangles = new int[(resolution + 2) * 3];
        int triIndex = 0;
        float offsetDegree = (360 / settings.numberOfStations) * sectorNum;
        Vector3 containerCenter = new Vector3(Mathf.Sin((360 / settings.numberOfStations) * 0.5f * Mathf.Deg2Rad), 0, Mathf.Cos((360 / settings.numberOfStations) * 0.5f * Mathf.Deg2Rad)).normalized;

        vertices[0] = Vector3.zero;
        for (int i = 1; i < (resolution + 2); i++)
        {
            float percent = (i - 1) / resolution;
            float angle = (360 / settings.numberOfStations) * percent;
            float mask = percent * percent * settings.containerRadius - settings.containerRadius;
            Vector2 pointOnSector = new Vector2(Mathf.Sin((angle + offsetDegree) * Mathf.Deg2Rad) + mask , Mathf.Cos((angle + offsetDegree) * Mathf.Deg2Rad) + mask).normalized * settings.starwheelRadius ;
            
            
            vertices[i] = new Vector3(pointOnSector.x,0,pointOnSector.y) ;
            if (i < resolution + 1 )
            {
                triangles[triIndex] = 0;
                triangles[triIndex + 1] = i;
                triangles[triIndex + 2] = i + 1;
                triIndex += 3;

            }
        }
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        
    }

    public static Vector2 travelAlongCircle(this Vector2 pos, Vector2 center, float distance)
    {
        Vector3 axis = Vector3.back;
        Vector2 dir = pos - center;
        float circumference = 2.0f * Mathf.PI * dir.magnitude;
        float angle = distance / circumference * 360.0f;
        dir = Quaternion.AngleAxis(angle, axis) * dir;
        return dir + center;
    }

}
