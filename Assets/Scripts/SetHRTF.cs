using UnityEngine;
using System.Collections;
public class SetHRTF : MonoBehaviour
{
    public enum ROOMSIZE { Small, Medium, Large, None };
    public ROOMSIZE room = ROOMSIZE.Small;  // Small is regarded as the "most average"
                                            // defaults and docs from MSDN
                                            // https://msdn.microsoft.com/en-us/library/windows/desktop/mt186602(v=vs.85).aspx
    public float mingain = -96f; // The minimum gain limit applied at any distance, from -96 to + 12
    public float maxgain = 12f;  // The maximum gain applied at any distance, from -96 to + 12
    public float unityGainDistance = 1; // The distance at which the gain applied is 0dB, from 0.05 to infinity
    public float bypassCurves = 1; // if > 0, will bypass Unity's volume attenuation and make a more accurate volume simulation automatically in the plugin
    AudioSource audiosource;
    void Awake()
    {
        audiosource = this.gameObject.GetComponent<AudioSource>();
        if (audiosource == null)
        {
            print("SetHRTFParams needs an audio source to do anything.");
            return;
        }
        audiosource.spatialize = true; // we DO want spatialized audio
        audiosource.spread = 0; // we dont want to reduce our angle of hearing
        audiosource.spatialBlend = 1;   // we do want to hear spatialized audio
        audiosource.SetSpatializerFloat(1, (float)room);    // 1 is the roomsize param
        audiosource.SetSpatializerFloat(2, mingain); // 2 is the mingain param
        audiosource.SetSpatializerFloat(3, maxgain); // 3 is the maxgain param
        audiosource.SetSpatializerFloat(4, unityGainDistance); // 4 is the unitygain param
        audiosource.SetSpatializerFloat(5, bypassCurves);    // 5 is bypassCurves, which is usually a good idea
    }
}