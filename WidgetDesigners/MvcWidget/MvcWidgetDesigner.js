Type.registerNamespace("SitefinityWebApp.WidgetDesigners.MvcWidget");

SitefinityWebApp.WidgetDesigners.MvcWidget.MvcWidgetDesigner = function (element) {
    /* Initialize Message fields */
    this._loginPlaceholder = null;
    this._usernamePlaceholder = null;
    this._passwordPlaceholder = null;
    
    /* Calls the base constructor */
    SitefinityWebApp.WidgetDesigners.MvcWidget.MvcWidgetDesigner.initializeBase(this, [element]);
}

SitefinityWebApp.WidgetDesigners.MvcWidget.MvcWidgetDesigner.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        /* Here you can attach to events or do other initialization */
        SitefinityWebApp.WidgetDesigners.MvcWidget.MvcWidgetDesigner.callBaseMethod(this, 'initialize');        
    },
    dispose: function () {
        /* this is the place to unbind/dispose the event handlers created in the initialize method */
        SitefinityWebApp.WidgetDesigners.MvcWidget.MvcWidgetDesigner.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods ---------------------------------- */

    findElement: function (id) {
        var result = jQuery(this.get_element()).find("#" + id).get(0);
        return result;
    },

    /* Called when the designer window gets opened and here is place to "bind" your designer to the control properties */
    refreshUI: function () {
        var controlData = this._propertyEditor.get_control().Settings; /* JavaScript clone of your control - all the control properties will be properties of the controlData too */

        /* RefreshUI Message */
        jQuery(this.get_loginPlaceholder()).val(controlData.LoginPlaceholder);
        jQuery(this.get_usernamePlaceholder()).val(controlData.UsernamePlaceholder);
        jQuery(this.get_passwordPlaceholder()).val(controlData.PasswordPlaceholder);
    },

    /* Called when the "Save" button is clicked. Here you can transfer the settings from the designer to the control */
    applyChanges: function () {
        var controlData = this._propertyEditor.get_control().Settings;

        /* ApplyChanges Message */
        controlData.LoginPlaceholder = jQuery(this.get_loginPlaceholder()).val();
        controlData.UsernamePlaceholder = jQuery(this.get_usernamePlaceholder()).val();
        controlData.PasswordPlaceholder = jQuery(this.get_passwordPlaceholder()).val();
    },

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */

    /* Message properties */
    get_loginPlaceholder: function () { return this._loginPlaceholder; }, 
    set_loginPlaceholder: function (value) { this._loginPlaceholder = value; },

    /* Message properties */
    get_usernamePlaceholder: function () { return this._usernamePlaceholder; },
    set_usernamePlaceholder: function (value) { this._usernamePlaceholder = value; },

    /* Message properties */
    get_passwordPlaceholder: function () { return this._passwordPlaceholder; },
    set_passwordPlaceholder: function (value) { this._passwordPlaceholder = value; }
}

SitefinityWebApp.WidgetDesigners.MvcWidget.MvcWidgetDesigner.registerClass('SitefinityWebApp.WidgetDesigners.MvcWidget.MvcWidgetDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);


