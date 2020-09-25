using System;

namespace GameJamStarterKit
{
    /// <summary>
    /// Provides a method attribute which draws a button in the inspector to call the method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class KitButtonAttribute : Attribute
    {
        /// <summary>
        /// Text for the button
        /// </summary>
        public string Text;
        /// <summary>
        /// Size of the button
        /// </summary>
        public StandardSize ButtonSize;

        /// <summary>
        /// Adds a button to the inspector with a given size using the method name as the text.
        /// </summary>
        /// <param name="buttonSize">Size of the button</param>
        public KitButtonAttribute(StandardSize buttonSize = StandardSize.Small)
        {
            Text = string.Empty;
            ButtonSize = buttonSize;
        }

        /// <summary>
        /// Adds a button to the inspector with a given display text and size.
        /// </summary>
        /// <param name="text">text to display on the button</param>
        /// <param name="buttonSize">Size of the button</param>
        public KitButtonAttribute(string text, StandardSize buttonSize = StandardSize.Small)
        {
            Text = text;
            ButtonSize = buttonSize;
        }
    }

}