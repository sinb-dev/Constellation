var PouchDB = require("pouchdb-browser").default;
var db = new PouchDB("constellation");

export default {
    checkSetupComplete() {
        return db.get("configuration")
    }
}