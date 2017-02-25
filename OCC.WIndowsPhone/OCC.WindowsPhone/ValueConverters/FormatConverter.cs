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
using System.Windows.Data;
using System.Globalization;
using System;
namespace OCC.WindowsPhone.ValueConverters
{
    public class FormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                var formatterString = parameter.ToString();
                if (!string.IsNullOrEmpty(formatterString))
                {
                    // convert dates to local time
                    if (value.GetType() == typeof(DateTime))
                    {
                        var date = (DateTime)value;
                        value = TimeZoneInfo.ConvertTime(date, TimeZoneInfo.Local);
                    }

                    if (formatterString == "YN")
                    {
                        Boolean resultBool;
                        if (Boolean.TryParse(value.ToString(), out resultBool))
                        {
                            return resultBool == true ? "Yes" : "No";
                        }
                    }
                    else if (formatterString.IndexOf("{").Equals(-1))
                    {
                        formatterString = "{0:" + formatterString + "}";
                    }
                    return string.Format(culture, formatterString, value);
                }
            }
            return value.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var strValue = value.ToString();
            if (targetType == typeof(DateTime))
            {
                DateTime resultDateTime;
                if (DateTime.TryParse(strValue, out resultDateTime))
                {
                    return resultDateTime;
                }
            }
            else if (targetType == typeof(Int32))
            {
                Int32 resultInt;
                if (Int32.TryParse(strValue, out resultInt))
                {
                    return resultInt;
                }

            }
            else if (targetType == typeof(bool))
            {
                bool resultBool;
                if (bool.TryParse(strValue, out resultBool))
                {
                    return resultBool;
                }
            }
            return value;
        }
    }
}
