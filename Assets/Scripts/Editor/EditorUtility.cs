using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using System.Linq;

public static class EditorUtility {

    public struct FieldData
    {
        public object obj;
        public object value;
        public FieldInfo info;
    }

    public struct PropertyData
    {
        public object obj;
        public object value;
        public PropertyInfo info;
    }

    public static void SerializeObject(object obj)
    {
        FieldData fieldData = new FieldData();
        foreach (var fi in obj.GetType().GetFields())
        {
            fieldData.obj = obj;
            fieldData.value = fi.GetValue(obj);
            fieldData.info = fi;
            SerializeField(fieldData);
        }

    }

    private static void SerializeField(FieldData fieldData)
    {
        #region Field
        if (fieldData.info != null)
        {
            if (fieldData.info.FieldType == typeof(int))
            {
                IntField(fieldData);
                //EditorUtility.IntField(obj, fi, EditorData.field);
                //int tempInt = (int)EditorData.currentField;
                //tempInt = EditorGUILayout.IntField(fi.Name, tempInt);
                //fi.SetValue(obj, tempInt);
            }
            else if (fieldData.info.FieldType == typeof(string))
            {
                TextField(fieldData);
                //EditorUtility.TextField(obj, fi, EditorData.fieldObject, GUILayout.Width(200));
                //string tempString = currentObj.ToString();
                //tempString = EditorGUILayout.TextField(fi.Name, tempString);
                //fi.SetValue(obj, EditorUtility.TextField(fi, currentObj));
            }
            else if (fieldData.info.FieldType.IsGenericType)
            {
                if (IsList(fieldData.value))
                {
                    //FieldData fd = new FieldData();
                    //if (list.Count > 0)
                    //{
                    //    for (int i = 0; i < list.Count; i++)
                    //    {
                    //        fd.value = list[0];
                    //        fd.obj = fieldData.value;
                    //        object[] index = { i };
                    //        //fd.propertyInfo = list.GetType().GetProperty("Item");
                    //        //Debug.Log("fd value : " + fd.value + "type :" + fd.propertyInfo.PropertyType);
                    //        //SerializeField(fd);
                    //    }
                    //    foreach (var type in fieldData.value as ICollection)
                    //    {
                    //        //FieldData fd = new FieldData();
                    //        //fd.value = type;
                    //        //fd.obj = fieldData.value;
                    //        //fd.fieldInfo = fieldData.fieldInfo;
                    //        //Debug.Log("fd value : " + fd.value + "type :" + fd.fieldInfo.FieldType);
                    //        //SerializeField(fd);

                    //        //Instead of going 1 layer deeper i should show the proper field thingy here;
                    //        //ShowEditableFields(type);
                    //    }
                    //}
                }

                else if (IsDictionary(fieldData.value))
                {
                    EditorGUILayout.TextField(fieldData.info.FieldType.GenericTypeArguments[0].ToString());
                    EditorGUILayout.TextField(fieldData.info.FieldType.GenericTypeArguments[1].ToString());
                }

            }
            else if (!fieldData.info.FieldType.IsGenericType)
            {
                SerializeObject(fieldData.value);
            }
        }
        #endregion
    }

    private static void SerializeProperty(PropertyData propertyData)
    {
        if (propertyData.info != null)
        {
            if (propertyData.info.PropertyType == typeof(int))
            {
                IntField(propertyData);
                //EditorUtility.IntField(obj, fi, EditorData.field);
                //int tempInt = (int)EditorData.currentField;
                //tempInt = EditorGUILayout.IntField(fi.Name, tempInt);
                //fi.SetValue(obj, tempInt);
            }
            else if (propertyData.info.PropertyType == typeof(string))
            {
                TextField(propertyData);
                //EditorUtility.TextField(obj, fi, EditorData.fieldObject, GUILayout.Width(200));
                //string tempString = currentObj.ToString();
                //tempString = EditorGUILayout.TextField(fi.Name, tempString);
                //fi.SetValue(obj, EditorUtility.TextField(fi, currentObj));
            }
            else if (propertyData.info.PropertyType.IsGenericType)
            {
                if (IsList(propertyData.value))
                {
                    var list = propertyData.value as IList;
                    Debug.Log(list.Count);
                    if (list.Count > 0)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {

                        }
                    }

                    //FieldData fd = new FieldData();
                    //if (list.Count > 0)
                    //{
                    //    for (int i = 0; i < list.Count; i++)
                    //    {
                    //        fd.value = list[0];
                    //        fd.obj = fieldData.value;
                    //        object[] index = { i };
                    //        //fd.propertyInfo = list.GetType().GetProperty("Item");
                    //        //Debug.Log("fd value : " + fd.value + "type :" + fd.propertyInfo.PropertyType);
                    //        //SerializeField(fd);
                    //    }
                    //    foreach (var type in fieldData.value as ICollection)
                    //    {
                    //        //FieldData fd = new FieldData();
                    //        //fd.value = type;
                    //        //fd.obj = fieldData.value;
                    //        //fd.fieldInfo = fieldData.fieldInfo;
                    //        //Debug.Log("fd value : " + fd.value + "type :" + fd.fieldInfo.FieldType);
                    //        //SerializeField(fd);

                    //        //Instead of going 1 layer deeper i should show the proper field thingy here;
                    //        //ShowEditableFields(type);
                    //    }
                    //}
                }

                else if (IsDictionary(propertyData.value))
                {
                    EditorGUILayout.TextField(propertyData.info.PropertyType.GenericTypeArguments[0].ToString());
                    EditorGUILayout.TextField(propertyData.info.PropertyType.GenericTypeArguments[1].ToString());
                }

            }
            else if (!propertyData.info.PropertyType.IsGenericType)
            {
                //SerializeProperty(propertyData.value);
            }
        }
    }

    public static void TextField(FieldData data, params GUILayoutOption[] options)
    {
        string tempValue = data.value.ToString();

        if (data.info != null)
        {
            tempValue = EditorGUILayout.TextField(data.info.Name, tempValue, options);
            //editorData.fieldInfo.SetValue(editorData.obj, tempValue);
            if (!data.info.IsInitOnly || !data.info.IsLiteral || !data.info.IsStatic)
            {
                SetValue(data.obj, tempValue, data.info);
            }
        }

    }

    public static void IntField(FieldData data, params GUILayoutOption[] options)
    {
        int tempValue = (int)data.value;
        if (data.info != null)
        {
            tempValue = EditorGUILayout.IntField(data.info.Name, tempValue, options);
            //editorData.fieldInfo.SetValue(editorData.obj, tempInt);
            if (!data.info.IsInitOnly || !data.info.IsLiteral || !data.info.IsStatic)
            {
                SetValue(data.obj, tempValue, data.info);
            }
        }
    }
    public static void TextField(PropertyData data, params GUILayoutOption[] options)
    {
        string tempValue = data.value.ToString();

        if (data.info != null)
        {
            tempValue = EditorGUILayout.TextField(data.info.Name, tempValue, options);
            SetValue(data.obj, tempValue, data.info);
        }

    }

    public static void IntField(PropertyData data, params GUILayoutOption[] options)
    {
        int tempValue = (int)data.value;
        if (data.info != null)
        {
            tempValue = EditorGUILayout.IntField(data.info.Name, tempValue, options);
            SetValue(data.obj, tempValue, data.info);
        }
    }

    static void SetValue(object obj, object value, FieldInfo fi = null)
    {
        fi?.SetValue(obj, value);
    }
    static void SetValue(object obj, object value, PropertyInfo pi = null)
    {
        pi?.SetValue(obj, value);
    }

    public static bool IsList(object obj)
    {
        if (obj == null) return false;
        return obj is IList &&
               obj.GetType().IsGenericType &&
               obj.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
    }

    public static bool IsDictionary(object obj)
    {
        if (obj == null) return false;
        return obj is IDictionary &&
               obj.GetType().IsGenericType &&
               obj.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>));
    }

}
