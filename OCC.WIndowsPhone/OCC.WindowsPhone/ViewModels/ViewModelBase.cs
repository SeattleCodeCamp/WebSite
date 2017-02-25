using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows;
using QualityData.Shared.Extensions;

// See Examples For Use at bottom of this file
// This file containing this class may be linked directly into some projects in order to avoid the need to distribute a separate assembly.
// To avoid namespace collisions, the namespace is determined by a conditional compilation constant.

namespace OCC.WindowsPhone.ViewModels
{
    public class ViewModelBase :  INotifyPropertyChanged
    {
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();
        private readonly IDictionary<string, List<string>> _propertyMap;
        private readonly IDictionary<string, List<string>> _methodMap;
        private readonly IDictionary<string, List<string>> _commandMap;

        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = true)]
        protected class DependsUponAttribute : Attribute
        {
            public string DependencyName { get; private set; }

            public bool VerifyStaticExistence { get; set;}
            
            public DependsUponAttribute(string propertyName)
            {
                DependencyName = propertyName;
            }
        }


        public bool IsInDesignMode
        {
            get
            {
                return DesignerProperties.IsInDesignTool;
            }
        }

        public ViewModelBase()
        {
            _propertyMap = MapDependencies<DependsUponAttribute>(() => GetType().GetProperties());
            _methodMap = MapDependencies<DependsUponAttribute>(() => GetType().GetMethods().Cast<MemberInfo>().Where(method => !method.Name.StartsWith(CAN_EXECUTE_PREFIX)));
            _commandMap = MapDependencies<DependsUponAttribute>(() => GetType().GetMethods().Cast<MemberInfo>().Where(method => method.Name.StartsWith(CAN_EXECUTE_PREFIX)));
            CreateCommands();
            VerifyDependancies();
        }

        protected T Get<T>(string name)
        {
            return Get(name, default(T));
        }

        protected T Get<T>(string name, T defaultValue)
        {
            if (_values.ContainsKey(name))
            {
                return (T)_values[name];
            }
            
            return defaultValue;
        }

        protected T Get<T>(string name, Func<T> initialValue)
        {
            if (_values.ContainsKey(name))
            {
                return (T)_values[name];
            }

            Set(name, initialValue());
            return Get<T>(name);
        }

        protected T Get<T>(Expression<Func<T>> expression)
        {
            return Get<T>(PropertyName(expression));
        }

        protected T Get<T>(Expression<Func<T>> expression, T defaultValue)
        {
            return Get(PropertyName(expression), defaultValue);
        }

        protected T Get<T>(Expression<Func<T>> expression, Func<T> initialValue)
        {
            return Get(PropertyName(expression), initialValue);
        }

        public void Set<T>(string name, T value)
        {
            if (_values.ContainsKey(name))
            {
                if (_values[name] == null && value == null)
                    return;

                if (_values[name] != null && _values[name].Equals(value))
                    return;

                _values[name] = value;
            }
            else
            {
                _values.Add(name, value);
            }

            RaisePropertyChanged(name);
        }

        protected void RaisePropertyChanged(string name)
        {
            PropertyChanged.Raise(this, name);
#if SILVERLIGHT
            PropertyChanged.Raise(this, "");
#endif

            if (_propertyMap.ContainsKey(name))
                _propertyMap[name].ForEach(RaisePropertyChanged);

            ExecuteDependentMethods(name);
            FireChangesOnDependentCommands(name);
        }

        private void ExecuteDependentMethods(string name)
        {
            if (_methodMap.ContainsKey(name))
                _methodMap[name].ForEach(ExecuteMethod);
        }

        private void FireChangesOnDependentCommands(string name)
        {
            if (_commandMap.ContainsKey(name))
                _commandMap[name].ForEach(RaiseCanExecuteChangedEvent);
        }

        protected void Set<T>(Expression<Func<T>> expression, T value)
        {
            Set(PropertyName(expression), value);
        }

        private static string PropertyName<T>(Expression<Func<T>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;

            if(memberExpression == null)
                throw new ArgumentException("expression must be a property expression");

            return memberExpression.Member.Name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void CreateCommands()
        {
            CommandNames.ForEach(name => Set(name, new RelayedCommand<object>(x => ExecuteCommand(name, x), x => CanExecuteCommand(name, x))));
        }

        private const string EXECUTE_PREFIX = "Execute_";
        private const string CAN_EXECUTE_PREFIX = "CanExecute_";
        
        private IEnumerable<string> CommandNames
        {
            get
            {
                return from method in GetType().GetMethods()
                       where method.Name.StartsWith(EXECUTE_PREFIX)
                       select method.Name.StripLeft(EXECUTE_PREFIX.Length);
            }
        }

        private void ExecuteCommand(string name, object parameter)
        {
            var methodInfo = GetType().GetMethod(EXECUTE_PREFIX + name);
            if (methodInfo == null) return;

            methodInfo.Invoke(this, methodInfo.GetParameters().Length == 1 ? new[] {parameter} : null);
        }

        private bool CanExecuteCommand(string name, object parameter)
        {
            var methodInfo = GetType().GetMethod(CAN_EXECUTE_PREFIX + name);
            if (methodInfo == null) return true;

            return (bool)methodInfo.Invoke(this, methodInfo.GetParameters().Length == 1 ? new[] { parameter } : null);
        }

        protected void RaiseCanExecuteChangedEvent(string canExecute_name)
        {
            var commandName = canExecute_name.StripLeft(CAN_EXECUTE_PREFIX.Length);
            var command = Get<RelayedCommand<object>>(commandName);
            if (command == null)
                return;
                
            command.RaiseCanExecuteChanged();
        }

#if SILVERLIGHT
        public object this[string key]
        {
            get { return Get<object>(key);}
            set { Set(key, value); }
        }
#endif

        private static IDictionary<string, List<string>> MapDependencies<T>(Func<IEnumerable<MemberInfo>> getInfo) where T : DependsUponAttribute
        {
            var dependencyMap = getInfo().ToDictionary(
                        p => p.Name,
                        p => p.GetCustomAttributes(typeof(T), true)
                              .Cast<T>()
                              .Select(a => a.DependencyName)
                              .ToList());

            return Invert(dependencyMap);
        }

        private static IDictionary<string, List<string>> Invert(IDictionary<string, List<string>> map)
        {
            var flattened = from key in map.Keys
                            from value in map[key]
                            select new { Key = key, Value = value };

            var uniqueValues = flattened.Select(x => x.Value).Distinct();

            return uniqueValues.ToDictionary(
                        x => x,
                        x => (from item in flattened
                             where item.Value == x
                             select item.Key).ToList());
        }

        private void ExecuteMethod(string name)
        {
            var memberInfo = GetType().GetMethod(name);
            if(memberInfo == null)
                return;

            memberInfo.Invoke(this, null);
        }

        private void VerifyDependancies()
        {
            var methods = GetType().GetMethods().Cast<MemberInfo>();
            var properties = GetType().GetProperties();

            var propertyNames = methods.Union(properties)
                .SelectMany(method => method.GetCustomAttributes(typeof (DependsUponAttribute), true).Cast<DependsUponAttribute>())
                .Where(attribute => attribute.VerifyStaticExistence)
                .Select(attribute => attribute.DependencyName);
        
            propertyNames.ForEach(VerifyDependancy);
        }

        private void VerifyDependancy(string propertyName)
        {
            var property = GetType().GetProperty(propertyName);
            if(property == null)
                throw new ArgumentException("DependsUpon Property Does Not Exist: " + propertyName);
        }



        // common Properties
        public virtual bool IsBusy
        {
            get { return Get(() => IsBusy); }
            set { Set(() => IsBusy, value); }
        }

        public string Message
        {
            get { return Get(() => Message); }
            set { Set(() => Message, value); }
        }
        
        protected virtual void HandleError(Exception e, bool display)
        {
            Message = e.Message;
            if (display) MessageBox.Show(e.Message);
        }
        protected void RaisePropertyChanged(params string[] propertyNames)
        {
            if (null == PropertyChanged)
                return;
            foreach (var each in propertyNames)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(each));
            }
        }
        protected void RaisePropertyChanged<TViewModel>(Expression<Func<TViewModel>> property)
        {
            var expression = property.Body as MemberExpression;

            if (PropertyChanged != null && expression !=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(expression.Member.Name));
            }
        }

        #region Examples for use
#if THESE_EXAMPLES_WILL_NOT_BE_COMPILED

        /// <summary>
        /// Default Value example.  The value, 29, is the default
        /// </summary>
        public int Age
        {
            get { return Get(() => Age, 29); }
            set { Set(() => Age, value); }
        }

        /// <summary>
        /// Depends upon example: When another property changes, this method will execute
        /// </summary>
        /// 
        [DependsUpon("Age")]
        public void UpdateAgeInformation()
        {
            // perform some logic as a result of a change to the Age property
        }

        /// <summary>
        /// Cascading DependsUpon example: when another property changes,
        /// this property changes and it then causes the subsequent property to change
        /// </summary>
        [DependsUpon("Age")]
        public int OldAge
        {
            get { return (int)(2 * Age); }
        }

        [DependsUpon("OldAge")]
        public string OldAgeDescription
        {
            get { return "Your old age would be: " + OldAge; }
        }


        /// <summary>
        /// Dynamic Property Binding Examples. 
        /// -SomeMethod()..Creates a virtual property (Wife) based on the provided string
        /// -MaritalDescription automatically raises propertyChanged notification
        /// -A RelayedCommand (UpdateWife) is automatically created based on the method signature
        ///  and can be used to update the dynamic property. If CanExecute_xxx (UpdateWife) is found then
        ///  it works with UpdateWife(). 
        ///  The optional DependsUpon attribute on CanExecute.. illustrates automatic command.RaiseCanExecuteChanged() triggering
        /// </summary>
        void SomeMethod()
        {
            Set("Wife", "Diane");
        }

        [DependsUpon("Wife")]
        public string MaritalDescription
        {
            get { return "My wife's name is " + Get<string>("Wife"); }
        }

        public void Execute_UpdateWife(string name)
        {
            Set("Wife", name);
        }
        [DependsUpon("Age")]
        public bool CanExecute_UpdateWife()
        {
            return true;
        }


        /// <summary>
        /// Dependency Injection example.
        /// - MyViewModel constructor requires IPerson
        /// - Concrete instance of IPerson (Person) is found/created and provided as a param
        /// </summary>
        public interface IPerson
        {
            string LastName { get; }
        }
        public class Person : IPerson
        {
            public string LastName { get { return "Smith"; } }
        }
        public class MyViewModel : ViewModelBase
        {
            public MyViewModel(IPerson person)
            {
                PersonLastName = person.LastName;
            }
            public string PersonLastName { get; private set; }
        }

#endif

        #endregion



    }

}
