using System;

namespace GameJamStarterKit.Attributes
{
    /// <summary>
    /// Executes the marked method when the editor's hierarchy changes
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ExecuteOnEditorHierarchyChangeAttribute : Attribute
    {
    }
}