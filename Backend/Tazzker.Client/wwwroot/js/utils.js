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