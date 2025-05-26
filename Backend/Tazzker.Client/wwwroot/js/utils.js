window.selectToEnd = function (element) {
    if (element && element.setSelectionRange) {
        const length = element.value.length;
        element.setSelectionRange(length, length);
    }
}
window.checkIndexedDbStores = async function (dbName) {
    return new Promise((resolve, reject) => {
        const request = indexedDB.open(dbName);

        request.onsuccess = function (event) {
            const db = event.target.result;
            const storeNames = db.objectStoreNames;
            resolve(storeNames.length > 0); // true если есть хотя бы один store
            db.close();
        };

        request.onerror = function () {
            resolve(false); // ошибка = считаем что нет
        };
    });
};


window.focusAndMoveCursorToEnd = function (el) {
    if (!el) return;
    el.focus();
    const val = el.value;
    el.value = '';
    el.value = val; // хак чтобы курсор прыгнул в конец
};

window.noteInterop = {
    getContent: function (selector) {
        const el = document.querySelector(selector);
        return el ? el.innerText : "";
    },

    setContent: function (selector, content) {
        const el = document.querySelector(selector);
        if (el) el.innerText = content;
    },
    preventLastInput: function (el) {
        let text = el.innerText;
        el.innerText = text.substring(0, text.length - 1);

        let range = document.createRange();
        let sel = window.getSelection();
        range.selectNodeContents(el);
        range.collapse(false);
        sel.removeAllRanges();
        sel.addRange(range);
    }

};


window.isOnline = () => navigator.onLine;



/*window.resizeTextareas = () => {
    console.log('[resizeTextareas] вызвана');

    const elements = document.querySelectorAll('textarea');
    console.log(`[resizeTextareas] найдено ${elements.length} textarea`);

    elements.forEach(el => {
        el.style.height = 'auto';
        el.style.height = el.scrollHeight + 'px';
    });
};
*/

window.resizeTextareas = () => {
    console.log('[resizeTextareas] вызвана');

    const elements = document.querySelectorAll('textarea');
    console.log(`[resizeTextareas] найдено ${elements.length} textarea`);

    elements.forEach((el, index) => {
        console.log(`\n[${index}] value:`, JSON.stringify(el.value));
        console.log(`[${index}] scrollHeight до: ${el.scrollHeight}`);
        console.log(`[${index}] offsetHeight до: ${el.offsetHeight}`);

        el.style.height = 'auto';
        el.style.height = (el.scrollHeight + 12) + 'px';

        console.log(`[${index}] scrollHeight после: ${el.scrollHeight}`);
        console.log(`[${index}] offsetHeight после: ${el.offsetHeight}`);
    });
};




window.resizeSingleTextarea = (el) => {
    if (!el) {
        console.log('❌ Элемент не передан в resizeSingleTextarea');
        return;
    }

    console.log('✅ Resize для:', el.value);
    el.style.height = 'auto';
    el.style.height = el.scrollHeight + 'px';
};




window.getApiBaseUrl = () => {
    const tag = document.querySelector('meta[name="api-base-url"]');
    return tag?.content || "/";
};