using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MachineSettings
{
    public enum MachineType{ Rotative, Lineal}
    public MachineType machineType;
    public int numberOfStations;
    public float containerRadius;
    public int outputStation;

    [System.Serializable]
    public class RotativeSettings : MachineSettings
    {
        public float starwheelRadius;
    }

    [System.Serializable]
    public class LinealSettings : MachineSettings
    {
        public float linealLength;
    }
    

}
