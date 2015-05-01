#region License, Terms and Author(s)
//
// Orlando Code Camp for Windows Phone 7
// Copyright (c) 2012 Orlando .Net User Group. All rights reserved.
//
//  Author(s):
//
//      Brian Mishler, http://www.qualitydata.com
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using System.ComponentModel;

// This file containing this class may be linked directly into some projects in order to avoid the need to distribute a separate assembly.
// To avoid namespace collisions, the namespace is determined by a conditional compilation constant.
#if IS_COMMON_ASSEMBLY
namespace QualityData.Common.Extensions
#else
namespace QualityData.Shared.Extensions
#endif
{
    public static class EnumExtensions
    {

        /// <summary>
        /// Returns the description for the enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

  



#if !SILVERLIGHT
        public static object EnumValueOf(string value, Type enumType)
        {
            var names = Enum.GetNames(enumType);
            foreach (var name in names.Where(name => GetDescription((Enum)Enum.Parse(enumType, name)).Equals(value)))
            {
                return Enum.Parse(enumType, name);
            }

            throw new ArgumentException("The string is not a description or value of the specified enum.");
        }
        /// <summary>
        /// Returns a list of Key/Value pairs where the key is the enum and the value is the description
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IList<KeyValuePair<Enum, string>> ToList(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (!type.IsEnum)
            {
                throw new ArgumentException("Argument must be enum", "type");
            }

            var enumValues = Enum.GetValues(type);

            return (from Enum value in enumValues select new KeyValuePair<Enum, string>(value, GetDescription(value))).ToList();
        }

        /// <summary>
        /// Returns a list descriptions for the enum
        /// </summary>
        /// <typeparam name="T">typeof the enum</typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IList ToList<T>(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (!type.IsEnum)
            {
                throw new ArgumentException("Argument must be enum", "type");
            }

            var enumValues = Enum.GetValues(type);
            return (from Enum value in enumValues
                    select new KeyValuePair<T, string>((T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture), GetDescription(value))).ToList();
        }


#endif




    }
}
