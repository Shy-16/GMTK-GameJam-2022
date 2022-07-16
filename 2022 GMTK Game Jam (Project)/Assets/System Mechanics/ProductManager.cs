using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ProductManager : MonoBehaviour
{
    //--------------------------------------------------
    // Properties
    //--------------------------------------------------
    public static ProductManager _isntance;
    public List<Product> finishedProducts = new();

    //--------------------------------------------------
    // Initialization
    //--------------------------------------------------
    void Awake()
    {
        _isntance = this;
    }

    //--------------------------------------------------
    // Methods
    //--------------------------------------------------
}
