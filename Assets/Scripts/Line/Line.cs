using System;
using System.Collections;
using System.Collections.Generic;
using CarRelated;
using UnityEngine;

public class Line : MonoBehaviour
{
    private List<Car> _cars;

    private void Awake()
    {
        _cars = new List<Car>();
    }
}
