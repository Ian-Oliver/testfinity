Type.registerNamespace("SitefinityWebApp.WidgetDesigners.ConfigWidget");

SitefinityWebApp.WidgetDesigners.ConfigWidget.ConfigWidgetDesigner = function (element) {
    /* Initialize ParentName fields */
    this._parentName = null;
    
    /* Initialize MyCheckbox fields */
    this._myCheckbox = null;
    
    /* Calls the base constructor */
    SitefinityWebApp.WidgetDesigners.ConfigWidget.ConfigWidgetDesigner.initializeBase(this, [element]);
}

SitefinityWebApp.WidgetDesigners.ConfigWidget.ConfigWidgetDesigner.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        /* Here you can attach to events or do other initialization */
        SitefinityWebApp.WidgetDesigners.ConfigWidget.ConfigWidgetDesigner.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        /* this is the place to unbind/dispose the event handlers created in the initialize method */
        SitefinityWebApp.WidgetDesigners.ConfigWidget.ConfigWidgetDesigner.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods ---------------------------------- */

    findElement: function (id) {
        var result = jQuery(this.get_element()).find("#" + id).get(0);
        return result;
    },

    /* Called when the designer window gets opened and here is place to "bind" your designer to the control properties */
    refreshUI: function () {
        var controlData = this._propertyEditor.get_control().Settings; /* JavaScript clone of your control - all the control properties will be properties of the controlData too */

        /* RefreshUI ParentName */
        jQuery(this.get_parentName()).val(controlData.ParentName);

        /* RefreshUI MyCheckbox */
        jQuery(this.get_myCheckbox()).attr("checked", controlData.MyCheckbox);
    },

    /* Called when the "Save" button is clicked. Here you can transfer the settings from the designer to the control */
    applyChanges: function () {
        var controlData = this._propertyEditor.get_control().Settings;

        /* ApplyChanges ParentName */
        controlData.ParentName = jQuery(this.get_parentName()).val();

        /* ApplyChanges MyCheckbox */
        controlData.MyCheckbox = jQuery(this.get_myCheckbox()).is(":checked");
    },

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */

    /* ParentName properties */
    get_parentName: function () { return this._parentName; }, 
    set_parentName: function (value) { this._parentName = value; },

    /* MyCheckbox properties */
    get_myCheckbox: function () { return this._myCheckbox; }, 
    set_myCheckbox: function (value) { this._myCheckbox = value; }
}

SitefinityWebApp.WidgetDesigners.ConfigWidget.ConfigWidgetDesigner.registerClass('SitefinityWebApp.WidgetDesigners.ConfigWidget.ConfigWidgetDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
