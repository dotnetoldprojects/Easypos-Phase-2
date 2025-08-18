using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUIForms.helpers
{
    public static class Commondatasales
    {
        public static void FillCombo<T>(ComboBox combo, List<T> dataSource, string display, string value)
        {
            if (dataSource == null || !dataSource.Any()) return;

            // إنشاء كائن جديد من النوع T وإضافة "اختر..." له
            var placeholder = Activator.CreateInstance<T>();
            var displayProp = typeof(T).GetProperty(display);
            var valueProp = typeof(T).GetProperty(value);

            // تعيين قيمة العرض والقيمة
            if (displayProp != null)
                displayProp.SetValue(placeholder, "-- اختر --");

            if (valueProp != null)
            {
                if (valueProp.PropertyType == typeof(int))
                    valueProp.SetValue(placeholder, 0);
                else if (valueProp.PropertyType == typeof(string))
                    valueProp.SetValue(placeholder, "0");
            }

            dataSource.Insert(0, placeholder);

            // ربط البيانات بالكمبو
            combo.DataSource = null;
            combo.DataSource = dataSource;
            combo.DisplayMember = display;
            combo.ValueMember = value;
            combo.SelectedIndex = 0;
        }
    }

}
