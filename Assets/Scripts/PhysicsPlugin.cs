using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using Photon.Pun;

public class PhysicsPlugin : MonoBehaviourPun, IPunObservable
{
    const string dllName = "PhysicsPlugin";
    [DllImport(dllName)]
    private static extern IntPtr HelloWorld();
    [StructLayout(LayoutKind.Sequential)]
    public struct RigidBody
    {

        public float velX;
        public float velY;
        public float velZ;
        public float posX;
        public float posY;
        public float posZ;
    }
    [DllImport(dllName)]
    private static extern IntPtr GetRigidBody(float posX_, float posY_, float posZ_);
    [DllImport(dllName)]
    private static extern void SetVelocity(IntPtr rb_, float velX_, float velY_, float velZ_);
    [DllImport(dllName)]
    private static extern void UpdatePosition(IntPtr rb_, float deltaTime_);
    [DllImport(dllName)]
    private static extern void AddForce(IntPtr rb_, float forceX_, float forceY_, float forceZ_);

    RigidBody rb;
    IntPtr rbPtr;
    private void Awake()
    {
        rb = (RigidBody)Marshal.PtrToStructure(GetRigidBody(transform.position.x, transform.position.y, transform.position.z), typeof(RigidBody));

        rbPtr = Marshal.AllocHGlobal(Marshal.SizeOf(rb));
        Marshal.StructureToPtr(rb, rbPtr, true);
      

    }

    private void Update()
    {
        UpdatePosition(rbPtr, Time.deltaTime);
        rb = (RigidBody)Marshal.PtrToStructure(rbPtr, typeof(RigidBody));
        transform.position = new Vector3(rb.posX, rb.posY, rb.posZ);

    }
    public void UpdatePosition(Vector3 pos_)
    {
        rb.posX = pos_.x;
        rb.posY = pos_.y;
        rb.posZ = pos_.z;
        rb = (RigidBody)Marshal.PtrToStructure(rbPtr, typeof(RigidBody));
        transform.position = new Vector3(rb.posX, rb.posY, rb.posZ);


    }



    public void UpdateVelocity(Vector3 velocity_)
    {

        SetVelocity(rbPtr, velocity_.x, velocity_.y, velocity_.z);
        rb = (RigidBody)Marshal.PtrToStructure(rbPtr, typeof(RigidBody));
        
    }

    public Vector3 GetVelocity()
    {
        return new Vector3(rb.velX, rb.velY, rb.velZ);
    }

    public void AddForce(Vector3 force_)
    {
        AddForce(rbPtr, force_.x, force_.y, force_.z);
        rb = (RigidBody)Marshal.PtrToStructure(rbPtr, typeof(RigidBody));

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(rb.velX);
            stream.SendNext(rb.velY);
            stream.SendNext(rb.velZ);
        }
        else
        {
            float x = (float)stream.ReceiveNext();
            float y = (float)stream.ReceiveNext();
            float z = (float)stream.ReceiveNext();
            SetVelocity(rbPtr, x, y, z);
            rb = (RigidBody)Marshal.PtrToStructure(rbPtr, typeof(RigidBody));
        }

    }


}

