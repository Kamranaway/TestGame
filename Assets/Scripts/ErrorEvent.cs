using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using UnityEngine;
using Debug = UnityEngine.Debug;

/*
 * 
 * EXAMPLE
 ********************************************************
 * 
 * try
 * {
 *      Throw new System.Exception("EXAMPLE EXCEPTION");
 * }
 * catch (System.Exception exception)
 * {
 *      ErrorTrace.Trace(exception);
 * }
 * 
 ********************************************************
 */
public sealed class ErrorEvent
{
    public static void Trace(System.Exception exception) 
    {
        System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(exception, true);
        Debug.LogError("\"" + exception.Message + "\" " + trace.GetFrame(0).GetMethod().ReflectedType.FullName + "(" + trace.GetFrame(0).GetFileLineNumber() + ", " + trace.GetFrame(0).GetFileColumnNumber() + ")");
    }

    

    
   

}
