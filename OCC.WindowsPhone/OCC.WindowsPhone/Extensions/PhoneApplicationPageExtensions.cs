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
using Microsoft.Phone.Controls;

namespace OCC.WindowsPhone.Extensions
{
    public static class PhoneApplicationPageExtensions
    {
        public static string QueryStringValue(this PhoneApplicationPage page, string key)
        {
            string val;
            return page.NavigationContext.QueryString.TryGetValue(key, out val) ? val : null;
        }
        public static int QueryStringInt(this PhoneApplicationPage page, string key)
        {
            int i;
            Int32.TryParse(page.QueryStringValue(key) ?? "0", out i);
            return i;

        }
    }
}
