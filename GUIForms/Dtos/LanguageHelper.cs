using Domain.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace GUIForms.Dtos
{
    public static class LanguageHelper
    {
        public static object ApplyLanguage(Form form)
        {
            if (form != null) {
                Getcentralaizes GL = new Getcentralaizes();
                company DC = (company)GL.Getcompanydatalist();
                if (DC != null)
                {
                    string lang = DC.Systemlang;
                    if (lang == "الانجليزية" || lang == "English")
                    {
                        SetCulture("en", form);
                    }
                    else
                    {
                        SetCulture("ar", form);
                    }
                }
                return DC;
            }
            else
            {
                return null;
            }
        }

        private static void SetCulture(string cultureName, Form form)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(cultureName);

            var resources = new ComponentResourceManager(form.GetType());

            foreach (Control c in GetAllControls(form))
            {
                resources.ApplyResources(c, c.Name);
            }

            resources.ApplyResources(form, "$this");
        }

        private static IEnumerable<Control> GetAllControls(Control control)
        {
            foreach (Control child in control.Controls)
            {
                foreach (var grandChild in GetAllControls(child))
                {
                    yield return grandChild;
                }
                yield return child;
            }
        }
    }
}
