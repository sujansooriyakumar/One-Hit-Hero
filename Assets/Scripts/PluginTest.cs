using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class PluginTest : MonoBehaviour
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

    RigidBody rb;
    IntPtr rbPtr;
    private void Start()
    {
        rb = (RigidBody)Marshal.PtrToStructure(GetRigidBody(transform.position.x, transform.position.y, transform.position.z), typeof(RigidBody));

        rbPtr = Marshal.AllocHGlobal(Marshal.SizeOf(rb));
        Marshal.StructureToPtr(rb, rbPtr, true);
        SetVelocity(rbPtr, 1.0f, 0.0f, 0.0f);
        rb = (RigidBody)Marshal.PtrToStructure(rbPtr, typeof(RigidBody));
        Debug.Log(rb.posX);
    }

    private void Update()
    {
        UpdatePosition(rbPtr, Time.deltaTime);
        rb = (RigidBody)Marshal.PtrToStructure(rbPtr, typeof(RigidBody));
        transform.position = new Vector3(rb.posX, rb.posY, rb.posZ);
    }
}
