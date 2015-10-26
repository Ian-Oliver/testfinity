using System;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using System.Collections.Generic;
using System.Collections;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Modules.Pages;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Multisite;
using System.Web;

namespace SitefinityWebApp.WidgetDesigners.MvcWidget
{
    /// <summary>
    /// Represents a designer for the <typeparamref name="SitefinityWebApp.Mvc.Controllers.MvcWidgetController"/> widget
    /// </summary>
    public class MvcWidgetDesigner : ControlDesignerBase
    {
        #region Properties
        /// <summary>
        /// Obsolete. Use LayoutTemplatePath instead.
        /// </summary>
        protected override string LayoutTemplateName
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the layout template's relative or virtual path.
        /// </summary>
        public override string LayoutTemplatePath
        {
            get
            {
                if (string.IsNullOrEmpty(base.LayoutTemplatePath))
                    return MvcWidgetDesigner.layoutTemplatePath;
                return base.LayoutTemplatePath;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }
        #endregion

        #region Control references
        /// <summary>
        /// Gets the control that is bound to the Message property
        /// </summary>
        protected virtual Control LoginPlaceholder
        {
            get
            {
                return this.Container.GetControl<Control>("LoginPlaceholder", true);
            }
        }

        /// <summary>
        /// Gets the control that is bound to the UsernamePlaceholder property
        /// </summary>
        protected virtual Control UsernamePlaceholder
        {
            get
            {
                return this.Container.GetControl<Control>("UsernamePlaceholder", true);
            }
        }

        /// <summary>
        /// Gets the control that is bound to the PasswordPlaceholder property
        /// </summary>
        protected virtual Control PasswordPlaceholder
        {
            get
            {
                return this.Container.GetControl<Control>("PasswordPlaceholder", true);
            }
        }

        protected Control StandardValue
        {
            get
            {
                return this.Container.GetControl<Control>("StandardValue", true);
            }
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Called when the deisnger loads
        /// </summary>
        /// <param name="container">The generic container</param>
        protected override void InitializeControls(GenericContainer container)
        {
            WidgetDesignerHelper helper = new WidgetDesignerHelper();
            helper.AddStandardValue(container.Controls[0].Controls.Cast<Control>());            
        }
        #endregion

        #region IScriptControl implementation
        /// <summary>
        /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
        /// </summary>
        public override System.Collections.Generic.IEnumerable<System.Web.UI.ScriptDescriptor> GetScriptDescriptors()
        {
            var scriptDescriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
            var descriptor = (ScriptControlDescriptor)scriptDescriptors.Last();

            descriptor.AddElementProperty("loginPlaceholder", this.LoginPlaceholder.ClientID);
            descriptor.AddElementProperty("usernamePlaceholder", this.UsernamePlaceholder.ClientID);
            descriptor.AddElementProperty("passwordPlaceholder", this.PasswordPlaceholder.ClientID);

            return scriptDescriptors;
        }

        /// <summary>
        /// Gets a collection of ScriptReference objects that define script resources that the control requires.
        /// </summary>
        public override System.Collections.Generic.IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences()
        {
            var scripts = new List<ScriptReference>(base.GetScriptReferences());
            scripts.Add(new ScriptReference(MvcWidgetDesigner.scriptReference));
            return scripts;
        }
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/WidgetDesigners/MvcWidget/MvcWidgetDesigner.ascx";
        public const string scriptReference = "~/WidgetDesigners/MvcWidget/MvcWidgetDesigner.js";
        #endregion
    }
}
 
