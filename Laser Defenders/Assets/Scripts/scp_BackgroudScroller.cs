using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scp_BackgroudScroller : MonoBehaviour
{
    [SerializeField] float ScrollSpeed = 0.5f;
    Material myMaterial;
    Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        myMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0f, ScrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
