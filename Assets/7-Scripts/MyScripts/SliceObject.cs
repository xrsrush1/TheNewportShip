using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using static UnityEngine.UI.Image;
using UnityEditor.Rendering;

public class SliceObject : MonoBehaviour
{
    public Transform startSlicePoint;
    public Transform endSlicePoint;

    public VelocityEstimator velocityEstimator; //component from another script,used to estimate the velocity of any game object

    public Material crossSectionMaterial;
    //this material will be used for the part which was cut, instead of pink

    public float cutForce = 500; //force value with which the sliced obj will fall

    public LayerMask sliceableLayer; //created a layer, which will be given to all obj which can be sliced

    private AudioSource CutAudio;

    // Start is called before the first frame update
    void Start()
    {
        CutAudio = GetComponent<AudioSource>();//to get the audio source on this component
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //check the collision between the sword and the target object for VR Slice

        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer); //checking the collision

        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;
            //here, we will take the obj which was hit 

            Slice(target); //now we call the slice function to cut the taregt obj we just hit with thw sword
        }
    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position , velocity);
        //here we calculated the normal vector which is a cross product of the vector of sword and its velocity, this normal vector will hit the target obj to cut it
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal); //function used to slice the game object given as the target

        if(hull != null)
        {
            //here we will generate the 2 sliced new meshes

            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);          //this will generate upper half mesh of the target sliced
            SetupSlicedComponent(upperHull);                                                    //we add this funct after cutting so the mesh falls down to create realism
            upperHull.layer = target.layer;                                                     //to make the cut meshes sliceable
            upperHull.transform.localPosition = target.transform.position;                      //to keep the cut meshes where the original mesh was there,

            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);          //this will generate lower half mesh of the target sliced
            SetupSlicedComponent(lowerHull);
            lowerHull.layer = target.layer;
            lowerHull.transform.localPosition = target.transform.position;

            //these are functions from ezyslice package we have imported.
            //parameters: target is the obj who's parts we have to create and cross sec mat is the material used for the cut part

            Destroy(target);
            //we will destroy the target after creation of both half meshes
            //as even after their generation, the original target obj is visible and it defeats the purpose.
            CutAudio.Play(); //to play the audio on collision
        }

    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        //in this function we will add some physics properties to the new half meshes
        //so that they dont just float in the scene

        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();                          //here, we are first adding rigidbody component to the sliced meshes
        MeshCollider meshCollider = slicedObject.AddComponent<MeshCollider>();          //here, we added mesh collider component to the sliced meshes
        meshCollider.convex = true; //as rigidbody only works is mesh collider is convex

        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 0.2f);
        //this this make the sliced obj fall down after being cut
        //parameters: force value for cutting, position of the force to be applied ie. here on the target so its position , explosion radius.

    }
}
