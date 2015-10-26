Type.registerNamespace("SitefinityWebApp");

SitefinityWebApp.FormDesigner = function (element) {
    /* Initialize MyLabel fields */
    this._myLabel = null;
    
    /* Initialize MyCheckbox fields */
    this._myCheckbox = null;
    
    /* Calls the base constructor */
    SitefinityWebApp.FormDesigner.initializeBase(this, [element]);
}

SitefinityWebApp.FormDesigner.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        /* Here you can attach to events or do other initialization */
        SitefinityWebApp.FormDesigner.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        /* this is the place to unbind/dispose the event handlers created in the initialize method */
        SitefinityWebApp.FormDesigner.callBaseMethod(this, 'dispose');
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

        /* RefreshUI MyCheckbox */
        jQuery(this.get_myCheckbox()).attr("checked", controlData.MyCheckbox);
    },

    /* Called when the "Save" button is clicked. Here you can transfer the settings from the designer to the control */
    applyChanges: function () {
        var controlData = this._propertyEditor.get_control();

        /* ApplyChanges MyLabel */
        controlData.MyLabel = jQuery(this.get_myLabel()).val();

        /* ApplyChanges MyCheckbox */
        controlData.MyCheckbox = jQuery(this.get_myCheckbox()).is(":checked");
    },

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */

    /* MyLabel properties */
    get_myLabel: function () { return this._myLabel; }, 
    set_myLabel: function (value) { this._myLabel = value; },

    /* MyCheckbox properties */
    get_myCheckbox: function () { return this._myCheckbox; }, 
    set_myCheckbox: function (value) { this._myCheckbox = value; }
}

SitefinityWebApp.FormDesigner.registerClass('SitefinityWebApp.FormDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
