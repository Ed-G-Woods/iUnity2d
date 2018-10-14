using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionManager : MonoBehaviour {
    public  enum ResolutionEnum
    {
        small,
        mid,
        large
    };
    

    public void setResEnum(ResolutionEnum re)
    {
        switch (re)
        {
            case ResolutionEnum.small:
                Screen.SetResolution(800, 600, false);
                break;
            case ResolutionEnum.mid:
                Screen.SetResolution(1440, 900, false);
                break;
            case ResolutionEnum.large:
                Screen.SetResolution(1600, 900, false);
                break;
            default:
                Screen.SetResolution(1440, 900, false);
                break;

        }
    }


    public void setResx(int x)
    {
        Screen.SetResolution(x, x, false);
    }

    public void setRes(int x ,int y,bool fs)
    {
        Screen.SetResolution(x, y,fs);
    }
}
