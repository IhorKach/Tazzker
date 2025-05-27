window.checkIndexedDbStores = async function (dbName) {
    return new Promise((resolve, reject) => {
        const request = indexedDB.open(dbName);

        request.onsuccess = function (event) {
            const db = event.target.result;
            const storeNames = db.objectStoreNames;
            resolve(storeNames.length > 0);
            db.close();
        };

        request.onerror = function () {
            resolve(false);
        };
    });
};

window.focusAndMoveCursorToEnd = function (el) {
    if (!el) return;
    el.focus();
    const val = el.value;
    el.value = '';
    el.value = val;
};

window.resizeTextareas = () => {

    const elements = document.querySelectorAll('textarea');

    elements.forEach((el, index) => {

        el.style.height = 'auto';
        el.style.height = (el.scrollHeight + 12) + 'px';
    });
};

window.getApiBaseUrl = () => {
    const tag = document.querySelector('meta[name="api-base-url"]');
    return tag?.content || "/";
};

window.isOnline = () => navigator.onLine;
