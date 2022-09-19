using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


namespace Krezme {
    public static class QualityOfLife {
        /// <summary>
        /// DeepClone allows the data of Objects to be duplicated without linking the objects
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T DeepClone<T>(this T obj) {
            using (MemoryStream stream = new MemoryStream()) {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                stream.Position = 0;
    
                return (T) formatter.Deserialize(stream);
            }
        }

        public static GameObject FindGameObjectInChildrenWithTag(Transform parent, string tag) {
            if (parent.childCount > 0) {
                for (int i = parent.childCount-1; i >= 0; i--) {
                    GameObject gO = FindGameObjectInChildrenWithTag(parent.GetChild(i), tag);
                    if (gO != null) {
                        return gO;
                    }
                }
                if (CompareTags(parent.tag, tag)) {
                    return parent.gameObject;
                }
            }
            else {
                if (CompareTags(parent.tag, tag)) {
                    return parent.gameObject;
                }
            }
            return null;
        }

        public static bool CompareTags(string firstTag, string secondTag) {
            return firstTag == secondTag ? true : false;
        }
    }
}
