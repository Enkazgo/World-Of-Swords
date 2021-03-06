﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{

    public Vector3 dir;

    // Update is called once per frame
    void Update()
    {
        if (dir.x == 0)
            dir.x = 360;
        transform.Rotate(dir * Time.deltaTime);
    }
}
