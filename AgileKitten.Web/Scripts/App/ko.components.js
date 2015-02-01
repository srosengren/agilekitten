ko.components.register('text-toggle', {
	viewModel: function (params) {
		var toggle = this;
		toggle.isOpen = ko.observable(false);
		toggle.text = params.text;
		toggle.value = ko.observable(params.value || '');

		toggle.inputFocus = ko.observable(false);
		toggle.isOpen.subscribe(function (newValue) {
			if (newValue)
				toggle.inputFocus(newValue);
			else
				setTimeout(function () {
					toggle.inputFocus(false);
				},200);
		});
		toggle.commit = function () {
			var args = params.commitParams.slice();
			args.push(toggle.value());
			params.commit.apply(undefined, args);
			toggle.isOpen(false);
		};
	},
	template: '\
		<button type="button" data-bind="text: text,click: isOpen.bind(undefined,true),visible: !isOpen()"></button>\
		<input type="text" data-bind="visible: isOpen, hasFocus: inputFocus,value:value, valueUpdate: \'keypress\'" />\
		<button type="button" data-bind="click: commit,visible: isOpen">+</button>\
'
});