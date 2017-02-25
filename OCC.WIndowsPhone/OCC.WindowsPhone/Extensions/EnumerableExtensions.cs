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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

// This file containing this class may be linked directly into some projects in order to avoid the need to distribute a separate assembly.
// To avoid namespace collisions, the namespace is determined by a conditional compilation constant.
#if IS_COMMON_ASSEMBLY
namespace QualityData.Common.Extensions
#else
namespace QualityData.Shared.Extensions
#endif
{
    /// <summary>
    /// Extension methods for classes that derive from IList and/or IEnumerable
    /// </summary>
    public static class EnumerableExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> coll)
        {
            var c = new ObservableCollection<T>();
            if (coll != null)
            {
                foreach (var e in coll)
                    c.Add(e);
            }

            return c;
        }
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
                action(item);
        }
        public static T LastItem<T>(this List<T> list)
        {
            return list.LastOrDefault();

        }

    }

}
