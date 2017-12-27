using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Sharing;
using HoloToolkit.Sharing.SyncModel;


public class MyStringSynchronizer : MonoBehaviour
{/*
    public string TheStringYoureActuallyTryingToSend
    {
        get;
        set
        {
            stringDataModel.MyString.Value = value;
        }
    }
    

    private MySyncDataModel stringDataModel;
    public MySyncDataModel StringDataModel
    {
        private get { return stringDataModel; }
        set
        {
            if (stringDataModel != value)
            {
                if (stringDataModel != null)
                {
                    stringDataModel.MyStringChagned -= OnStringChagned;
                }

                stringDataModel = value;

                if (stringDataModel != null)
                {
                    TheStringYoureActuallyTryingToSend = stringDataModel.MyString.Value;

                    stringDataModel.MyStringChagned += OnStringChagned;
                }
            }
        }
    }

    private void OnStringChanged()
    {
        TheStringYoureActuallyTryingToSend = stringDataModel.MyString.Value;
    }
    */
}
