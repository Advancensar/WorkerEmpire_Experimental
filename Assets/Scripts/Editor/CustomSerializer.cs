using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Linq;

public static class CustomSerializer {

    static List<Type> ExcludeFromSerialization = new List<Type>();

    public static void SerializeObject(object obj, object info = null)
    {
        const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
        MemberInfo[] memberInfos = obj.GetType().GetFields(bindingFlags).Cast<MemberInfo>()
            .Concat(obj.GetType().GetProperties(bindingFlags)).ToArray();

        if (info != null)
            EditorGUILayout.LabelField(obj.ToString()+ "[" + info + "]", EditorStyles.boldLabel);
        else
            EditorGUILayout.LabelField(obj.ToString(), EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        //Debug.Log(obj.GetType());
        foreach (var memberInfo in memberInfos)
        {
            //Debug.Log(GetValue(member, obj));
            var type = GetType(memberInfo, obj);
            if (type == typeof(int))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.IntField(memberInfo.Name, (int)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(string))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.TextField(memberInfo.Name, (string)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(float))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.FloatField(memberInfo.Name, (float)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(Color))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.ColorField(memberInfo.Name, (Color)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type == typeof(Bounds))
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
            else if (type == typeof(AnimationCurve))
            {
                var value = GetValue(memberInfo, obj);
                value = EditorGUILayout.CurveField(memberInfo.Name, (AnimationCurve)value);
                SetValue(memberInfo, obj, value);
            }
            else if (type.IsGenericType)
            {
                if (IsList(GetValue(memberInfo,obj)))
                {
                    var list = GetValue(memberInfo, obj);// as IList;

                    SerializeList(memberInfo, obj, list);
                    //for (int i = 0; i < list.Count; i++)
                    //{
                    //    SetValue(memberInfo, GetValue(memberInfo, obj), SerializeList(list[i]), i);
                    //}
                    //Debug.Log(list.GetType().GetProperty("Item").GetValue(GetValue(memberInfo, obj)));
                    //Debug.Log("Item : " + list.GetType().GetProperty("Item"));
                    //for (int i = 0; i < list.Count; i++)
                    //{
                    //    Debug.Log("erenmon");
                    //    var value = 5;

                    //    SetValue(memberInfo, obj, value, i);
                    //}
                }
                else if (IsDictionary(type))
                {

                }
            }
            else
            {
                SerializeObject(GetValue(memberInfo, obj));                                
            }
        }
        EditorGUI.indentLevel--;
      
    }
    static bool foldout = true;
    static void SerializeList(MemberInfo mi, object baba, object list)
    {
        var liste = list as IList;
        //EditorGUILayout.LabelField(mi.Name, EditorStyles.boldLabel);
        //bool foldout = true;

        foldout = EditorGUILayout.Foldout(foldout, mi.Name);
        EditorGUI.indentLevel++;
        if (foldout)
        {
            for (int i = 0; i < liste.Count; i++)
            {
                EditorGUIUtility.labelWidth = 100;
                var type = liste[i].GetType();
                if (type == typeof(int))
                {
                    liste[i] = EditorGUILayout.IntField("[" + i.ToString() + "]", (int)liste[i]);                    
                    SetValue(mi, baba, liste);
                }
                else if (type == typeof(float))
                {
                    liste[i] = EditorGUILayout.FloatField("[" + i.ToString() + "]", (float)liste[i]);
                    SetValue(mi, baba, liste);
                }
                else if (type == typeof(string))
                {
                    liste[i] = EditorGUILayout.TextField("[" + i.ToString() + "]", (string)liste[i]);
                    SetValue(mi, baba, liste);
                }
                else if (type == typeof(Color))
                {
                    liste[i] = EditorGUILayout.ColorField("[" + i.ToString() + "]", (Color)liste[i]);
                    SetValue(mi, baba, liste);
                }
                //else if (ExcludeFromSerialization.OfType<type>().Any())
                //{

                //}
                else
                {
                    EditorGUIUtility.labelWidth = 0;
                    SerializeObject(liste[i], i);
                }
            }
        }
        
        EditorGUI.indentLevel--;
    }

    static void SerializeDictionary(MemberInfo mi, object baba, object dic)
    {
        var dictionary = dic as IDictionary;
        //EditorGUILayout.LabelField(mi.Name, EditorStyles.boldLabel);
        //bool foldout = true;

        foldout = EditorGUILayout.Foldout(foldout, mi.Name);
        EditorGUI.indentLevel++;
        if (foldout)
        {
            for (int i = 0; i < dictionary.Values.Count; i++)
            {
                EditorGUIUtility.labelWidth = 100;
                var type = dictionary[i].GetType();
                if (type == typeof(int))
                {
                    dictionary[i] = EditorGUILayout.IntField("[" + i.ToString() + "]", (int)dictionary[i]);
                    SetValue(mi, baba, dictionary);
                }
                else if (type == typeof(float))
                {
                    dictionary[i] = EditorGUILayout.FloatField("[" + i.ToString() + "]", (float)dictionary[i]);
                    SetValue(mi, baba, dictionary);
                }
                else if (type == typeof(Color))
                {
                    dictionary[i] = EditorGUILayout.ColorField("[" + i.ToString() + "]", (Color)dictionary[i]);
                    SetValue(mi, baba, dictionary);
                }
                else if (type == typeof(string))
                {
                    dictionary[i] = EditorGUILayout.TextField("[" + i.ToString() + "]", (string)dictionary[i]);
                    SetValue(mi, baba, dictionary);
                }
                //else if (ExcludeFromSerialization.OfType<type>().Any())
                //{

                //}
                else
                {
                    EditorGUIUtility.labelWidth = 0;
                    SerializeObject(dictionary[i], i);
                }
            }
        }

        EditorGUI.indentLevel--;
    }

    public static object GetValue(this MemberInfo memberInfo, object ofObject)
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
    public static void SetValue(this MemberInfo memberInfo, object ofObject, object value)
    {
        //if (index != null)
        //{
        //    switch (memberInfo.MemberType)
        //    {
        //        case MemberTypes.Field:
        //            ((FieldInfo)memberInfo).SetValue(forObject, value);
        //            break;
        //        case MemberTypes.Property:
        //            ((PropertyInfo)memberInfo).SetValue(forObject, value, index);
        //            break;
        //        default:
        //            throw new NotImplementedException();
        //    }
        //}
        //else
        //{
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
        //}
    }
    public static Type GetType(this MemberInfo memberInfo, object ofObject)
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

