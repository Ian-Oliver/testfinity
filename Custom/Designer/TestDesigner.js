Type.registerNamespace("SitefinityWebApp.Custom.Designer");

SitefinityWebApp.Custom.Designer.TestDesigner = function (element) {
    /* Initialize MyLabel fields */
    this._myLabel = null;
    
    /* Calls the base constructor */
    SitefinityWebApp.Custom.Designer.TestDesigner.initializeBase(this, [element]);
}

SitefinityWebApp.Custom.Designer.TestDesigner.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        /* Here you can attach to events or do other initialization */
        SitefinityWebApp.Custom.Designer.TestDesigner.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        /* this is the place to unbind/dispose the event handlers created in the initialize method */
        SitefinityWebApp.Custom.Designer.TestDesigner.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods ---------------------------------- */

    findElement: function (id) {
        var result = jQuery(this.get_element()).find("#" + id).get(0);
        return result;
    },

    /* Called when the designer window gets opened and here is place to "bind" your designer to the control properties */
    refreshUI: function () {
        var controlData = this._propertyEditor.get_control(); /* JavaScript clone of your control - all the control properties will be properties of the controlData too */

        /* RefreshUI MyLabel */
        jQuery(this.get_myLabel()).val(controlData.MyLabel);

    },

    /* Called when the "Save" button is clicked. Here you can transfer the settings from the designer to the control */
    applyChanges: function () {
        var controlData = this._propertyEditor.get_control();

        /* ApplyChanges MyLabel */
        controlData.MyLabel = jQuery(this.get_myLabel()).val();
    },

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */

    /* MyLabel properties */
    get_myLabel: function () { return this._myLabel; }, 
    set_myLabel: function (value) { this._myLabel = value; },
}

SitefinityWebApp.Custom.Designer.TestDesigner.registerClass('SitefinityWebApp.Custom.Designer.TestDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
