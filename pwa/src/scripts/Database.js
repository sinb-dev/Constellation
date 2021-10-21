var PouchDB = require("pouchdb-browser").default;
var db = new PouchDB("constellation");
const couchdb_host = "sofa.hoxer.net"
var configuration = {}

db.get("user").then( doc=> { configuration = doc;} )
db.get("user").then( () => {
    
})


export default {
    loadConfiguration() {
        return db.get("user")
    },
    getCouchDBName()
    {
        let encoded = new Buffer(configuration.username+"_"+configuration.course).toString('hex');    
        return "userdb-"+encoded
    },
    sync() {
        let username = configuration.username + "_" + configuration.course;        
        let dbname = this.getCouchDBName();
        let uri = 'https://'+username+":"+configuration.password+"@"+couchdb_host+":6984/"+dbname
        
        PouchDB.sync(uri,db);
    }
    
}