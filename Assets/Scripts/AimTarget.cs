using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.SearchService;
using UnityEngine;

public class AimTarget : MonoBehaviour, ITargetable
{
    private Material _currentMaterial;

    [SerializeField] private Color targetColor = Color.red;
    private Vector3 spawnPosition;
    private quaternion spawnRotation;

    private Color initialColor;
    // Start is called before the first frame update
    void Start()
    {
        _currentMaterial = GetComponent<Renderer>().material;
        initialColor = _currentMaterial.color;
        spawnPosition = gameObject.transform.position;
        spawnRotation = gameObject.transform.rotation;

        FindAnyObjectByType<RespawnSystem>().Respawn.AddListener(OnRespawn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Target()
    {
        _currentMaterial.color = Color.red;
    }

    public void StopTarget()
    {
        _currentMaterial.color = initialColor;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.transform.SetParent(null);
    }

    public void OnRespawn()//Moves the objects to where they first appear
    {
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        gameObject.transform.position = spawnPosition;
        gameObject.transform.rotation = spawnRotation;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

    }
}
