var PouchDB = require("pouchdb-browser").default;
var db = new PouchDB("constellation");
const couchdb_host = "sofa.hoxer.net"
var configuration = {}

db.get("user").then( doc=> { configuration = doc;} )
db.get("user").then( () => {
    
}
)

//import SHA1 from './SHA1.js'
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
        //let password = SHA1.SHA1(configuration.password);
        let dbname = "userdb-416e6e655f48345044313031313231"//this.getCouchDBName();
        configuration.password = "dam"
        let uri = 'https://'+username+":"+configuration.password+"@"+couchdb_host+":6984/"+dbname
        //uri= 'http://admin:123hemlig@'+couchdb_host+":5984/test"
        console.log(uri);
        PouchDB.sync(uri,db);
    }
    
}