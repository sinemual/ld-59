using System;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class SignalBlockPanels : MonoBehaviour
{
    [SerializeField] private SignalBlockPanel[] panels;

    public event Action<SignalBlockData> BuyBlock;
    
    public void InitItems(List<SignalBlockData> blockDatas, int money, int labPoints, bool isTutorialStep)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            int k = i;
            panels[k].BuyBlock -= BuyBlock;
        }

        for (int i = 0; i < panels.Length; i++)
        {
            int k = i;
            panels[k].UpdateInfoByData(blockDatas[k], money, labPoints, isTutorialStep);
            panels[k].BuyBlock += BuyBlock;
        }
    }
}