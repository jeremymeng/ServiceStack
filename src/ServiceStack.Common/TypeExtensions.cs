using System;
using System.Collections.Generic;
#if DNXCORE50
using System.Linq;
using System.Reflection;
#endif

namespace ServiceStack
{
    public static class TypeExtensions
    {
        public static Type[] GetReferencedTypes(this Type type)
        {
            var refTypes = new HashSet<Type> { type };

            AddReferencedTypes(type, refTypes);

            return refTypes.ToArray();
        }

#if !DNXCORE50
        /// <summary>
        /// Just pass through for Desktop .Net Framework.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetTypeInfo(this Type type)
        {
            return type;
        }
#endif

        public static void AddReferencedTypes(Type type, HashSet<Type> refTypes)
        {
            if (type.GetTypeInfo().BaseType != null)
            {
                if (!refTypes.Contains(type.GetTypeInfo().BaseType))
                {
                    refTypes.Add(type.GetTypeInfo().BaseType);
                    AddReferencedTypes(type.GetTypeInfo().BaseType, refTypes);
                }

                if (!type.GetTypeInfo().BaseType.GetGenericArguments().IsEmpty())
                {
                    foreach (var arg in type.GetTypeInfo().BaseType.GetGenericArguments())
                    {
                        if (!refTypes.Contains(arg))
                        {
                            refTypes.Add(arg);
                            AddReferencedTypes(arg, refTypes);
                        }
                    }
                }
            }

            var properties = type.GetProperties();
            if (!properties.IsEmpty())
            {
                foreach (var p in properties)
                {
                    if (!refTypes.Contains(p.PropertyType))
                    {
                        refTypes.Add(p.PropertyType);
                        AddReferencedTypes(type, refTypes);
                    }

                    var args = p.PropertyType.GetGenericArguments();
                    if (!args.IsEmpty())
                    {
                        foreach (var arg in args)
                        {
                            if (!refTypes.Contains(arg))
                            {
                                refTypes.Add(arg);
                                AddReferencedTypes(arg, refTypes);
                            }
                        }
                    }
                    else if (p.PropertyType.IsArray)
                    {
                        var elType = p.PropertyType.GetElementType();
                        if (!refTypes.Contains(elType))
                        {
                            refTypes.Add(elType);
                            AddReferencedTypes(elType, refTypes);
                        }
                    }
                }
            }
        }

#if DNXCORE50
        public static TypeCode GetTypeCode(Type type)
        {
            if (type == null) return TypeCode.Empty;
            TypeCode result;
            if (typeCodeLookup.TryGetValue(type, out result)) return result;

            if (type.IsEnum())
            {
                type = Enum.GetUnderlyingType(type);
                if (typeCodeLookup.TryGetValue(type, out result)) return result;
            }
            return TypeCode.Object;
        }
        static readonly Dictionary<Type, TypeCode> typeCodeLookup = new Dictionary<Type, TypeCode>
        {
            {typeof(bool), TypeCode.Boolean },
            {typeof(byte), TypeCode.Byte },
            {typeof(char), TypeCode.Char},
            {typeof(DateTime), TypeCode.DateTime},
            {typeof(decimal), TypeCode.Decimal},
            {typeof(double), TypeCode.Double },
            {typeof(short), TypeCode.Int16 },
            {typeof(int), TypeCode.Int32 },
            {typeof(long), TypeCode.Int64 },
            {typeof(object), TypeCode.Object},
            {typeof(sbyte), TypeCode.SByte },
            {typeof(float), TypeCode.Single },
            {typeof(string), TypeCode.String },
            {typeof(ushort), TypeCode.UInt16 },
            {typeof(uint), TypeCode.UInt32 },
            {typeof(ulong), TypeCode.UInt64 },
        };
#else
        public static TypeCode GetTypeCode(Type type)
        {
            return Type.GetTypeCode(type);
        }
#endif
    }

}