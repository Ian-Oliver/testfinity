Type.registerNamespace("SitefinityWebApp.Custom.Designer");

SitefinityWebApp.Custom.Designer.ConfigDesigner = function (element) {
    /* Initialize ParentName fields */
    this._parentName = null;
    
    /* Calls the base constructor */
    SitefinityWebApp.Custom.Designer.ConfigDesigner.initializeBase(this, [element]);
}

SitefinityWebApp.Custom.Designer.ConfigDesigner.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        /* Here you can attach to events or do other initialization */
        SitefinityWebApp.Custom.Designer.ConfigDesigner.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        /* this is the place to unbind/dispose the event handlers created in the initialize method */
        SitefinityWebApp.Custom.Designer.ConfigDesigner.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods ---------------------------------- */

    findElement: function (id) {
        var result = jQuery(this.get_element()).find("#" + id).get(0);
        return result;
    },

    /* Called when the designer window gets opened and here is place to "bind" your designer to the control properties */
    refreshUI: function () {
        var controlData = this._propertyEditor.get_control(); /* JavaScript clone of your control - all the control properties will be properties of the controlData too */

        /* RefreshUI ParentName */
        jQuery(this.get_parentName()).val(controlData.ParentName);
    },

    /* Called when the "Save" button is clicked. Here you can transfer the settings from the designer to the control */
    applyChanges: function () {
        var controlData = this._propertyEditor.get_control();

        /* ApplyChanges ParentName */
        controlData.ParentName = jQuery(this.get_parentName()).val();
    },

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */

    /* ParentName properties */
    get_parentName: function () { return this._parentName; }, 
    set_parentName: function (value) { this._parentName = value; }
}

SitefinityWebApp.Custom.Designer.ConfigDesigner.registerClass('SitefinityWebApp.Custom.Designer.ConfigDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
