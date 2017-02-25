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
using System.Windows.Input;

namespace OCC.WindowsPhone.ViewModels
{
    public class RelayedCommand<T> : ICommand
    {
        private readonly Action<T> execute;
        private readonly Predicate<T> canExecute;

        public RelayedCommand(Action<T> execute)
            :this(execute, x => true)
        {
        }

        public RelayedCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (canExecute == null) throw new ArgumentNullException("canExecute");
            if (execute == null) throw new ArgumentNullException("execute");

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            execute((T) parameter);
        }

        public bool CanExecute(object parameter)
        {
            return canExecute((T)parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged.Raise(this);
        }
    }

    public class RelayedCommand : RelayedCommand<object>
    {
        public RelayedCommand(Action execute)
            : base(execute != null ? x => execute() : (Action<object>)null)
        {
        }

        public RelayedCommand(Action execute, Func<bool> canExecute)
            : base( execute != null ? x => execute() : (Action<object>)null, 
                    canExecute != null ? x => canExecute() : (Predicate<object>)null)
        {
        }
    }
}
