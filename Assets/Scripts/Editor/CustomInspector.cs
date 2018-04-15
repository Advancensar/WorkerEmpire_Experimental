using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Linq;

public static class CustomInspector
{    
    public static void SerializeObject(object obj, object info = null)
    {
        if (obj == null)
            return;

        const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;

        MemberInfo[] memberInfos = obj.GetType().GetFields(bindingFlags).Cast<MemberInfo>()
            .Concat(obj.GetType().GetProperties(bindingFlags)).ToArray();

        if (info != null)
            EditorGUILayout.LabelField(obj.ToString() + " [" + info + "]", EditorStyles.boldLabel);
        else
            EditorGUILayout.LabelField(obj.ToString(), EditorStyles.boldLabel);

        EditorGUI.indentLevel++;
        //Debug.Log(obj.GetType());
        foreach (var memberInfo in memberInfos)
        {
            //Debug.Log(GetValue(member, obj));

            var type = GetType(memberInfo, obj);
            string label = memberInfo.Name;

#region Supported Types
            if (type == typeof(Bounds))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.BoundsField(label, (Bounds)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(BoundsInt))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.BoundsIntField(label, (BoundsInt)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(Color))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.ColorField(label, (Color)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(AnimationCurve))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.CurveField(label, (AnimationCurve)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(double))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.DoubleField(label, (double)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(float))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.FloatField(label, (float)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(int))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.IntField(label, (int)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(long))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.LongField(label, (long)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(UnityEngine.Object))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.ObjectField((UnityEngine.Object)value, type, true);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(Rect))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.RectField(label, (Rect)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(RectInt))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.RectIntField(label, (RectInt)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(string))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.TextField(label, (string)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(Vector2))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.Vector2Field(label, (Vector2)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(Vector2Int))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.Vector2IntField(label, (Vector2Int)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(Vector3))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.Vector3Field(label, (Vector3)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(Vector3Int))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.Vector3IntField(label, (Vector3Int)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(Vector4))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.Vector4Field(label, (Vector4)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(bool))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.Toggle(label, (bool)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(GameObject))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.ObjectField((GameObject)value, type, true);
                SetValue(memberInfo, obj, value);
            }
#endregion
            else if (type.IsGenericType)
            {
                if (IsList(GetValue(memberInfo, obj)))
                {
                    var list = GetValue(memberInfo, obj);
                    SerializeList(memberInfo, obj, list);
                }
                else if (IsDictionary(GetValue(memberInfo, obj)))
                {
                    var dictionary = GetValue(memberInfo, obj);
                    SerializeDictionary(memberInfo, obj, dictionary);
                }
            }
            else if (type.IsEnum)
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.EnumPopup(label, (Enum)value);
                SetValue(memberInfo, obj, value);
            }
            else
            {
                SerializeObject(GetValue(memberInfo, obj));
            }
        }
        EditorGUI.indentLevel--;

    }

    static Dictionary<int, bool> foldout = new Dictionary<int, bool>();

    static void SerializeList(MemberInfo mi, object obj, object list)
    {
        var liste = list as IList;
        int hashCode = list.GetHashCode();
        if (!foldout.ContainsKey(hashCode))
            foldout.Add(hashCode, false);

        foldout[hashCode] = EditorGUILayout.Foldout(foldout[hashCode], mi.Name, true);
        EditorGUI.indentLevel++;
        if (foldout[hashCode])
        {
            for (int i = 0; i < liste.Count; i++)
            {                
                var type = liste[i].GetType();
                string label = "[" + i.ToString() + "]";
#region Supported Types
                if (type == typeof(Bounds))
                {
                    liste[i] = EditorGUILayout.BoundsField(label, (Bounds)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(BoundsInt))
                {
                    liste[i] = EditorGUILayout.BoundsIntField(label, (BoundsInt)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(Color))
                {
                    liste[i] = EditorGUILayout.ColorField(label, (Color)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(AnimationCurve))
                {
                    liste[i] = EditorGUILayout.CurveField(label, (AnimationCurve)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(double))
                {
                    liste[i] = EditorGUILayout.DoubleField(label, (double)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(float))
                {
                    liste[i] = EditorGUILayout.FloatField(label, (float)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(int))
                {
                    liste[i] = EditorGUILayout.IntField(label, (int)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(long))
                {
                    liste[i] = EditorGUILayout.LongField(label, (long)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(UnityEngine.Object))
                {
                    liste[i] = EditorGUILayout.ObjectField((UnityEngine.Object)liste[i], type, true);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(Rect))
                {
                    liste[i] = EditorGUILayout.RectField(label, (Rect)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(RectInt))
                {
                    liste[i] = EditorGUILayout.RectIntField(label, (RectInt)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(string))
                {
                    liste[i] = EditorGUILayout.TextField(label, (string)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(Vector2))
                {
                    liste[i] = EditorGUILayout.Vector2Field(label, (Vector2)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(Vector2Int))
                {
                    liste[i] = EditorGUILayout.Vector2IntField(label, (Vector2Int)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(Vector3))
                {
                    liste[i] = EditorGUILayout.Vector3Field(label, (Vector3)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(Vector3Int))
                {
                    liste[i] = EditorGUILayout.Vector3IntField(label, (Vector3Int)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(Vector4))
                {
                    liste[i] = EditorGUILayout.Vector4Field(label, (Vector4)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(bool))
                {
                    liste[i] = EditorGUILayout.Toggle(label, (bool)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(GameObject))
                {
                    liste[i] = EditorGUILayout.ObjectField((GameObject)liste[i], type, true);
                    SetValue(mi, obj, liste);
                }
                else if (type.IsEnum)
                {
                    liste[i] = EditorGUILayout.EnumPopup(label, (Enum)liste[i]);
                    SetValue(mi, obj, liste);
                }
                #endregion
                else
                {
                    SerializeObject(liste[i], i);
                }
            }
        }
        EditorGUI.indentLevel--;
    }

    static void SerializeDictionary(MemberInfo mi, object obj, object dic)
    {
        var dict = dic as IDictionary;
        int hashCode = dic.GetHashCode();
        if (!foldout.ContainsKey(hashCode))
            foldout.Add(hashCode, false);

        foldout[hashCode] = EditorGUILayout.Foldout(foldout[hashCode], mi.Name, true);
        EditorGUI.indentLevel++;
        if (foldout[hashCode])
        {
            foreach (var key in dict.Keys)
            {
                var type = dict[key].GetType();
                string label = "[" + key + "]";
                #region Supported Types

                if (type == typeof(Bounds))
                {
                    dict[key] = EditorGUILayout.BoundsField(label, (Bounds)dict[key]);
                    SetValue(mi, obj, dict);
                }
                else if (type == typeof(BoundsInt))
                {
                    dict[key] = EditorGUILayout.BoundsIntField(label, (BoundsInt)dict[key]);
                    SetValue(mi, obj, dict);
                }
                else if (type == typeof(Color))
                {
                    dict[key] = EditorGUILayout.ColorField(label, (Color)dict[key]);
                    SetValue(mi, obj, dict);
                }
                else if (type == typeof(AnimationCurve))
                {
                    dict[key] = EditorGUILayout.CurveField(label, (AnimationCurve)dict[key]);
                    SetValue(mi, obj, dict);
                }
                else if (type == typeof(double))
                {
                    dict[key] = EditorGUILayout.DoubleField(label, (double)dict[key]);
                    SetValue(mi, obj, dict);
                }
                else if (type == typeof(float))
                {
                    dict[key] = EditorGUILayout.FloatField(label, (float)dict[key]);
                    SetValue(mi, obj, dict);
                }
                else if (type == typeof(int))
                {
                    dict[key] = EditorGUILayout.IntField(label, (int)dict[key]);
                    SetValue(mi, obj, dict);
                }
                else if (type == typeof(long))
                {
                    dict[key] = EditorGUILayout.LongField(label, (long)dict[key]);
                    SetValue(mi, obj, dict);
                }
                else if (type == typeof(UnityEngine.Object))
                {
                    dict[key] = EditorGUILayout.ObjectField((UnityEngine.Object)dict[key], type, true);
                    SetValue(mi, obj, dict);
                }
                else if (type == typeof(Rect))
                {
                    dict[key] = EditorGUILayout.RectField(label, (Rect)dict[key]);
                    SetValue(mi, obj, dict);
                }
                else if (type == typeof(RectInt))
                {
                    dict[key] = EditorGUILayout.RectIntField(label, (RectInt)dict[key]);
                    SetValue(mi, obj, dict);
                }
                else if (type == typeof(string))
                {
                    dict[key] = EditorGUILayout.TextField(label, (string)dict[key]);
                    SetValue(mi, obj, dict);
                }
                else if (type == typeof(Vector2))
                {
                    dict[key] = EditorGUILayout.Vector2Field(label, (Vector2)dict[key]);
                    SetValue(mi, obj, dict);
                }
                else if (type == typeof(Vector2Int))
                {
                    dict[key] = EditorGUILayout.Vector2IntField(label, (Vector2Int)dict[key]);
                    SetValue(mi, obj, dict);
                }
                else if (type == typeof(Vector3))
                {
                    dict[key] = EditorGUILayout.Vector3Field(label, (Vector3)dict[key]);
                    SetValue(mi, obj, dict);
                }
                else if (type == typeof(Vector3Int))
                {
                    dict[key] = EditorGUILayout.Vector3IntField(label, (Vector3Int)dict[key]);
                    SetValue(mi, obj, dict);
                }
                else if (type == typeof(Vector4))
                {
                    dict[key] = EditorGUILayout.Vector4Field(label, (Vector4)dict[key]);
                    SetValue(mi, obj, dict);
                }
                else if (type == typeof(bool))
                {
                    dict[key] = EditorGUILayout.Toggle(label, (bool)dict[key]);
                    SetValue(mi, obj, dict);
                }
                else if (type == typeof(GameObject))
                {
                    dict[key] = EditorGUILayout.ObjectField((GameObject)dict[key], type, true);
                    SetValue(mi, obj, dict);
                }
                else if (type.IsEnum)
                {
                    dict[key] = EditorGUILayout.EnumPopup(label, (Enum)dict[key]);
                    SetValue(mi, obj, dict);
                }
                #endregion
                else
                {
                    SerializeObject(dict[key], key);
                }

            }
        }        
        EditorGUI.indentLevel--;
    }

    public static object GetValue(MemberInfo memberInfo, object ofObject)
    {
        switch (memberInfo.MemberType)
        {
            case MemberTypes.Field:
                return ((FieldInfo)memberInfo).GetValue(ofObject);
            case MemberTypes.Property:
                return ((PropertyInfo)memberInfo).GetValue(ofObject);
            default:
                throw new NotImplementedException();
        }
    }
    public static void SetValue(MemberInfo memberInfo, object ofObject, object value)
    {
        switch (memberInfo.MemberType)
        {
            case MemberTypes.Field:
                ((FieldInfo)memberInfo).SetValue(ofObject, value);
                break;
            case MemberTypes.Property:
                ((PropertyInfo)memberInfo).SetValue(ofObject, value);
                break;
            default:
                throw new NotImplementedException();
        }
    }
    public static Type GetType(MemberInfo memberInfo, object ofObject)
    {
        switch (memberInfo.MemberType)
        {
            case MemberTypes.Field:
                return ((FieldInfo)memberInfo).FieldType;
            case MemberTypes.Property:
                return ((PropertyInfo)memberInfo).PropertyType;
            default:
                throw new NotImplementedException();
        }
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

