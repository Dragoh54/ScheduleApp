try {
    const status = rs.status();
    print("Replica set already initialized, status:");
    printjson(status);
} catch (e) {
    if (e.codeName === 'NotYetInitialized') {
        print("Initializing replica set...");
        
        rs.initiate({
            "_id": "rs0",
            "members": [
                { "_id": 0, "host": "mongo1:27017", "priority": 3 },
                { "_id": 1, "host": "mongo2:27017", "priority": 2 },
                { "_id": 2, "host": "mongo3:27017", "priority": 1 }
            ]
        });
        
        sleep(10000);
    } else {
        print("Error checking replica set status:", e);
    }
}

quit();