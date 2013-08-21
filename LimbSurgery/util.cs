using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GnomoriaLimbSurgery
{

    public static class EnumerableExtensions
    {
        public static string Join(this IEnumerable<string> source, string seperator)
        {
            return String.Join(seperator, source);
        }

        public static T ElementAfterOrDefault<T>(this IEnumerable<T> source, T elementBeforeSearchResult)
        {
            var comp = EqualityComparer<T>.Default;
            bool foundIt = false;
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            foreach (var el in source)
            {
                if (foundIt)
                {
                    return el;
                }
                else if (comp.Equals(el, elementBeforeSearchResult))
                {
                    foundIt = true;
                }
            }
            return default(T);
        }
        public static T ElementBeforeOrDefault<T>(this IEnumerable<T> source, T elementAfterSearchResult)
        {
            var comp = EqualityComparer<T>.Default;
            T last = default(T);
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            foreach (var el in source)
            {
                if (comp.Equals(el, elementAfterSearchResult))
                {
                    return last;
                }
                else
                {
                    last = el;
                }
            }
            return default(T);
        }
        public static T ElementAfterOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            bool foundIt = false;
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            foreach (var el in source)
            {
                if (foundIt)
                {
                    return el;
                }
                else if (predicate(el))
                {
                    foundIt = true;
                }
            }
            return default(T);
        }
        public static T ElementBeforeOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            T last = default(T);
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            foreach (var el in source)
            {
                if (predicate(el))
                {
                    return last;
                }
                else
                {
                    last = el;
                }
            }
            return default(T);
        }
        public static T ElementAfter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            bool foundIt = false;
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            foreach (var el in source)
            {
                if (foundIt)
                {
                    return el;
                }
                else if (predicate(el))
                {
                    foundIt = true;
                }
            }
            if (foundIt)
            {
                throw new InvalidOperationException("Found element is the last item of the collection");
            }
            else
            {
                throw new InvalidOperationException("No elements in collection match the condition");
            }
        }
        public static T ElementAfter<T>(this IEnumerable<T> source, T elementBeforeSearchResult)
        {
            var comp = EqualityComparer<T>.Default;
            bool foundIt = false;
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            foreach (var el in source)
            {
                if (foundIt)
                {
                    return el;
                }
                else if (comp.Equals(el, elementBeforeSearchResult))
                {
                    foundIt = true;
                }
            }
            if (foundIt)
            {
                throw new InvalidOperationException("Found element is the last item of the collection");
            }
            else
            {
                throw new InvalidOperationException("No elements in collection match the condition");
            }
        }
        public static int IndexOf<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException("items");
            if (predicate == null) throw new ArgumentNullException("predicate");

            int retVal = 0;
            foreach (var item in source)
            {
                if (predicate(item)) return retVal;
                retVal++;
            }
            return -1;
        }
        public static int IndexOf<T>(this IEnumerable<T> source, T element, IEqualityComparer<T> comparer)
        {
            return IndexOf(source, el => comparer.Equals(el, element));
        }
        public static int IndexOf<T>(this IEnumerable<T> source, T element)
        {
            return IndexOf(source, element, EqualityComparer<T>.Default);
        }
        public static IEnumerable<T> Union<T>(this IEnumerable<T> source, T element)
        {
            return source.Union(new T[] { element });
        }
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> source, T element)
        {
            return source.Concat(new T[] { element });
        }
        public static bool SequenceEqual<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, bool> equals_comparer)
        {
            if (equals_comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            if (first == null)
            {
                throw new ArgumentNullException("first");
            }
            if (second == null)
            {
                throw new ArgumentNullException("second");
            }
            using (var enumerator = first.GetEnumerator())
            {
                using (var enumerator2 = second.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        if (!enumerator2.MoveNext() || !equals_comparer(enumerator.Current, enumerator2.Current))
                        {
                            bool result = false;
                            return result;
                        }
                    }
                    if (enumerator2.MoveNext())
                    {
                        bool result = false;
                        return result;
                    }
                }
            }
            return true;
        }
    }


    public static class EventHandlerExtensions
    {
        public static void TryRaise<T, T2, T3>(this EventHandler<EventArgs<T, T2, T3>> handler, object self, T arg1, T2 arg2, T3 arg3)
        {
            if (handler != null)
            {
                handler.Invoke(self, new EventArgs<T, T2, T3>(arg1, arg2, arg3));
            }
        }
        public static void TryRaise<T, T2>(this EventHandler<EventArgs<T, T2>> handler, object self, T arg1, T2 arg2)
        {
            if (handler != null)
            {
                handler.Invoke(self, new EventArgs<T, T2>(arg1, arg2));
            }
        }
        public static void TryRaise<T>(this EventHandler<EventArgs<T>> handler, object self, T args)
        {
            if (handler != null)
            {
                handler.Invoke(self, new EventArgs<T>(args));
            }
        }
        public static void TryRaise(this EventHandler handler, object self)
        {
            if (handler != null)
            {
                handler.Invoke(self, new EventArgs());
            }
        }
        public static void TryRaise<T>(this EventHandler<T> handler, object self, T args) where T : EventArgs
        {
            if (handler != null)
            {
                handler.Invoke(self, args);
            }
        }
    }
    public class EventArgs<T> : EventArgs
    {
        public T Argument;
        public EventArgs(T arg)
            : base()
        {
            Argument = arg;
        }
    }
    public class EventArgs<T, T2> : EventArgs<T>
    {
        public T2 Argument2;
        public EventArgs(T arg, T2 arg2)
            : base(arg)
        {
            Argument2 = arg2;
        }
    }
    public class EventArgs<T, T2, T3> : EventArgs<T, T2>
    {
        public T3 Argument3;
        public EventArgs(T arg, T2 arg2, T3 arg3)
            : base(arg, arg2)
        {
            Argument3 = arg3;
        }
    }




    namespace Serialization
    {
        public static class JSON
        {
            /*
            private static Dictionary<Type, System.Runtime.Serialization.Json.DataContractJsonSerializer> Serializers = new Dictionary<Type, System.Runtime.Serialization.Json.DataContractJsonSerializer>();
            private static System.Runtime.Serialization.Json.DataContractJsonSerializer GetSerializer<T>(params Type knownTypes)
            {
                var type = typeof(T);
                System.Runtime.Serialization.Json.DataContractJsonSerializer seri;
                if (Serializers.TryGetValue(type, out seri))
                {
                    return seri;
                }
                return Serializers[type] = new System.Runtime.Serialization.Json.DataContractJsonSerializer(type);
            }
            */

            public static void ToJSON<T>(T object_to_serialize, System.IO.Stream target_stream, params Type[] knownTypes)
            {
                var seri = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T), knownTypes);
                seri.WriteObject(target_stream, object_to_serialize);
            }
            public static string ToJSON<T>(T object_to_serialize, params Type[] knownTypes)
            {
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    ToJSON(object_to_serialize, stream, knownTypes);
                    return Encoding.Default.GetString(stream.ToArray());
                }
            }
            public static T FromJSON<T>(System.IO.Stream source_stream, params Type[] knownTypes)
            {
                var seri = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T), knownTypes);
                return (T)seri.ReadObject(source_stream);
            }
            public static T FromJSON<T>(string json_text, params Type[] knownTypes)
            {
                return FromJSON<T>(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(json_text)), knownTypes);
            }
        }
        [Serializable]
        public class SerializableDataBag<T> : System.Runtime.Serialization.ISerializable, IEnumerable<KeyValuePair<String, T>>
        {
            private Dictionary<string, T> data = new Dictionary<String, T>();

            public SerializableDataBag(IEnumerable<KeyValuePair<String, T>> initial_data)
            {
                if (initial_data == null)
                    throw new ArgumentNullException("initial_data");
                foreach (var el in initial_data)
                {
                    if (el.Key == null)
                    {
                        throw new ArgumentException("Cannot have an empty key!");
                    }
                    data.Add(el.Key, el.Value);
                }
            }
            public SerializableDataBag(IEnumerable<Tuple<String, T>> initial_data)
            {
                if (initial_data == null)
                    throw new ArgumentNullException("initial_data");
                foreach (var el in initial_data)
                {
                    if (el.Item1 == null)
                    {
                        throw new ArgumentException("Cannot have an empty key!");
                    }
                    data.Add(el.Item1, el.Item2);
                }
            }
            public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                foreach (KeyValuePair<string, T> kvp in data)
                {
                    info.AddValue(kvp.Key, kvp.Value);
                }
            }
            public IEnumerator<KeyValuePair<string, T>> GetEnumerator()
            {
                return data.GetEnumerator();
            }
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
            protected SerializableDataBag(SerializationInfo info, StreamingContext context)
            {
                // TODO: validate inputs before deserializing. See http://msdn.microsoft.com/en-us/library/ty01x675(VS.80).aspx
                foreach (SerializationEntry entry in info)
                {
                    data.Add(entry.Name, (T)entry.Value);
                }
            }
            public string ToJSON()
            {
                return JSON.ToJSON(this, typeof(T));
            }
            public static SerializableDataBag<T> FromJSON(String data)
            {
                return JSON.FromJSON<SerializableDataBag<T>>(data, typeof(T));
            }
            public Dictionary<string, T> ToDictionary()
            {
                return new Dictionary<string, T>(data);
            }
            public Dictionary<string, TOut> ToDictionary<TOut>(Func<T, TOut> converter)
            {
                var dict = new Dictionary<string, TOut>();
                foreach (var el in data)
                {
                    dict.Add(el.Key, converter(el.Value));
                }
                return dict;
            }
            public Dictionary<string, TOut> ToDictionary<TOut>(Func<string, T, TOut> converter)
            {
                var dict = new Dictionary<string, TOut>();
                foreach (var el in data)
                {
                    dict.Add(el.Key, converter(el.Key, el.Value));
                }
                return dict;
            }
        }
        public static class SerializableDataBag
        {
            public static String ToJSON<T>(IEnumerable<Tuple<String, T>> data)
            {
                return data.ToBag().ToJSON();
            }
            public static String ToJSON<T>(IEnumerable<KeyValuePair<String, T>> data)
            {
                return data.ToBag().ToJSON();
            }
            public static SerializableDataBag<T> ToBag<T>(this IEnumerable<Tuple<String, T>> data)
            {
                return new SerializableDataBag<T>(data);
            }
            public static SerializableDataBag<T> ToBag<T>(this IEnumerable<KeyValuePair<String, T>> data)
            {
                return new SerializableDataBag<T>(data);
            }
            public static String ToBagJSON<T>(this IEnumerable<Tuple<String, T>> data)
            {
                return ToJSON(data);
            }
            public static String ToBagJSON<T>(this IEnumerable<KeyValuePair<String, T>> data)
            {
                return ToJSON(data);
            }
            public static SerializableDataBag<T> FromJSON<T>(String data)
            {
                return SerializableDataBag<T>.FromJSON(data);
            }
        }
    }
}
