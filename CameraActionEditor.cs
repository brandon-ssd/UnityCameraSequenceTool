using UnityEngine;
using UnityEditor;

/*
    MAKE A FOLDER, NAME IT "Editor" EXACTLY, AND PUT THIS SCRIPT IN. THIS WILL CLEAN UP YOUR INSPECTOR TOOL FOR CAMERA TRIGGER.
*/

[CustomPropertyDrawer(typeof(CameraAction))]
public class CameraActionDrawer : PropertyDrawer
{
    private const float padding = 2f;
    private const float lineHeight = 18f;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        position.height = lineHeight;

        // Draw the ActionType field
        SerializedProperty actionType = property.FindPropertyRelative("actionType");
        EditorGUI.PropertyField(position, actionType);

        // Only show additional properties for the Wait and WaitWithInput ActionTypes
        if ((ActionType)actionType.enumValueIndex == ActionType.Wait)
        {
            // Draw the WaitDuration field
            position.y += position.height + padding;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("waitDuration"));

            // Check if this is the last property for the Wait action type
            if (property.propertyPath.EndsWith("waitDuration"))
            {
                position.y += lineHeight;
            }
        }
        else if ((ActionType)actionType.enumValueIndex == ActionType.InputToWait)
        {
            // Draw the InputKey field
            position.y += position.height + padding;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("inputKey"));

            // Check if this is the last property for the WaitWithInput action type
            if (property.propertyPath.EndsWith("inputKey"))
            {
                position.y += lineHeight;
            }
        }
        // Only show the Target field for the MoveToTarget ActionType
        else if ((ActionType)actionType.enumValueIndex == ActionType.MoveToTarget)
        {
            // Draw the Target field
            position.y += lineHeight + padding;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("target"));

            // Check if this is the last property for the MoveToTarget action type
            if (property.propertyPath.EndsWith("target"))
            {
                position.y += lineHeight;
            }
        }

        EditorGUI.EndProperty();
    }


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = lineHeight;

        SerializedProperty actionType = property.FindPropertyRelative("actionType");

        if ((ActionType)actionType.enumValueIndex == ActionType.Wait)
        {
            height += lineHeight + padding;
        }
        else if ((ActionType)actionType.enumValueIndex == ActionType.InputToWait)
        {
            height += lineHeight + padding;
        }
        else if ((ActionType)actionType.enumValueIndex == ActionType.MoveToTarget)
        {
            height += lineHeight + padding;
            height += lineHeight; // add an extra line height for the last property
        }

        height += lineHeight; // add an extra line height for every action type

        return height;
    }


}
