;(function () {
'use strict'

    cr.plugins_.sweetAlert2 = Plugin

    function Plugin (runtime) {

        this.runtime = runtime

    }

    var pluginProto = Plugin.prototype

    pluginProto.Type = function(plugin) {

        this.plugin = plugin
        this.runtime = plugin.runtime

    }

    var typeProto = pluginProto.Type.prototype

    typeProto.onCreate = function () {}

    pluginProto.Instance = function (type) {

        this.type = type
        this.runtime = type.runtime

    }

    pluginProto.Instance.prototype = {
        onCreate: function () {}
    }

    var value
    var attr
    var option
    var t_f = [true,false]
    var $t_f = [false, true]
    var typeInput = ['text', 'email', 'password', 'textarea', 'select', 'radio', 'checkbox', 'range' ]
    var type = ['', 'success', 'error', 'warning', 'info', 'question']
    pluginProto.acts = {
         alert: function (title,body,typeIndex,confirmButtonText,cancelButtonText,showCancelButton,showConfirmButton,colorConfirmButton,colorCancelButton,reverseButton,showCloseButton,width,padding,allowOutsideClick,allowEscapeKey) {
          var that = this
            swal({
              onOpen: function () {
                  that.runtime.trigger(pluginProto.cnds.onCreate, that)
                },
               title: title,
               text: body,
              type: type[typeIndex],
              confirmButtonText: confirmButtonText,
              cancelButtonText: cancelButtonText,
              showCancelButton: t_f[showCancelButton],
              showConfirmButton: t_f[showConfirmButton],
              confirmButtonColor: colorConfirmButton,
              cancelButtonColor: colorCancelButton,
              // buttonsStyling: t_f[buttonsStyle],
              reverseButtons: $t_f[reverseButton],
              // focusCancel: $t_f[focusCancel],
              showCloseButton: $t_f[showCloseButton],
              // showLoaderOnConfirm: $t_f[showLoaderOnConfirm],
              width: width,
              padding: padding,
              // timer: timer,
              // animation: t_f[animation],
              allowOutsideClick: t_f[allowOutsideClick],
              allowEscapeKey: t_f[allowEscapeKey],
              // allowEnterKey: t_f[allowEnterKey],
              onClose: function () {
                  that.runtime.trigger(pluginProto.cnds.onClose, that)
               },
           }).then(function () {
             that.runtime.trigger(pluginProto.cnds.onClickConfirm, that)
           }, function(dismiss) {
             if (dismiss === 'cancel') {
               that.runtime.trigger(pluginProto.cnds.onClickCancel, that)
             }
           })

          //  swal.onOpen(function () {_this.runtime.trigger(pluginProto.cnds.onCreate, _this)})
          //  swal.onClose(function () {_this.runtime.trigger(pluginProto.cnds.onClose, _this)})
        },
         input: function (title, body,type,rejec,maxlength,inputValuee,attributes,options,placeholder,allowOutsideClick,showCancelButton,showConfirmButton,colorConfirmButton,colorCancelButton,confirmButtonText,cancelButtonText){
            var that = this
            // var IValue
            // if (inputValue === NaN) {
            //   IValue = inputValue
            // }else if (inputValue !== NaN) {
            //   IValue = Number(inputValue)
            // }
            var option = {}
            if (options.match(/\w+/g) !== null) {
              var options = options.match(/\w+/g)
              for (var i = 0; i < options.length; i++) {
                option[options[i]] = options[i]
              }
            }
            //برسی درست بودم attrebute  ها
            var attr = {}
            if (/\d+/g.test(attributes)) {
              attr = attributes.match(/\d+/g)
            }
             swal({
               onOpen: function () {
                   that.runtime.trigger(pluginProto.cnds.onCreate, that)
                 },
                 title: title,
                 text: body,
                 input: typeInput[type],
                 inputValidator: function (value) {
                   return new Promise(function (resolve, reject) {
                     if (value) {
                       resolve()
                     } else {
                       reject(rejec)
                     }
                   })
                 },
                //  inputAttributes: {'minlength': JSON.parse(attr.min), 'maxlength': JSON.parse(attr.max)},
                 inputValue: inputValuee,
                 inputAttributes: {min: Number(attr[0]),max: Number(attr[1]), step: Number(attr[2]) ,'maxlength': maxlength },
                 inputOptions: option,
                 inputPlaceholder: placeholder,
                 confirmButtonText: confirmButtonText,
                 cancelButtonText: cancelButtonText,
                 showCancelButton: t_f[showCancelButton],
                 showConfirmButton: t_f[showConfirmButton],
                 confirmButtonColor: colorConfirmButton,
                 cancelButtonColor: colorCancelButton,
                 allowOutsideClick: t_f[allowOutsideClick],
                 allowEscapeKey: false,
                 onClose: function () {
                     that.runtime.trigger(pluginProto.cnds.onClose, that)
                  },
               }).then(function (valuea) {
                 value = valuea
                 that.runtime.trigger(pluginProto.cnds.onClickConfirm, that)
               }, function(dismiss) {
                 if (dismiss === 'cancel') {
                   that.runtime.trigger(pluginProto.cnds.onClickCancel, that)
                 }
               })
         }
    }
    pluginProto.cnds = {
        onClose: function () {return true},
        onCreate: function () {return true},
        isVisible: function () {if (swal.isVisible()){return true}else {return false}},
        onClickConfirm:function () {return true},
        onClickCancel: function () {return true},
    }
    pluginProto.exps = {
        value: function (ret) {
            ret.set_any(value)
        }
    }

})()
