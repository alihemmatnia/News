function GetPluginSettings() {

    return {
        name: 'sweetAlert2',
        id: 'sweetAlert2',
        version: '1.0',
        description: 'This plugin for alert is beautiful',
        author: 'mostafa fallahi',
        'help url': '',
        category: 'General',
        type: 'object',
        rotatable: false,
        dependency: 'sweetalert2.min.js;sweetalert2.min.css;sweetalert2.js',
        flags: pf_singleglobal
    }
}
///////////////////////////////////////////
//Condition
AddCondition(0, cf_trigger, 'on close', 'General', 'on close', 'When the alert was closed', 'onClose')
AddCondition(1, cf_trigger, 'on create', 'General', 'on create', 'when the alert was create', 'onCreate')
AddCondition(2, cf_none, 'is visible', 'General', 'is visible', '', 'isVisible')
AddCondition(3, cf_trigger, 'on click confirm button', 'Click', 'on click confirm', 'When click on the confirm button.', 'onClickConfirm')
AddCondition(4, cf_trigger, 'on click cancel button', 'Click', 'on click cancel', 'When click on the cancel button.', 'onClickCancel')
///////////////////////////////////////////
//Action

AddStringParam('title', 'title for the alert')
AddStringParam('body', 'text alert')

AddComboParamOption('none')
AddComboParamOption('success')
AddComboParamOption('error')
AddComboParamOption('warning')
AddComboParamOption('info')
AddComboParamOption('question')
AddComboParam('type', 'Select the type of alert')

AddStringParam('confirm button text', 'the confirm button text', '"ok"')
AddStringParam('cancel button text', 'the cancel button text', '"Cancel"')

AddComboParamOption('true')
AddComboParamOption('false')
AddComboParam('show cancel botton', 'show the cancel button (true or false)')

AddComboParamOption('true')
AddComboParamOption('false')
AddComboParam('show confirm botton', 'show the confirm botton (true or false)')
AddStringParam('color confirm button', 'the color of confirm button', '"#3085d6"')
AddStringParam(' color cancel button', 'the color of cancel button', '"#aaa"')

// AddComboParamOption('true')
// AddComboParamOption('false')
// AddComboParam('buttons style', '')

AddComboParamOption('false')
AddComboParamOption('true')
AddComboParam('reverse buttons', 'Set to true if you want to invert default buttons positions ("Confirm"-button on the right side)')

// AddComboParamOption('false')
// AddComboParamOption('true')
// AddComboParam('focus cancel', 'Set to true if you want to focus the "Cancel"-button by default.')

AddComboParamOption('false')
AddComboParamOption('true')
AddComboParam('show close button', 'Set to true to show close button in top right corner of the modal')

// AddComboParamOption('false')
// AddComboParamOption('true')
// AddComboParam('show loader on confirm', 'Set to true to disable buttons and show that something is loading')
AddStringParam('width', 'Modal window width', '"500px"')
AddNumberParam('padding', 'Modal window padding.', '20')
// AddStringParam('timer', 'Auto close timer of the modal.', '"null"')

// AddComboParamOption('true')
// AddComboParamOption('false')
// AddComboParam('animation', 'If set to false, modal animation will be disabled')
//
AddComboParamOption('true')
AddComboParamOption('false')
AddComboParam('allow outside click', "If set to false, the user can't dismiss the modal by clicking outside it")

AddComboParamOption('true')
AddComboParamOption('false')
AddComboParam('allow escape key', "If set to false, the user can't dismiss the modal by pressing the Escape key.")

// AddComboParamOption('false')
// AddComboParamOption('true')
// AddComboParam('allow enter key', "If set to false, the user can't confirm the modal by pressing the Enter or Space keys, unless they manually focus the confirm button.")

AddAction(0, af_none, 'alert', 'Alert', 'alert {1}: {2}', 'Beautiful alerts', 'alert')


AddStringParam('title', 'title for the alert')
AddStringParam('body', 'text fot alert')

AddComboParamOption('text')
AddComboParamOption('email')
AddComboParamOption('password')
AddComboParamOption('textarea')
AddComboParamOption('select')
AddComboParamOption('radio')
AddComboParamOption('checkbox')
AddComboParamOption('range')
AddComboParam('type', 'type of alert')

AddStringParam('reject', 'When the bar is empty text input', '""')

AddNumberParam('max length', 'the max length for fild input', '10')
AddStringParam('input value', 'The initial value of the input field', '""')
AddStringParam("attributes", '"min;max;step"', '""')
AddStringParam('options', '"Option1;option2;option3..."', '""')
AddStringParam('plaseholder', 'input field placeholder', '""')

AddComboParamOption('true')
AddComboParamOption('false')
AddComboParam('allow outside click', "If set to false, the user can't dismiss the modal by clicking outside it")

AddComboParamOption('true')
AddComboParamOption('false')
AddComboParam('show cancel botton', 'show the cancel button (true or false)')

AddComboParamOption('true')
AddComboParamOption('false')
AddComboParam('show confirm botton', 'show the confirm botton (true or false)')

AddStringParam('color confirm button', 'the color of confirm button', '"#3085d6"')
AddStringParam(' color cancel button', 'the color of cancel button', '"#aaa"')
AddStringParam('confirm button text', 'the confirm button text', '"OK"')
AddStringParam('cancel button text', 'the cancel button text', '"Cancel"')

AddAction(1, af_none, 'input', 'Input', 'input {0}: {1}', 'Beautiful input', 'input')
/////////////////////////////////////////////
//Expression
AddExpression(0, ef_return_string, 'value', 'General', 'value', 'return value input')

/////////////////////////////////////////////
ACESDone();

var property_list = []

function CreateIDEObjectType() {

    return new ObjectType()
}

function ObjectType(){}

ObjectType.prototype.CreateInstance = function (instance) {
	return new Instance(instance)
}

function Instance(instance) {
	this.instance = instance
    this.properties = {}
}

Instance.prototype = {
    OnPropertyChanged: function (prop) {}
}
