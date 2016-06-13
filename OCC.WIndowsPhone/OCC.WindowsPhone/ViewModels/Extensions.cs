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

namespace OCC.WindowsPhone.ViewModels
{
    public static class Extensions
    {
        public static string StripLeft(this string value, int length)
        {
            return value.Substring(length, value.Length - length);
        }

        public static void Raise(this PropertyChangedEventHandler eventHandler, object source, string propertyName)
        {
            var handlers = eventHandler;
            if (handlers != null)
                handlers(source, new PropertyChangedEventArgs(propertyName));
        }

        public static void Raise(this EventHandler eventHandler, object source)
        {
            var handlers = eventHandler;
            if (handlers != null)
                handlers(source, EventArgs.Empty);
        }

        public static void Register(this INotifyPropertyChanged model, string propertyName, Action whenChanged)
        {
            model.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == propertyName)
                    whenChanged();
            };
        }
    }
}
