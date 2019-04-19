var rxn = rxn || {};

(function ($) {

    if (!$) {
        return;
    }

    /* UTILS *************************************************/

    rxn.createNamespace = function (root, ns) {
        var parts = ns.split('.');
        for (var i = 0; i < parts.length; i++) {
            if (typeof root[parts[i]] == 'undefined') {
                root[parts[i]] = {};
            }

            root = root[parts[i]];
        }

        return root;
    };

    /* AJAX *************************************************/

    rxn.ajax = function (userOptions) {
        userOptions = userOptions || {};

        var options = $.extend({}, rxn.ajax.defaultOpts, userOptions);
        options.success = undefined;
        options.error = undefined;

        return $.Deferred(function ($dfd) {
            $.ajax(options)
                .done(function (data, textStatus, jqXHR) {
                    $dfd.resolve(data);
                    rxn.ajax.handleResponse(data, userOptions, $dfd, jqXHR);
                }).fail(function (jqXHR) {
                    rxn.notify.error("An error has occurred !");
                });
        });
    };

    $.extend(rxn.ajax, {
        defaultOpts: {
            dataType: 'json',
            type: 'POST',
            contentType: 'application/json'
        },

        handleResponse: function (data, userOptions, $dfd, jqXHR) {
            if (data) {
                if (data.success === true) {
                    $dfd && $dfd.resolve(data.result, data, jqXHR);
                    userOptions.success && userOptions.success(data.result, data, jqXHR);

                } else if (data.success === false) {
                    rxn.notify.error(data.errorMessage)

                    $dfd && $dfd.reject(data.error, jqXHR);
                    userOptions.error && userOptions.error(data.error, jqXHR);

                }
            } else { //no data sent to back
                $dfd && $dfd.resolve(jqXHR);
                userOptions.success && userOptions.success(jqXHR);
            }
        },
    });


    /* NOTIFY *************************************************/

    rxn.notify = rxn.notify || {};

    if (toastr) {
        toastr.options.positionClass = 'toast-bottom-right';

        var showNotification = function (type, message, title, options) {
            toastr[type](message, title, options);
        };

        rxn.notify.success = function (message, title, options) {
            showNotification('success', message, title, options);
        };

        rxn.notify.info = function (message, title, options) {
            showNotification('info', message, title, options);
        };

        rxn.notify.warn = function (message, title, options) {
            showNotification('warning', message, title, options);
        };

        rxn.notify.error = function (message, title, options) {
            showNotification('error', message, title, options);
        };
    }



    /* JQUERY BLOCK UI *************************************************/

    rxn.ui = rxn.ui || {};

    if ($.blockUI) {
        $.extend($.blockUI.defaults, {
            message: ' ',
            css: { },
            overlayCSS: {
                backgroundColor: '#AAA',
                opacity: 0.3,
                cursor: 'wait'    
            }
        });
        
        rxn.ui.block = function (elm) {
            if (!elm) {
                $.blockUI();
                $('body').spin();
            } else {
                $(elm).block();
            }
        };
    
        rxn.ui.unblock = function (elm) {
            if (!elm) {
                $.unblockUI();
                $('body').spin(false);
            } else {
                $(elm).unblock();
            }
        };
    }

    

})(jQuery);