﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SHVDN;

/// <summary>
/// Some imports of required WINAPI
/// </summary>
public static unsafe class PInvoke
{
    public static readonly HMODULE User32 = NativeLibrary.Load("User32.dll");
    public static readonly HMODULE Kernel32 = NativeLibrary.Load("Kernel32.dll");
    public static delegate* unmanaged<ulong> GetTickCount64 = (delegate* unmanaged<ulong>)NativeLibrary.GetExport(Kernel32, "GetTickCount64");
    public static delegate* unmanaged<DWORD> GetCurrentThreadId = (delegate* unmanaged<DWORD>)NativeLibrary.GetExport(Kernel32, "GetCurrentThreadId");
    public static delegate* unmanaged<LPVOID, void> SwitchToFiber = (delegate* unmanaged<LPVOID, void>)NativeLibrary.GetExport(Kernel32, "SwitchToFiber");
    public static delegate* unmanaged<nuint, IntPtr, LPVOID, LPVOID> CreateFiber = (delegate* unmanaged<nuint, IntPtr, LPVOID, LPVOID>)NativeLibrary.GetExport(Kernel32, "CreateFiber");
    public static delegate* unmanaged<LPVOID, void> DeleteFiber = (delegate* unmanaged<LPVOID, void>)NativeLibrary.GetExport(Kernel32, "DeleteFiber");
    public static delegate* unmanaged<HANDLE, string, string, uint, int> MessageBoxA = (delegate* unmanaged<HANDLE, string, string, uint, int>)NativeLibrary.GetExport(User32, "MessageBoxA");
    public static delegate* unmanaged<int, short> GetAsyncKeyState = (delegate* unmanaged<int, short>)NativeLibrary.GetExport(User32, "GetAsyncKeyState");
    public static string GetClipboardText()
    {
        if (!OpenClipboard(default)) throw new Win32Exception();
        var pChar = (char*)GetClipboardData(CF_UNICODETEXT);
        var text = new string(pChar);
        CloseClipboard();
        return text;
    }


    public static delegate* unmanaged<uint, IntPtr> GetClipboardData = (delegate* unmanaged<uint, IntPtr>)NativeLibrary.GetExport(User32, "GetClipboardData");

    public static delegate* unmanaged<IntPtr, bool> OpenClipboard = (delegate* unmanaged<IntPtr, bool>)NativeLibrary.GetExport(User32, "OpenClipboard");

    public static delegate* unmanaged<bool> CloseClipboard = (delegate* unmanaged<bool>)NativeLibrary.GetExport(User32, "CloseClipboard");

    [DllImport("kernel32.dll", SetLastError = true)]
    [PreserveSig]
    public static extern uint GetModuleFileNameW(HMODULE hModule, char* buf, uint nSize);

}
