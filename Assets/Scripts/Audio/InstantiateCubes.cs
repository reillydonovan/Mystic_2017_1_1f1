using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCubes : MonoBehaviour {
    public GameObject _sampleCubePrefab;
    GameObject[] _sampleCube = new GameObject[512];
    public float _maxScale;
	// Use this for initialization
	void Start () {
		
        for(int i = 0; i < _sampleCube.Length; i++)
        {
            GameObject _instanceSampleCube = (GameObject)Instantiate(_sampleCubePrefab);
            _instanceSampleCube.transform.position = this.transform.position;
            _instanceSampleCube.transform.parent = this.transform;
            _instanceSampleCube.name = "SampleCube" + i;
            this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0);
            _instanceSampleCube.transform.position = Vector3.forward * 1;
            _sampleCube[i] = _instanceSampleCube;

        }

	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < _sampleCube.Length; i++)
        {
            if(_sampleCube != null)
            {
                _sampleCube[i].transform.localScale = new Vector3(.01f, (AudioPeer._samples[i] * _maxScale), .01f);
            }
        }
	}
}
