using Godot;
using System;

public static class Utils
{
    // Generic method to find a component of type T in the children of the provided parent node
    public static T FindComponentInChildren<T>(Node parent) where T : class
    {
        // Check if the current node is of the generic type T
        if (parent is T)
        {
            return parent as T;
        }

        // Recursively search through all children
        foreach (var child in parent.GetChildren())
        {
            var found = FindComponentInChildren<T>(child);
            if (found != null)
            {
                return found;
            }
        }

        return null; // Return null if no component of type T is found
    }
}