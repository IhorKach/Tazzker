window.indexedDbBridge = {
    openDb: function (dbName, version, storeNames) {
        return new Promise((resolve, reject) => {
            const request = indexedDB.open(dbName, version);

            request.onupgradeneeded = function (event) {
                const db = event.target.result;
                storeNames.forEach(store => {
                    if (!db.objectStoreNames.contains(store)) {
                        db.createObjectStore(store, { keyPath: "id" });
                    }
                });
            };

            request.onsuccess = function () {
                resolve();
            };

            request.onerror = function (event) {
                reject(event.target.error);
            };
        });
    },

    addRecord: function (dbName, storeName, data) {
        return new Promise((resolve, reject) => {
            const request = indexedDB.open(dbName);

            request.onsuccess = function (event) {
                try {
                    const db = event.target.result;
                    const tx = db.transaction(storeName, "readwrite");
                    const store = tx.objectStore(storeName);
                    const addRequest = store.put(data);

                    addRequest.onsuccess = () => resolve();
                    addRequest.onerror = (e) => reject(e.target.error);
                } catch (err) {
                    console.error(`[IndexedDbBridge] Store "${storeName}" not found — reloading...`, err);
                    location.reload();
                }
            };

            request.onerror = function (event) {
                reject(event.target.error);
            };
        });
    },

    getAll: function (dbName, storeName) {
        return new Promise((resolve, reject) => {
            const request = indexedDB.open(dbName);

            request.onsuccess = function (event) {
                try {
                    const db = event.target.result;
                    const tx = db.transaction(storeName, "readonly");
                    const store = tx.objectStore(storeName);
                    const getAllRequest = store.getAll();

                    getAllRequest.onsuccess = () => resolve(getAllRequest.result);
                    getAllRequest.onerror = (e) => reject(e.target.error);
                } catch (err) {
                    console.error(`[IndexedDbBridge] Store "${storeName}" not found — reloading...`, err);
                    location.reload();
                }
            };

            request.onerror = function (event) {
                reject(event.target.error);
            };
        });
    },

    deleteRecord: function (dbName, storeName, id) {
        return new Promise((resolve, reject) => {
            const request = indexedDB.open(dbName);

            request.onsuccess = function (event) {
                try {
                    const db = event.target.result;
                    const tx = db.transaction(storeName, "readwrite");
                    const store = tx.objectStore(storeName);
                    const deleteRequest = store.delete(id);

                    deleteRequest.onsuccess = () => resolve();
                    deleteRequest.onerror = (e) => reject(e.target.error);
                } catch (err) {
                    console.error(`[IndexedDbBridge] Store "${storeName}" not found — reloading...`, err);
                    location.reload();
                }
            };

            request.onerror = function (event) {
                reject(event.target.error);
            };
        });
    },

    clearStore: function (dbName, storeName) {
        return new Promise((resolve, reject) => {
            const request = indexedDB.open(dbName);

            request.onsuccess = function (event) {
                try {
                    const db = event.target.result;
                    const tx = db.transaction(storeName, "readwrite");
                    const store = tx.objectStore(storeName);
                    const clearRequest = store.clear();

                    clearRequest.onsuccess = () => resolve();
                    clearRequest.onerror = (e) => reject(e.target.error);
                } catch (err) {
                    console.error(`[IndexedDbBridge] Store "${storeName}" not found — reloading...`, err);
                    location.reload();
                }
            };

            request.onerror = function (event) {
                reject(event.target.error);
            };
        });
    },
    safeInitDb: function (dbName, version, expectedStores) {
        return new Promise((resolve, reject) => {
            const request = indexedDB.open(dbName, version);

            request.onupgradeneeded = function (event) {
                const db = event.target.result;
                expectedStores.forEach(store => {
                    if (!db.objectStoreNames.contains(store)) {
                        db.createObjectStore(store, { keyPath: "id" });
                    }
                });
            };

            request.onsuccess = function (event) {
                const db = event.target.result;
                const missing = expectedStores.some(store => !db.objectStoreNames.contains(store));
                if (missing) {
                    db.close();
                    const deleteReq = indexedDB.deleteDatabase(dbName);
                    deleteReq.onsuccess = function () {
                        indexedDbBridge.safeInitDb(dbName, version, expectedStores)
                            .then(resolve)
                            .catch(reject);
                    };
                    deleteReq.onerror = e => reject(e.target.error);
                    deleteReq.onblocked = () => reject("Deletion blocked");
                } else {
                    resolve(); 
                }
            };

            request.onerror = e => reject(e.target.error);
        });
    }



};
