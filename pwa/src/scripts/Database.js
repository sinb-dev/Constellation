var PouchDB = require("pouchdb-browser").default;
var db = new PouchDB("constellation");
const couchdb_host = "sofa.hoxer.net"
import store from '@/store'
export default {
    loadData() {
        db.get('user')
            .then((doc) => {
                console.log(doc.container_definitions)
                store.state.user = doc;
                /*store.state.username = doc.username,
                store.state.password = doc.password,
                store.state.course = doc.course
                store.state.container_definitions = doc.container_definitions;
                store.state._rev = doc._rev;*/
                this.sync();
            })
            .catch(function(e) {
                if (e.status == 404) {
                    require("../router/index.js").default.push("/setup")
                }
                
                //document.location.href="/setup"
            })
    },
    saveData() 
    {
        return db.put({
            _id : "user",
            _rev : store.state.user._rev,
            username : store.state.user.username,
            password : store.state.user.password,
            course : store.state.user.course,
            container_definitions : store.state.user.container_definitions
        }).then(()=>this.loadData())

    //  db.sync('constellation', 'https://localhost:5984/mydb');

    },
    getCouchDBName()
    {
        let encoded = new Buffer(store.state.user.username+"_"+store.state.user.course).toString('hex');    
        return "userdb-"+encoded
    },
    sync() {
        
        let username = store.state.user.username + "_" + store.state.user.course;        
        let dbname = this.getCouchDBName();
        let uri = 'https://'+username+":"+store.state.user.password+"@"+couchdb_host+":6984/"+dbname
        console.log(uri)
        PouchDB.sync(uri,db);
    },
    save() {

    }
    
}