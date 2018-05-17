using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Markup;
using TaskControlSystem.DataAccess.Models;

namespace TaskControlSystem.Views
{
    public class EnumValuesExtension : MarkupExtension
    {
        private readonly List<EnumValueDescription> _list;

        public EnumValuesExtension(Type type)
        {
            if (type.Name != nameof(TaskStatus)) return;

            _list = new List<EnumValueDescription>();
            foreach (var enumValue in type.GetEnumValues())
            {
                _list.Add(new EnumValueDescription(enumValue, GetDisplayName(type, enumValue.ToString())));
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _list;
        }

        private static string GetDisplayName(Type type, string name)
        {
            var attr =
                (DisplayAttribute)
                    type.GetField(name).GetCustomAttributes(typeof(DisplayAttribute), true).SingleOrDefault();

            return (attr != null) ? attr.Name : string.Empty;
        }
    }
}
