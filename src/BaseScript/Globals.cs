﻿global using DWORD = System.Int32;
global using DWORD64 = System.Int64;
global using WORD = System.UInt16;
global using HANDLE = System.IntPtr;
global using LPVOID = System.IntPtr;
global using HINSTANCE = System.IntPtr;
global using HMODULE = System.IntPtr;
global using static PInvoke.User32;
global using static SHVDN.Globals;
namespace SHVDN;

public static partial class Globals
{
    public const int DLL_PROCESS_ATTACH = 1;
    public const int DLL_PROCESS_DETACH = 0;
    public const int DLL_THREAD_ATTACH = 2;
    public const int DLL_THREAD_DETACH = 3;


    public const int VK_CONTROL = 0x11;
    public const int VK_SHIFT = 0x10;
}