using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicManager : MonoBehaviour
{
    public static GraphicManager ins;
    public SettingData settingData;
    public List<Light> lightList = new List<Light>();
    private void Start()
    {
        ins = this;
    }
    public bool GetGraphicState()
    {
        return settingData.isHigh;
    }
}
