window.selectToEnd = function (element) {
    if (element && element.setSelectionRange) {
        const length = element.value.length;
        element.setSelectionRange(length, length);
    }
}
