using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Linq;

public static class CustomSerializer {
    
    public static void SerializeObject(object obj, object info = null)
    {
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

#region Supported Types
            if (type == typeof(Bounds))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.BoundsField(memberInfo.Name, (Bounds)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(BoundsInt))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.BoundsIntField(memberInfo.Name, (BoundsInt)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(Color))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.ColorField(memberInfo.Name, (Color)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(AnimationCurve))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.CurveField(memberInfo.Name, (AnimationCurve)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(double))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.DoubleField(memberInfo.Name, (double)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(float))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.FloatField(memberInfo.Name, (float)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(int))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.IntField(memberInfo.Name, (int)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(long))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.LongField(memberInfo.Name, (long)value);
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
                value = EditorGUILayout.RectField(memberInfo.Name, (Rect)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(RectInt))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.RectIntField(memberInfo.Name, (RectInt)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(string))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.TextField(memberInfo.Name, (string)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(Vector2))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.Vector2Field(memberInfo.Name, (Vector2)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(Vector2Int))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.Vector2IntField(memberInfo.Name, (Vector2Int)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(Vector3))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.Vector3Field(memberInfo.Name, (Vector3)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(Vector3Int))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.Vector3IntField(memberInfo.Name, (Vector3Int)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(Vector4))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.Vector4Field(memberInfo.Name, (Vector4)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(bool))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.Toggle(memberInfo.Name, (bool)value);
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
                value = EditorGUILayout.EnumPopup(memberInfo.Name, (Enum)value);
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
#region Supported Types
                if (type == typeof(Bounds))
                {
                    liste[i] = EditorGUILayout.BoundsField("[" + i.ToString() + "]", (Bounds)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(BoundsInt))
                {
                    liste[i] = EditorGUILayout.BoundsIntField("[" + i.ToString() + "]", (BoundsInt)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(Color))
                {
                    liste[i] = EditorGUILayout.ColorField("[" + i.ToString() + "]", (Color)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(AnimationCurve))
                {
                    liste[i] = EditorGUILayout.CurveField("[" + i.ToString() + "]", (AnimationCurve)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(double))
                {
                    liste[i] = EditorGUILayout.DoubleField("[" + i.ToString() + "]", (double)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(float))
                {
                    liste[i] = EditorGUILayout.FloatField("[" + i.ToString() + "]", (float)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(int))
                {
                    liste[i] = EditorGUILayout.IntField("[" + i.ToString() + "]", (int)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(long))
                {
                    liste[i] = EditorGUILayout.LongField("[" + i.ToString() + "]", (long)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(UnityEngine.Object))
                {
                    liste[i] = EditorGUILayout.ObjectField((UnityEngine.Object)liste[i], type, true);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(Rect))
                {
                    liste[i] = EditorGUILayout.RectField("[" + i.ToString() + "]", (Rect)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(RectInt))
                {
                    liste[i] = EditorGUILayout.RectIntField("[" + i.ToString() + "]", (RectInt)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(string))
                {
                    liste[i] = EditorGUILayout.TextField("[" + i.ToString() + "]", (string)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(Vector2))
                {
                    liste[i] = EditorGUILayout.Vector2Field("[" + i.ToString() + "]", (Vector2)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(Vector2Int))
                {
                    liste[i] = EditorGUILayout.Vector2IntField("[" + i.ToString() + "]", (Vector2Int)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(Vector3))
                {
                    liste[i] = EditorGUILayout.Vector3Field("[" + i.ToString() + "]", (Vector3)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(Vector3Int))
                {
                    liste[i] = EditorGUILayout.Vector3IntField("[" + i.ToString() + "]", (Vector3Int)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(Vector4))
                {
                    liste[i] = EditorGUILayout.Vector4Field("[" + i.ToString() + "]", (Vector4)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(bool))
                {
                    liste[i] = EditorGUILayout.Toggle("[" + i.ToString() + "]", (bool)liste[i]);
                    SetValue(mi, obj, liste);
                }
                else if (type == typeof(GameObject))
                {
                    liste[i] = EditorGUILayout.ObjectField((GameObject)liste[i], type, true);
                    SetValue(mi, obj, liste);
                }
                else if (type.IsEnum)
                {
                    liste[i] = EditorGUILayout.EnumPopup("[" + i.ToString() + "]", (Enum)liste[i]);
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

    static void SerializeDictionary(MemberInfo mi, object baba, object dic)
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
                SerializeObject(dict[key], key);
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

