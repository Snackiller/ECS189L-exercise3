using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikminiSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pikminiPrefab;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            //Debug.Log("Fire2");
            Instantiate(this.pikminiPrefab, this.gameObject.transform.position, Quaternion.identity);
            FindObjectOfType<SoundManager>().PlaySoundEffect("NewMini");
        }
    }
}
