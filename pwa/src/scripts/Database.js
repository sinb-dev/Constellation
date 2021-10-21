var PouchDB = require("pouchdb-browser").default;
var db = new PouchDB("constellation");
const couchdb_host = "sofa.hoxer.net"
import store from '@/store'
export default {
    loadConfiguration() {
        db.get('user')
            .then((doc) => {
                
                this.sync();
                store.state.username = doc.username,
                store.state.password = doc.password,
                store.state.course = doc.course
                store.state.container_definitions = doc.container_definitions;
                store.state._rev = doc._rev;
            })
            .catch(function(e) {
                if (e.status == 404) {
                    require("../router/index.js").default.push("/setup")
                }
                
                //document.location.href="/setup"
            })
    },
    getCouchDBName()
    {
        let encoded = new Buffer(store.state.username+"_"+store.state.course).toString('hex');    
        return "userdb-"+encoded
    },
    sync() {
        let username = store.state.username + "_" + store.state.course;        
        let dbname = this.getCouchDBName();
        let uri = 'https://'+username+":"+store.state.password+"@"+couchdb_host+":6984/"+dbname
        
        PouchDB.sync(uri,db);
    }
    
}