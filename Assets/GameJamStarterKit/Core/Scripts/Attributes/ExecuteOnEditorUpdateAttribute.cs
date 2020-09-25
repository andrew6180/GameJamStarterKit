using System;

namespace GameJamStarterKit
{
    /// <summary>
    /// Executes the attached method when the EditorApplication.update is called. (Every frame)
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ExecuteOnEditorUpdateAttribute : Attribute
    {
    }
}